using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadImage")]
    [ApiController]
    public class UploadImageController : ODataController
    {
        private readonly string _imageDirectory_Avatar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Avatar");
        private readonly string _imageDirectory_Recipe = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Recipe");
        private readonly string _imageDirectory_Book = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Book");
        private readonly string _imageDirectory_Ebook = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Ebook");
        private readonly string _imageDirectory_Service = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Service");

        [HttpPost("{Type}")]
        public async Task<IActionResult> UploadImage(IFormFile image, [FromODataUri] int Type)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image file provided");
                }

                string _imageDirectory = "";

                if (Type == 1)
                {
                    _imageDirectory = _imageDirectory_Avatar;
                }
                else if (Type == 2)
                {
                    _imageDirectory = _imageDirectory_Recipe;
                }
                else if (Type == 3)
                {
                    _imageDirectory = _imageDirectory_Book;
                }
                else if (Type == 4)
                {
                    _imageDirectory = _imageDirectory_Ebook;
                }
                else if (Type == 5)
                {
                    _imageDirectory = _imageDirectory_Service;
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

                if (Type == 1)
                {
                    var relativePath = $"/Images/Avatar/{fileName}";
                    return Ok(relativePath);
                }
                else if (Type == 2)
                {
                    var relativePath = $"/Images/Recipe/{fileName}";
                    return Ok(relativePath);
                }
                else if (Type == 3)
                {
                    var relativePath = $"/Images/Book/{fileName}";
                    return Ok(relativePath);
                }
                else if (Type == 4)
                {
                    var relativePath = $"/Images/Ebook/{fileName}";
                    return Ok(relativePath);
                }
                else if (Type == 5)
                {
                    var relativePath = $"/Images/Service/{fileName}";
                    return Ok(relativePath);
                }
                return BadRequest("Invalid image type");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    //    [HttpPost("{UserID}/{Type}")]
    //    public async Task<IActionResult> UploadUserProfileImage(IFormFile image, [FromODataUri] int UserID, [FromODataUri] int Type)
    //    {
    //        private readonly string _imageDirectory_Service = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Service");
    // }
    }
}
    
   
