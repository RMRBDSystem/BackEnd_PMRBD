using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadPDF")]
    [ApiController]
    public class UploadPDFController : ODataController
    {

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromODataUri] int id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file provided");
                }

                var allowedFileTypes = new[] { "application/pdf", "application/vnd.openxmlformats-officedocument.presentationml.presentation" };
                if (!allowedFileTypes.Contains(file.ContentType))
                {
                    return BadRequest("Invalid file type. Only PDF and PPTX files are allowed.");
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
                string directoryPath = Path.Combine("wwwroot", "PDF", id.ToString());
                string filePath = Path.Combine(directoryPath, fileName);

                Directory.CreateDirectory(directoryPath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                string relativePath = $"/PDF/{id}/{fileName}";
                return Ok(relativePath);
            }
            catch (Exception ex)
            {
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
    }
}
