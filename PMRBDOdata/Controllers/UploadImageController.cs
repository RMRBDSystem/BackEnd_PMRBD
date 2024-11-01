using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Repository;
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

        //[HttpPost("{TypeName}/{Id}")]
        //public async Task<IActionResult> UploadListImage(List<IFormFile> image, [FromODataUri] string TypeName, [FromODataUri] int Id)
        //{
        //    try
        //    {
        //        if (image == null || image.Count == 0)
        //        {
        //            return BadRequest("No image file provided");
        //        }
        //
        //        List<string> fileNameList = new List<string>();
        //
        //        foreach (var item in image)
        //        {
        //            var fileName = $"{Path.GetFileNameWithoutExtension(item.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(item.FileName)}";
        //            string directoryPath;
        //            string relativePath;
        //
        //            if (TypeName.Equals("Recipe"))
        //            {
        //                directoryPath = Path.Combine("wwwroot", "Images", "Recipe", Id.ToString());
        //                relativePath = $"/Images/Recipe/{Id}/{fileName}";
        //            }
        //            else if (TypeName.Equals("Book"))
        //            {
        //                directoryPath = Path.Combine("wwwroot", "Images", "Book", Id.ToString());
        //                relativePath = $"/Images/Book/{Id}/{fileName}";
        //            }
        //            else
        //            {
        //                return BadRequest("Invalid type");
        //            }
        //
        //            Directory.CreateDirectory(directoryPath);
        //
        //            string filePath = Path.Combine(directoryPath, fileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await item.CopyToAsync(fileStream);
        //            }
        //
        //            fileNameList.Add(relativePath);
        //        }
        //        return Ok(fileNameList);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        
        [HttpPost("{Type}/{Id}")]
        public async Task<IActionResult> UploadImage(IFormFile image, [FromODataUri] string Type, [FromODataUri] int Id)
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
                if (Type == "CustomerProfile")
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "CustomerProfile", Id.ToString());
                    relativePath = $"/Images/CustomerProfile/{Id}/{fileName}";
                }
                else if (Type == "EmployeeProfile")
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "EmployeeProfile", Id.ToString());
                    relativePath = $"/Images/EmployeeProfile/{Id}/{fileName}";
                }
                else if (Type == "Recipe")
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "Recipe", Id.ToString());
                    relativePath = $"/Images/Recipe/{Id}/{fileName}";

                    var imageEntity = new Image
                    {
                        RecipeId = Id,
                        BookId = null,
                        ImageUrl = relativePath,
                        Status = 1

                    };
                }
                else if (Type == "Book")
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "Book", Id.ToString());
                    relativePath = $"/Images/Book/{Id}/{fileName}";
                }
                else if (Type == "Ebook")
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "Ebook", Id.ToString());
                    relativePath = $"/Images/Ebook/{Id}/{fileName}";
                }
                else if (Type == "Service")
                {
                    directoryPath = Path.Combine("wwwroot", "Images", "Service", Id.ToString());
                    relativePath = $"/Images/Service/{Id}/{fileName}";
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


