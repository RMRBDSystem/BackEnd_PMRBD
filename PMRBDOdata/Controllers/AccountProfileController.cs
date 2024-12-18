using BusinessObject.Models;
using Firebase.Auth;
using Firebase.Storage;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MimeKit;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/AccountProfile")]
    [ApiController]
    public class AccountProfileController : ODataController
    {
        private static string ApiKey = "AIzaSyCPn2OSvk7rHKjBFwe9Sa_v-aSUZUHxdM4";
        private static string Bucket = "rmrbdfirebase.appspot.com";
        private static string AuthEmail = "ngockhanhpham8a@gmail.com";
        private static string AuthPassword = "khanh30320";

        private readonly IAccountProfileRepository accountProfileRepository;
        private readonly IConfiguration _configuration;

        public AccountProfileController(IConfiguration configuration)
        {
            accountProfileRepository = new AccountProfileRepository();
            _configuration = configuration;
        }
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RMRBDSystem", "ngockhanhpham8a@gmail.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    // Kết nối đến Gmail SMTP server với bảo mật TLS
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // Sử dụng mật khẩu ứng dụng
                    await client.AuthenticateAsync("ngockhanhpham8a@gmail.com", "iukm cdoc qkwx wmqu");

                    await client.SendAsync(message); // Gửi email
                    await client.DisconnectAsync(true); // Ngắt kết nối sau khi gửi
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    Console.WriteLine("Error sending email: " + ex.Message);
                }
            }
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<AccountProfile>>> GetAllAccountProfiles()
        {
            var list = await accountProfileRepository.GetAllAccountProfiles();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountProfile>> GetAccountProfileById([FromODataUri] int id)
        {
            var accountProfile = await accountProfileRepository.GetAccountProfileByAccountId(id);
            if (accountProfile == null)
            {
                return NotFound();
            }
            return Ok(accountProfile);
        }

        [HttpPost]
        public async Task AddAccountProfile([FromBody] AccountProfile accountProfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await accountProfileRepository.AddAccountProfile(accountProfile);
                //return Created(accountProfile);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("Censor/{id}")]
        public async Task<ActionResult<AccountProfile>> UpdateAccountProfile([FromODataUri] int id, [FromBody] AccountProfile accountProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountProfileToUpdate = await accountProfileRepository.GetAccountProfileByAccountId(id);
            if (accountProfileToUpdate == null)
            {
                return NotFound();
            }
            accountProfile.AccountId = accountProfileToUpdate.AccountId;
            await accountProfileRepository.UpdateAccountProfile(accountProfile);

            var loginUrl = "https://fe-rmrbd.vercel.app/login";
            if (accountProfile.Status == 0)
            {
                var userEmail = accountProfileToUpdate.Account.Email;
                var subject = "[RMRBDSystem]Yêu cầu bổ sung thông tin tài khoản";
                var body = $@"
                <html>
                    <body>
                        <p>Chào <strong>{accountProfileToUpdate.Account.UserName}</strong>,</p>
                        <br>
                        <p>Cảm ơn bạn đã đăng ký tài khoản trên hệ thống của chúng tôi. Tuy nhiên, hồ sơ của bạn chưa được duyệt vì một số thông tin còn thiếu hoặc không hợp lệ.</p>
                        <p>Để đảm bảo tài khoản được kích hoạt thành công, vui lòng kiểm tra lại thông tin và gửi lại biểu mẫu đăng ký của bạn.</p>
                        <p>Nếu cần hỗ trợ thêm, bạn có thể liên hệ với chúng tôi qua email <a href='mailto:RMRBDSystem@gmail.com'>ngockhanhpham8a@gmail.com</a> hoặc số hotline <strong>1800-123-456</strong>.</p>
                        <br>
                        <p>Trân trọng,<br>
                        Đội ngũ Hỗ trợ Khách hàng.</p>
                    </body>
                </html>";
                // Gửi email thông báo
                await SendEmailAsync(userEmail, subject, body);
            }

            if (accountProfile.Status == 1)
            {
                var userEmail = accountProfileToUpdate.Account.Email;
                var subject = "[RMRBDSystem]Chúc mừng! Tài khoản của bạn đã được duyệt";
                var body = $@"
                <html>
                  <body>
                      <p>Chào <strong>{accountProfileToUpdate.Account.UserName}</strong>,</p>
                      <br>
                      <p>Chúng tôi xin thông báo rằng thông tin tài khoản của bạn đã được cập nhật thành công trên hệ thống của chúng tôi.</p>
                      <p>Hãy đăng nhập và khám phá ngay tại: <a href='{loginUrl}'>Đăng nhập ngay</a>.</p>
                      <p>Nếu bạn gặp bất kỳ vấn đề nào trong quá trình sử dụng, vui lòng liên hệ với đội ngũ hỗ trợ của chúng tôi qua email <a href='mailto:RMRBDSystem@gmail.com'>ngockhanhpham8a@gmail.com</a> hoặc qua phần livechat.</p>
                      <p>Cảm ơn bạn đã tin tưởng sử dụng dịch vụ của chúng tôi!</p>
                      <br>
                      <p>Trân trọng,<br>
                      Đội ngũ Hỗ trợ Khách hàng.</p>
                  </body>
                </html>";

                // Gửi email thông báo
                await SendEmailAsync(userEmail, subject, body);
            }

            if (accountProfile.Status == 2)
            {
                var userEmail = accountProfileToUpdate.Account.Email;
                var subject = "[RMRBDSystem]Yêu cầu cập nhật lại thông tin thanh toán thành công";
                var body = $@"
                 <html>
                   <body>
                       <p>Chào <strong>{accountProfileToUpdate.Account.UserName}</strong>,</p>
                       <br>
                       <p>Yêu cầu cập nhật thông tin thanh toán của bạn đã được xử lý thành công.</p>
                       <p>Vui lòng đăng nhập vào hệ thống để kiểm tra và đảm bảo rằng các thông tin thanh toán của bạn đã chính xác:</p>
                       <p><a href='{loginUrl}'>Đăng nhập ngay</a></p>
                       <br>
                       <p>Nếu bạn có bất kỳ câu hỏi hoặc cần hỗ trợ thêm, vui lòng liên hệ với đội ngũ hỗ trợ khách hàng của chúng tôi.</p>
                       <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!</p>
                       <br>
                       <p>Trân trọng,<br>
                       Đội ngũ Hỗ trợ Khách hàng.</p>
                   </body>
                 </html>";

                // Gửi email thông báo
                await SendEmailAsync(userEmail, subject, body);
            }

            return Updated(accountProfile);
        }


        [HttpPost("{AccountID}")]
        public async Task<IActionResult> UploadImage(
    IFormFile bankAccountQR,
    IFormFile portrait,
    [FromODataUri] int accountID,
    IFormFile frontIDCard,
    IFormFile backIDCard,
    [FromForm] DateTime dateOfBirth,
    [FromForm] string iDCardNumber)
        {

            try
            {
                // Check if account profile exists and is under review
                var accountProfile = await accountProfileRepository.GetAccountProfileByAccountId(accountID);
                if (accountProfile != null && accountProfile.Status == -1)
                {
                    return BadRequest(new { message = "You have already submitted your profile. Please wait for approval." });
                }



                // Validate required fields
                if (string.IsNullOrWhiteSpace(iDCardNumber))
                {
                    return BadRequest(new { message = "Please provide all required fields." });
                }



                // Check required files
                if (portrait == null || portrait.Length == 0)
                    return BadRequest(new { message = "No portrait file provided." });
                if (bankAccountQR == null || bankAccountQR.Length == 0)
                    return BadRequest(new { message = "No bank account QR file provided." });
                if (frontIDCard == null || frontIDCard.Length == 0)
                    return BadRequest(new { message = "No front ID card file provided." });
                if (backIDCard == null || backIDCard.Length == 0)
                    return BadRequest(new { message = "No back ID card file provided." });



                // Generate unique file names
                var portraitFileName = $"{Path.GetFileNameWithoutExtension(portrait.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(portrait.FileName)}";
                var bankQRFileName = $"{Path.GetFileNameWithoutExtension(bankAccountQR.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(bankAccountQR.FileName)}";
                var frontIDCardFileName = $"{Path.GetFileNameWithoutExtension(frontIDCard.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(frontIDCard.FileName)}";
                var backIDCardFileName = $"{Path.GetFileNameWithoutExtension(backIDCard.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(backIDCard.FileName)}";


                // Firebase Authentication
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var authLink = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Configure Firebase Storage
                var cancellationToken = new CancellationTokenSource();
                var firebaseStorage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                        ThrowOnCancel = true
                    });


                // Upload files to Firebase Storage
                var uploadTaskPortrait = firebaseStorage
                    .Child("Portrait")
                    .Child(accountID.ToString())
                    .Child(portraitFileName)
                    .PutAsync(portrait.OpenReadStream(), cancellationToken.Token);
                await uploadTaskPortrait;

                var uploadTaskBankQR = firebaseStorage
                    .Child("BankQR")
                    .Child(accountID.ToString())
                    .Child(bankQRFileName)
                    .PutAsync(bankAccountQR.OpenReadStream(), cancellationToken.Token);
                await uploadTaskBankQR;

                var uploadTaskFrontIDCard = firebaseStorage
                    .Child("FrontIDCard")
                    .Child(accountID.ToString())
                    .Child(frontIDCardFileName)
                    .PutAsync(frontIDCard.OpenReadStream(), cancellationToken.Token);
                await uploadTaskFrontIDCard;

                var uploadTaskBackIDCard = firebaseStorage
                    .Child("BackIDCard")
                    .Child(accountID.ToString())
                    .Child(backIDCardFileName)
                    .PutAsync(backIDCard.OpenReadStream(), cancellationToken.Token);
                await uploadTaskBackIDCard;

                // Retrieve download URLs
                var downloadUrlPortrait = await firebaseStorage
                    .Child("Portrait")
                    .Child(accountID.ToString())
                    .Child(portraitFileName)
                    .GetDownloadUrlAsync();

                var downloadUrlBankQR = await firebaseStorage
                    .Child("BankQR")
                    .Child(accountID.ToString())
                    .Child(bankQRFileName)
                    .GetDownloadUrlAsync();

                var downloadUrlFrontIDCard = await firebaseStorage
                    .Child("FrontIDCard")
                    .Child(accountID.ToString())
                    .Child(frontIDCardFileName)
                    .GetDownloadUrlAsync();

                var downloadUrlBackIDCard = await firebaseStorage
                    .Child("BackIDCard")
                    .Child(accountID.ToString())
                    .Child(backIDCardFileName)
                    .GetDownloadUrlAsync();

                // Save to database
                var accountProfileEntity = new AccountProfile
                {
                    AccountId = accountID,
                    FrontIdcard = downloadUrlFrontIDCard,
                    BackIdcard = downloadUrlBackIDCard,
                    IdcardNumber = iDCardNumber,
                    DateOfBirth = dateOfBirth,
                    Portrait = downloadUrlPortrait,
                    BankAccountQR = downloadUrlBankQR,
                    Status = -1
                };
                await accountProfileRepository.AddAccountProfile(accountProfileEntity);

                // Return download URLs
                return Ok(new { portraitUrl = downloadUrlPortrait, bankQRUrl = downloadUrlBankQR, frontIDCardUrl = downloadUrlFrontIDCard, backIDCardUrl = downloadUrlBackIDCard });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return BadRequest(new { message = "An error occurred while uploading the files. Please try again later." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountProfile([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await accountProfileRepository.DeleteAccountProfile(id);
            return NoContent();
        }



        [HttpPut("{AccountID}")]
        public async Task<IActionResult> UpdateProfile(
    IFormFile bankAccountQR,
    IFormFile portrait,
    [FromODataUri] int accountID,
    IFormFile frontIDCard,
    IFormFile backIDCard,
    [FromForm] DateTime dateOfBirth,
    [FromForm] string iDCardNumber)
        {
            try
            {
                // Check if account profile exists
                var accountProfile = await accountProfileRepository.GetAccountProfileByAccountId(accountID);
                if (accountProfile == null)
                {
                    return NotFound(new { message = "Account profile not found." });
                }


                // Validate required fields
                if (string.IsNullOrWhiteSpace(iDCardNumber))
                {
                    return BadRequest(new { message = "Please provide all required fields." });
                }

                // Check for updated files and validate them
                if (portrait == null && bankAccountQR == null && frontIDCard == null && backIDCard == null)
                {
                    return BadRequest(new { message = "Please provide at least one file to update." });
                }

                // Generate unique file names if files are provided
                string portraitFileName = accountProfile.Portrait;
                string bankQRFileName = accountProfile.BankAccountQR;
                string frontIDCardFileName = accountProfile.FrontIdcard;
                string backIDCardFileName = accountProfile.BackIdcard;

                var cancellationToken = new CancellationTokenSource();
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var authLink = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Configure Firebase Storage
                var firebaseStorage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                        ThrowOnCancel = true
                    });

                // Upload files if provided
                if (portrait != null && portrait.Length > 0)
                {
                    var newPortraitFileName = $"{Path.GetFileNameWithoutExtension(portrait.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(portrait.FileName)}";
                    var uploadTaskPortrait = firebaseStorage
                        .Child("Portrait")
                        .Child(accountID.ToString())
                        .Child(newPortraitFileName)
                        .PutAsync(portrait.OpenReadStream(), cancellationToken.Token);
                    await uploadTaskPortrait;
                    portraitFileName = await firebaseStorage
                        .Child("Portrait")
                        .Child(accountID.ToString())
                        .Child(newPortraitFileName)
                        .GetDownloadUrlAsync();
                }

                if (bankAccountQR != null && bankAccountQR.Length > 0)
                {
                    var newBankQRFileName = $"{Path.GetFileNameWithoutExtension(bankAccountQR.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(bankAccountQR.FileName)}";
                    var uploadTaskBankQR = firebaseStorage
                        .Child("BankQR")
                        .Child(accountID.ToString())
                        .Child(newBankQRFileName)
                        .PutAsync(bankAccountQR.OpenReadStream(), cancellationToken.Token);
                    await uploadTaskBankQR;
                    bankQRFileName = await firebaseStorage
                        .Child("BankQR")
                        .Child(accountID.ToString())
                        .Child(newBankQRFileName)
                        .GetDownloadUrlAsync();
                }

                if (frontIDCard != null && frontIDCard.Length > 0)
                {
                    var newFrontIDCardFileName = $"{Path.GetFileNameWithoutExtension(frontIDCard.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(frontIDCard.FileName)}";
                    var uploadTaskFrontIDCard = firebaseStorage
                        .Child("FrontIDCard")
                        .Child(accountID.ToString())
                        .Child(newFrontIDCardFileName)
                        .PutAsync(frontIDCard.OpenReadStream(), cancellationToken.Token);
                    await uploadTaskFrontIDCard;
                    frontIDCardFileName = await firebaseStorage
                        .Child("FrontIDCard")
                        .Child(accountID.ToString())
                        .Child(newFrontIDCardFileName)
                        .GetDownloadUrlAsync();
                }

                if (backIDCard != null && backIDCard.Length > 0)
                {
                    var newBackIDCardFileName = $"{Path.GetFileNameWithoutExtension(backIDCard.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(backIDCard.FileName)}";
                    var uploadTaskBackIDCard = firebaseStorage
                        .Child("BackIDCard")
                        .Child(accountID.ToString())
                        .Child(newBackIDCardFileName)
                        .PutAsync(backIDCard.OpenReadStream(), cancellationToken.Token);
                    await uploadTaskBackIDCard;
                    backIDCardFileName = await firebaseStorage
                        .Child("BackIDCard")
                        .Child(accountID.ToString())
                        .Child(newBackIDCardFileName)
                        .GetDownloadUrlAsync();
                }

                // Update the account profile
                accountProfile.FrontIdcard = frontIDCardFileName;
                accountProfile.BackIdcard = backIDCardFileName;
                accountProfile.Portrait = portraitFileName;
                accountProfile.BankAccountQR = bankQRFileName;
                accountProfile.IdcardNumber = iDCardNumber;
                accountProfile.DateOfBirth = dateOfBirth;

                // Update profile status to under review
                accountProfile.Status = -1;
                // Save updated profile to the database
                await accountProfileRepository.UpdateAccountProfile(accountProfile);

                // Return the updated download URLs
                return Ok(new
                {
                    portraitUrl = portraitFileName,
                    bankQRUrl = bankQRFileName,
                    frontIDCardUrl = frontIDCardFileName,
                    backIDCardUrl = backIDCardFileName
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return BadRequest(new { message = "An error occurred while updating the profile. Please try again later." });
            }
        }

    }
}