using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace PMRBDOdata.Controllers
{
    [Route("odata/UploadVideo")]
    [ApiController]
    public class UploadVideoController : ODataController
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

                var allowedFileTypes = new[] { "video/mp4", "video/x-m4v", "video/quicktime", "video/x-msvideo", "video/x-flv", "video/x-mpeg" };
                if (!allowedFileTypes.Contains(file.ContentType))
                {
                    return BadRequest("Invalid file type. Only video files are allowed.");
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now.Ticks}{Path.GetExtension(file.FileName)}";
                string directoryPath = Path.Combine("wwwroot", "Videos", id.ToString());
                string filePath = Path.Combine(directoryPath, fileName);

                Directory.CreateDirectory(directoryPath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                string relativePath = $"/Videos/{id}/{fileName}";
                return Ok(relativePath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetVideo(string relativePath)
        {
            try
            {
                var filePath = Path.Combine("wwwroot", relativePath.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "video/mp4", Path.GetFileName(filePath));
                }
                else
                {
                    return NotFound("Video not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteVideo([FromBody] DeleteVideoRequest relativePath)
        {
            try
            {
                string filePath = Path.Combine("wwwroot", relativePath.RelativePath.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok("Video deleted successfully");
                }
                else
                {
                    return NotFound("Video not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public class DeleteVideoRequest
        {
            public string RelativePath { get; set; }
        }
    }
}
