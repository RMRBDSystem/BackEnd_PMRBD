
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace PMRBDOdata.Controllers
{
    [Route("odata/SMSSender")]
    [ApiController]
    public class SMSSenderController : ODataController
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public SMSSenderController(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _cache = memoryCache;
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["Mocean:ApiKey"]; // Lấy API Key từ appsettings.json
            _apiSecret = configuration["Mocean:ApiSecret"]; // Lấy API Secret từ appsettings.json
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromBody] OtpRequest request)
        {
            try
            {
                // Tạo mã OTP ngẫu nhiên 6 chữ số
                var otpCode = new Random().Next(100000, 999999).ToString();

                // Lưu OTP vào cache với thời gian hết hạn là 5 phút
                _cache.Set(request.PhoneNumber, otpCode, TimeSpan.FromMinutes(5));

                var url = $"https://rest.moceanapi.com/rest/2/sms";
                var payload = new Dictionary<string, string>
                {
                    { "mocean-api-key", _apiKey },
                    { "mocean-api-secret", _apiSecret },
                    { "mocean-to", request.PhoneNumber },
                    { "mocean-from", "PMRBD" },
                    { "mocean-text", $"Mã OTP của bạn là: {otpCode}" }
                };

                var content = new FormUrlEncodedContent(payload);
                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { message = "OTP đã gửi thành công!" });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, new { message = "Gửi OTP thất bại.", error = errorContent });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi gửi OTP.", error = ex.Message });
            }
        }

        [HttpPost("verify")]
        public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            // Kiểm tra OTP từ cache
            if (_cache.TryGetValue(request.PhoneNumber, out string cachedOtp))
            {
                if (cachedOtp == request.OtpCode)
                {
                    // OTP hợp lệ
                    _cache.Remove(request.PhoneNumber); // Xóa OTP sau khi xác thực thành công
                    return Ok(new { message = "OTP hợp lệ!" });
                }
                else
                {
                    // OTP không hợp lệ
                    return BadRequest(new { message = "OTP không đúng." });
                }
            }
            else
            {
                // OTP đã hết hạn hoặc không tồn tại
                return BadRequest(new { message = "OTP đã hết hạn hoặc không tồn tại." });
            }
        }
    }

    public class OtpRequest
    {
        public string PhoneNumber { get; set; } // Số điện thoại người nhận
    }

    public class OtpVerificationRequest
    {
        public string PhoneNumber { get; set; } // Số điện thoại người nhận
        public string OtpCode { get; set; }     // Mã OTP cần xác thực
    }

}
