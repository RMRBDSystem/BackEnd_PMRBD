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
