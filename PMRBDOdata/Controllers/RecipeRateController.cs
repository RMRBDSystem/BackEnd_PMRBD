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
        public async Task<ActionResult<RecipeRate>> GetRecipeRateById([FromODataUri] int recipeId)
        {
            var recipeRate = await recipeRateRepository.GetRecipeRateById(recipeId);
            if (recipeRate == null)
            {
                return NotFound();
            }
            return Ok(recipeRate);
        }
        [HttpGet("{RecipeId}/{AccountId}")]
        public async Task<ActionResult<RecipeRate>> GetRecipeRateByRecipeIdAccountId([FromODataUri] int recipeId, [FromODataUri] int accountId)
        {
            var recipeRate = await recipeRateRepository.GetRecipeRateByRecipeIdAccountId(recipeId, accountId);
            if (recipeRate == null)
            {
                return NotFound();
            }
            return Ok(recipeRate);
        }

        [HttpPost]
        public async Task <ActionResult> AddRecipeRate([FromBody] RecipeRate recipeRate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await recipeRateRepository.AddRecipeRate(recipeRate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{RecipeId}/{AccountId}")]
        public async Task<ActionResult<RecipeRate>> UpdateRecipeRate([FromODataUri] int recipeId, [FromODataUri] int accountId, [FromBody] RecipeRate recipeRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var recipeRateToUpdate = await recipeRateRepository.GetRecipeRateByRecipeIdAccountId(recipeId, accountId);
            if (recipeRateToUpdate == null)
            {
                return NotFound();
            }
            /*recipeRate.RecipeId = recipeRateToUpdate.RecipeId;
            recipeRate.AccountId = recipeRateToUpdate.AccountId;*/
            
            // Cập nhật chỉ những trường cần thiết
            recipeRateToUpdate.RecipeId = recipeId;
            recipeRateToUpdate.AccountId = accountId;
            await recipeRateRepository.UpdateRecipeRate(recipeRate);
            return Updated(recipeRate);
        }
    }
}
