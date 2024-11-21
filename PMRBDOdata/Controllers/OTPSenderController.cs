using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PMRBDOdata.Controllers
{
    [Route("odata/OTPSender")]
    [ApiController]
    public class OTPSenderController : ODataController
    {
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;

        public OTPSenderController(IConfiguration config, IMemoryCache cache)
        {
            _config = config;
            _cache = cache;
        }

        [HttpPost("send")]
        public IActionResult SendOTP([FromBody] OtpSenderRequest request)
        {
            // Tạo mã OTP ngẫu nhiên
            var otpCode = new Random().Next(100000, 999999).ToString();

            // Khởi tạo Twilio client
            TwilioClient.Init(_config["Twilio:AccountSID"], _config["Twilio:AuthToken"]);

            try
            {
                var message = MessageResource.Create(
                    to: new PhoneNumber(request.PhoneNumber),
                    from: new PhoneNumber(_config["Twilio:FromPhoneNumber"]),
                    body: $"Your OTP code is: {otpCode}");

                // Lưu OTP vào cache với thời gian hết hạn 5 phút
                _cache.Set(request.PhoneNumber, otpCode, TimeSpan.FromMinutes(5));

                return Ok(new { Success = true, Message = "OTP sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP([FromBody] OTPVerificationRequest request)
        {
            // Kiểm tra OTP từ cache
            if (_cache.TryGetValue(request.PhoneNumber, out string cachedOTP))
            {
                if (cachedOTP == request.OTPCode)
                {
                    _cache.Remove(request.PhoneNumber); // Xóa OTP sau khi xác thực thành công
                    return Ok(new { Success = true, Message = "OTP verified successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Invalid OTP" });
                }
            }
            else
            {
                return BadRequest(new { Success = false, Message = "OTP expired or invalid" });
            }
        }
    }

    public class OtpSenderRequest
    {
        public string PhoneNumber { get; set; } // Số điện thoại người nhận
    }

    public class OTPVerificationRequest
    {
        public string PhoneNumber { get; set; }
        public string OTPCode { get; set; }
    }
}
