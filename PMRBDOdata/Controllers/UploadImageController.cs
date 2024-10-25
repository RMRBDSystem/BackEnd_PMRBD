using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Net.Http.Headers;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadImage")]
    [ApiController]
    public class UploadImageController : ODataController
    {

        private readonly IWebHostEnvironment _env;

        public UploadImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        private readonly Dictionary<int, string> _imageDirectories = new Dictionary<int, string>
        {
            { 1, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Avatar") },
            { 2, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Recipe") },
            { 3, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Book") },
            { 4, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Ebook") },
            { 5, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Service") }
        };

        [HttpPost("{Type}")]
        public async Task<IActionResult> UploadImage(IFormFile image, [FromODataUri] int Type)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image file provided");
                }

                if (!_imageDirectories.TryGetValue(Type, out var _imageDirectory))
                {
                    return BadRequest("Invalid image type");
                }

                if (!Directory.Exists(_imageDirectory))
                {
                    Directory.CreateDirectory(_imageDirectory);
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(_imageDirectory, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var relativePath = $"/Images/{_imageDirectories[Type].Split(Path.DirectorySeparatorChar).Last()}/{fileName}";
                return Ok(relativePath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("{Type}/{Id}")]
        public async Task<IActionResult> UploadProfileImage(IFormFile image, [FromODataUri] int Type, [FromODataUri] int Id)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image file provided");
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";

                string directoryPath;
                string relativePath;
                if (Type == 1)
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "UserProfile", Id.ToString());
                    relativePath = $"/Images/UserProfile/{Id}/{fileName}";
                }
                else if (Type == 2)
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "EmployeeProfile", Id.ToString());
                    relativePath = $"/Images/EmployeeProfile/{Id}/{fileName}";
                }
                else
                {
                    return BadRequest("Invalid Type");
                }

                Directory.CreateDirectory(directoryPath);

                string filePath = Path.Combine(directoryPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return Ok(relativePath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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


