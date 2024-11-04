using Firebase.Auth;
using Firebase.Storage;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using BusinessObject.Models;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadPDF")]
    [ApiController]
    public class UploadPDFController : ODataController
    {
       
        private readonly IWebHostEnvironment _env;
        private readonly IEbookRepository _ebookRepository;
        private readonly IConfiguration _configuration;
        public UploadPDFController(IWebHostEnvironment env, IEbookRepository ebookRepository, IConfiguration configuration)
        {
            _env = env;
            _ebookRepository = ebookRepository;
            _configuration = configuration;
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> UploadPDF([FromForm] IFormFile document, [FromODataUri] int Id)
        {
            string ApiKey = _configuration["FirebaseSettings:ApiKey"];
            string Bucket = _configuration["FirebaseSettings:Bucket"];
            string AuthEmail = _configuration["FirebaseSettings:AuthEmail"];
            string AuthPassword = _configuration["FirebaseSettings:AuthPassword"];
            try
            {
                // Check if the document is provided
                if (document == null || document.Length == 0)
                {
                    return BadRequest("No document file provided");
                }

                // Validate file type (PDF and PPTX only)
                var allowedFileTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.presentationml.presentation" };
                if (!allowedFileTypes.Contains(document.ContentType))
                {
                    return BadRequest("Invalid file type. Only PDF and PPTX files are allowed.");
                }

                // Generate a unique file name
                var fileName = $"{Path.GetFileNameWithoutExtension(document.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(document.FileName)}";

                // Sign in to Firebase
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var authLink = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Set up Firebase Storage
                var cancellationToken = new CancellationTokenSource();
                var firebaseStorage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                        ThrowOnCancel = true
                    });

                // Upload document to Firebase Storage
                var uploadTask = firebaseStorage
                    .Child("ebook")
                    .Child(Id.ToString())
                    .Child(fileName)
                    .PutAsync(document.OpenReadStream(), cancellationToken.Token);

                await uploadTask;

                // Retrieve the download URL
                var downloadUrl = await firebaseStorage
                    .Child("ebook")
                    .Child(Id.ToString())
                    .Child(fileName)
                    .GetDownloadUrlAsync();

                // Save URL in the database for the ebook type
                var eboookEntity = new Ebook
                {
                    EbookName = "aaaa",
                    EbookId = Id,
                    Pdfurl = downloadUrl,
                    Status = 1,
                    ImageUrl = "aaaa"
                };
                await _ebookRepository.AddEbook(eboookEntity);

                // Return the download URL
                return Ok(downloadUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{relativePath}")]
        public async Task<IActionResult> GetPDF([FromODataUri] string relativePath)
        {
            try
            {
                string filePath = Path.Combine("wwwroot", relativePath);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open);
                return File(fileStream, GetContentType(filePath), Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetContentType(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                default:
                    return "application/octet-stream";
            }
        }


        [HttpDelete]
        public IActionResult DeletePDF([FromBody] DeletePDFRequest relativePath)
        {
            try
            {
                string filePath = Path.Combine("wwwroot", relativePath.RelativePath.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok("File deleted successfully");
                }
                else
                {
                    return NotFound("File not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class DeletePDFRequest
        {
            public string RelativePath { get; set; }
        }
    }
}