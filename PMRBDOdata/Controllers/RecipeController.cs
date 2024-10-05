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
    [Route("odata/Recipe")]
    [ApiController]
    public class RecipeController : ODataController
    {
        private readonly IRecipeRepository recipeRepository;
        public RecipeController()
        {
            recipeRepository = new RecipeRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
        {
            var list = await recipeRepository.GetAllRecipes();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById([FromODataUri] int id)
        {
            var recipe = await recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody] Recipe recipe)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await recipeRepository.AddRecipe(recipe);
                return Created(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> UpdateRecipe([FromODataUri] int id, [FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var recipeToUpdate = await recipeRepository.GetRecipeById(id);
            if (recipeToUpdate == null)
            {
                return NotFound();
            }
            recipe.RecipeId = recipeToUpdate.RecipeId;
            await recipeRepository.UpdateRecipe(recipe);
            return Updated(recipe);
        }
    }
}
