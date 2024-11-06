using BusinessObject.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadImage")]
    [ApiController]
    public class UploadImageController : ODataController
    {
        
        private readonly IWebHostEnvironment _env;
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _configuration;
        private readonly IEbookRepository _ebookRepository;

        public UploadImageController(IWebHostEnvironment env, IImageRepository imageRepository, IEbookRepository ebookRepository)
        {
            _env = env;
            _imageRepository = imageRepository;
            _ebookRepository = ebookRepository;
        }



        [HttpPost("{Type}/{Id}")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image, [FromODataUri] string Type, [FromODataUri] int Id)
        {
            string ApiKey = _configuration["FirebaseSettings:ApiKey"];
            string Bucket = _configuration["FirebaseSettings:Bucket"];
            string AuthEmail = _configuration["FirebaseSettings:AuthEmail"];
            string AuthPassword = _configuration["FirebaseSettings:AuthPassword"];
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image file provided");
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";

                // Đăng nhập vào Firebase
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var authLink = await authProvider.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Thiết lập Firebase Storage
                var cancellationToken = new CancellationTokenSource();
                var firebaseStorage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authLink.FirebaseToken),
                        ThrowOnCancel = true
                    });

                // Tải ảnh lên Firebase Storage
                var uploadTask = firebaseStorage
                    .Child(Type)
                    .Child(Id.ToString())
                    .Child(fileName)
                    .PutAsync(image.OpenReadStream(), cancellationToken.Token);

                await uploadTask;

                // Lấy URL tải về
                var downloadUrl = await firebaseStorage
                    .Child(Type)
                    .Child(Id.ToString())
                    .Child(fileName)
                    .GetDownloadUrlAsync();

                // Lưu vào cơ sở dữ liệu dựa trên Type

                if (Type == "CustomerProfile")
                {

                }
                else if (Type == "EmployeeProfile")
                {

                }
                else if (Type == "Recipe")
                {
                    var imageEntity = new Image
                    {
                        RecipeId = Id,
                        BookId = null,
                        ImageUrl = downloadUrl,
                        Status = 1

                    };
                    await _imageRepository.AddImage(imageEntity);
                }
                else if (Type == "Book")
                {

                }
                else if (Type == "Ebook")
                {

                }
                else if (Type == "Service")
                {

                }
                else
                {
                    return BadRequest("Invalid Type");
                }



                // Trả về URL tải ảnh
                return Ok(downloadUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("firstImage/{recipeId}")]
        public async Task<IActionResult> GetFirstImageByRecipeId([FromODataUri] int recipeId)
        {
            var image = await _imageRepository.GetFirstImageByRecipeId(recipeId);
            if (image == null)
            {
                return NotFound($"No images found for recipe ID {recipeId}.");
            }

            var downloadUrl = image.ImageUrl;
            try
            {
                // Tạo một HttpClient để lấy ảnh từ Firebase
                using (var httpClient = new HttpClient())
                {
                    var imageBytes = await httpClient.GetByteArrayAsync(downloadUrl);

                    // Xác định loại nội dung dựa trên phần mở rộng
                    var extension = Path.GetExtension(downloadUrl).ToLower();
                    string contentType = extension switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".bmp" => "image/bmp",
                        ".tiff" => "image/tiff",
                        _ => "application/octet-stream" // Loại mặc định nếu không xác định được
                    };

                    return File(imageBytes, contentType); // Trả về ảnh cho client
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve image from Firebase: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetImage([FromQuery] string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return NotFound();
            }

            var absolutePath = Path.Combine(_env.WebRootPath, relativePath.TrimStart('/'));

            if (!System.IO.File.Exists(absolutePath))
            {
                return NotFound();
            }

            var fileInfo = new FileInfo(absolutePath);
            var contentType = GetContentType(fileInfo.Extension);

            return File(System.IO.File.ReadAllBytes(absolutePath), contentType, fileInfo.Name);
        }

        private string GetContentType(string extension)
        {
            var types = GetMimeTypes();
            if (types.TryGetValue(extension.ToLowerInvariant(), out var contentType))
            {
                return contentType;
            }
            return "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
            };
        }
    }
}