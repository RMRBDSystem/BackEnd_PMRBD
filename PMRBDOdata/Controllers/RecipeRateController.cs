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
    [Route("odata/RecipeRate")]
    [ApiController]
    public class RecipeRateController : ODataController
    {
        private readonly IRecipeRateRepository recipeRateRepository;
        public RecipeRateController()
        {
            recipeRateRepository = new RecipeRateRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeRate>>> GetAllRecipeRates()
        {
            var list = await recipeRateRepository.GetAllRecipeRates();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeRate>> GetRecipeRateById([FromODataUri] int id)
        {
            var recipeRate = await recipeRateRepository.GetRecipeRateById(id);
            if (recipeRate == null)
            {
                return NotFound();
            }
            return Ok(recipeRate);
        }

        [HttpPost]
        public async Task AddRecipeRate([FromBody] RecipeRate recipeRate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await recipeRateRepository.AddRecipeRate(recipeRate);
                //return Created(recipeRate);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeRate>> UpdateRecipeRate([FromODataUri] int id, [FromBody] RecipeRate recipeRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var recipeRateToUpdate = await recipeRateRepository.GetRecipeRateById(id);
            if (recipeRateToUpdate == null)
            {
                return NotFound();
            }
            recipeRate.RecipeId = recipeRateToUpdate.RecipeId;
            await recipeRateRepository.UpdateRecipeRate(recipeRate);
            return Updated(recipeRate);
        }
    }
}
