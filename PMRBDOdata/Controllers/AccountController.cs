using BusinessObject.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Identity.Client;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Account")]
    [ApiController]

    public class AccountController : ODataController
    {
        private static string ApiKey = "AIzaSyCPn2OSvk7rHKjBFwe9Sa_v-aSUZUHxdM4";
        private static string Bucket = "rmrbdfirebase.appspot.com";
        private static string AuthEmail = "ngockhanhpham8a@gmail.com";
        private static string AuthPassword = "khanh30320";
        private readonly IAccountRepository accountRepository;
        public AccountController()
        {
            accountRepository = new AccountRepository();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var list = await accountRepository.GetAllAccounts();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById([FromODataUri] int id)
        {
            var account = await accountRepository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task AddAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await accountRepository.AddAccount(account);
                //return Created(account);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccount(
    [FromODataUri] int id,
    IFormFile? image,
    [FromForm] string userName,
    [FromForm] string googleId,
    [FromForm] string email,
    [FromForm] int status,
    [FromForm] int roleId,
    [FromForm] decimal coin)
        {
            try
            {
                // Lấy thông tin tài khoản hiện tại từ cơ sở dữ liệu
                var existingAccount = await accountRepository.GetAccountById(id);
                if (existingAccount == null)
                {
                    return NotFound(new { message = "Account not found" });
                }

                // Nếu không có ảnh mới được tải lên, giữ nguyên giá trị Avatar hiện tại
                string avatarUrl = existingAccount.Avatar;

                if (image != null && image.Length > 0)
                {
                    // Tạo tên file ảnh mới
                    var imageFileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";

                    // Tải ảnh lên Firebase Storage
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                    var authLink = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                    var cancellationToken = new CancellationTokenSource();
                    var firebaseStorage = new FirebaseStorage(
                        Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                            ThrowOnCancel = true
                        });

                    var uploadTaskImage = firebaseStorage
                        .Child("Image Account")
                        .Child(id.ToString())
                        .Child(imageFileName)
                        .PutAsync(image.OpenReadStream(), cancellationToken.Token);
                    await uploadTaskImage;

                    // Lấy URL của ảnh mới từ Firebase
                    avatarUrl = await firebaseStorage
                        .Child("Image Account")
                        .Child(id.ToString())
                        .Child(imageFileName)
                        .GetDownloadUrlAsync();
                }

                // Cập nhật thông tin tài khoản với Avatar URL mới hoặc giữ nguyên
                var accountEntity = new Account()
                {
                    AccountId = id,
                    Avatar = avatarUrl,
                    UserName = userName,
                    GoogleId = googleId,
                    Email = email,
                    AccountStatus = status,
                    RoleId = roleId,
                    Coin = coin
                };
                await accountRepository.UpdateAccount(accountEntity);

                return Ok(new { avatarURL = avatarUrl });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return BadRequest(new { message = "An error occurred while updating the account. Please try again later." });
            }
        }

    }
}
