using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Image")]
    [ApiController]
    public class ImageController : ODataController
    {
        private readonly IImageRepository imageRepository;
        public ImageController()
        {
            imageRepository = new ImageRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAllImages()
        {
            var list = await imageRepository.GetAllImages();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImageById([FromODataUri] int id)
        {
            var image = await imageRepository.GetImageById(id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult<IEnumerable<Image>>> GetImagesByRecipeId([FromRoute] int recipeId)
        {
            var images = await imageRepository.GetImagesByRecipeId(recipeId);

            if (images == null || !images.Any())
            {
                return NotFound();
            }
            return Ok(images); 
        }

        [HttpPost]
        public async Task AddImage([FromBody] Image image)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await imageRepository.AddImage(image);
                //return Created(image);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Image>> UpdateImage([FromODataUri] int id, [FromBody] Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageToUpdate = await imageRepository.GetImageById(id);
            if (imageToUpdate == null)
            {
                return NotFound();
            }
            image.ImageId = imageToUpdate.ImageId;
            await imageRepository.UpdateImage(image);
            return Updated(image);
        }
    }
}
