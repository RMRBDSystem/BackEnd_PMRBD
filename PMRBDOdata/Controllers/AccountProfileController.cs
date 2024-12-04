using System.Net.Sockets;
using BusinessObject.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Repository.IRepository;
using Repository.Repository;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpPut("{id}")]
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
    }
}