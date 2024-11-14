using BusinessObject.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;
using System.Runtime.InteropServices;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Ebook")]
    [ApiController]
    public class EbookController : ODataController
    {
        private readonly IEbookRepository ebookRepository;
        private readonly IConfiguration _configuration;

        public EbookController(IConfiguration configuration)
        {
            ebookRepository = new EbookRepository();
            _configuration = configuration;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ebook>>> GetAllEbooks()
        {
            var list = await ebookRepository.GetAllEbooks();
            
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ebook>> GetEbookById([FromODataUri] int id)
        {
            var ebook = await ebookRepository.GetEbookById(id);
            if (ebook == null)
            {
                return NotFound();
            }
            
            return Ok(ebook);
        }

        [HttpPost]
        public async Task<IActionResult> AddEbook([FromBody] Ebook ebook)
        { 

            try
            {
                if (!ModelState.IsValid)
                {
                     BadRequest(ModelState);
                }
                await ebookRepository.AddEbook(ebook);
                var ebookId = ebook.EbookId;
                return CreatedAtAction(nameof(GetEbookById), new { id = ebookId }, ebook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{ebookId}")]
        public async Task<IActionResult> UpdateEBook(IFormFile image, [FromForm] Ebook ebook, [FromODataUri] int ebookId)
        {
            try
            {
                var ebookEntity = await ebookRepository.GetEbookById(ebookId);
                ebook.EbookId = ebookEntity.EbookId;

                if (image != null && image.Length > 0)
                {
                    var allowedFileTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.presentationml.presentation", "image/jpeg", "image/png" };
                    if (!allowedFileTypes.Contains(image.ContentType))
                    {
                        return BadRequest("Invalid file type. Only PDF, PPTX, JPEG, and PNG files are allowed.");
                    }

                    var firebaseSettings = _configuration.GetSection("FirebaseSettings").Get<FirebaseSettings>();
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseSettings.ApiKey));
                    var authLink = await authProvider.SignInWithEmailAndPasswordAsync(firebaseSettings.AuthEmail, firebaseSettings.AuthPassword);

                    var firebaseStorage = new FirebaseStorage(
                        firebaseSettings.Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                            ThrowOnCancel = true
                        });

                    var imageFileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";
                    await firebaseStorage.Child("EbookImages").Child(ebook.CreateById.ToString()).Child(imageFileName).PutAsync(image.OpenReadStream());

                    var imageUrl = await firebaseStorage.Child("EbookImages").Child(ebook.CreateById.ToString()).Child(imageFileName).GetDownloadUrlAsync();
                    ebook.ImageUrl = imageUrl;
                }

                await ebookRepository.UpdateEbook(ebook);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest("Error");
            }
        }

        public class FirebaseSettings
        {
            public string ApiKey { get; set; }
            public string Bucket { get; set; }
            public string AuthEmail { get; set; }
            public string AuthPassword { get; set; }
        }
    }
}
