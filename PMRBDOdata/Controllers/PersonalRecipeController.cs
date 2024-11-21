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
    [Route("odata/PersonalRecipe")]
    [ApiController]
    public class PersonalRecipeController : ODataController
    {
        private readonly IPersonalRecipeRepository personalRecipeRepository;
        public PersonalRecipeController()
        {
            personalRecipeRepository = new PersonalRecipeRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalRecipe>>> GetAllPersonalRecipes()
        {
            var list = await personalRecipeRepository.GetAllPersonalRecipes();
            return Ok(list);
        }

        [HttpGet("{CustomerId}")]
        public async Task<ActionResult<IEnumerable<PersonalRecipe>>> GetPersonalRecipesByCustomerId([FromODataUri] int CustomerId)
        {
            try
            {
                var personalRecipes = await personalRecipeRepository.GetPersonalRecipesByCustomerId(CustomerId);

                if (personalRecipes == null || personalRecipes.Count == 0)
                {
                    return NotFound(new { Message = "No personal recipes found for the given CustomerId." });
                }

                return Ok(personalRecipes);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, new { Message = "An error occurred while fetching the data.", Error = ex.Message });
            }
        }

        [HttpGet("{CustomerId}/{RecipeId}")]
        public async Task<ActionResult<PersonalRecipe>> GetPersonalRecipeById([FromODataUri] int CustomerId, [FromODataUri] int RecipeId)
        {
            var personalRecipe = await personalRecipeRepository.GetPersonalRecipeByCustomerIdAndRecipeId(CustomerId, RecipeId);
            if (personalRecipe == null)
            {
                return NotFound();
            }
            return Ok(personalRecipe);
        }

        [HttpPost]
        public async Task AddPersonalRecipe([FromBody] PersonalRecipe personalRecipe)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await personalRecipeRepository.AddPersonalRecipe(personalRecipe);
                //return Created(personalRecipe);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{CustomerId}/{RecipeId}")]
        public async Task<ActionResult<PersonalRecipe>> UpdatePersonalRecipe([FromODataUri] int CustomerId, [FromODataUri] int RecipeId, [FromBody] PersonalRecipe personalRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var personalRecipeToUpdate = await personalRecipeRepository.GetPersonalRecipeByCustomerIdAndRecipeId(CustomerId, RecipeId);
            if (personalRecipeToUpdate == null)
            {
                return NotFound();
            }
            personalRecipe.RecipeId = personalRecipeToUpdate.RecipeId;
            await personalRecipeRepository.UpdatePersonalRecipe(personalRecipe);
            return Updated(personalRecipe);
        }
    }
}
