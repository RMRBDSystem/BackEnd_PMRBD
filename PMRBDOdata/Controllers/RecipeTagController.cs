using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/RecipeTag")]
    [ApiController]
    public class RecipeTagController : ODataController
    {
        private readonly IRecipeTagRepository _recipeTagRepository;

        public RecipeTagController()
        {
            _recipeTagRepository = new RecipeTagRepository();
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<RecipeTag>>> GetAllRecipeTags()
        {
            var recipeTags = await _recipeTagRepository.GetAllRecipeTags();
            return Ok(recipeTags);
        }

        [HttpGet("{id}")]
        [EnableQuery]

        public async Task<ActionResult<IEnumerable<RecipeTag>>> GetRecipeTagByRecipeId([FromODataUri] int id)
        {
            var recipeTag = await _recipeTagRepository.GetRecipeTagsByRecipeId(id);
            return Ok(recipeTag);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeTag>> AddRecipeTag([FromBody] RecipeTag recipeTag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _recipeTagRepository.AddRecipeTag(recipeTag);
                return Created(recipeTag);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{recipeId}/{tagId}")]
        public async Task<ActionResult> DeleteRecipeTag([FromODataUri] int recipeId, [FromODataUri] int tagId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var recipeTagToDelete = await _recipeTagRepository.GetRecipeTagByRecipeIdAndTagId(recipeId, tagId);
                if (recipeTagToDelete != null)
                {
                    await _recipeTagRepository.DeleteRecipeTag(recipeId, tagId);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
