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
    [Route("odata/RecipeTransaction")]
    [ApiController]
    public class RecipeTransactionController : ODataController
    {
        private readonly IRecipeTransactionRepository recipeTransactionRepository;

        public RecipeTransactionController()
        {
            recipeTransactionRepository = new RecipeTransactionRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeTransaction>>> GetAllRecipeTransactions()
        {
            var list = await recipeTransactionRepository.GetAllRecipeTransactions();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RecipeTransaction>>> GetRecipeTransactionByCusyomerId([FromODataUri] int id)
        {
            var recipeTransaction = await recipeTransactionRepository.GetRecipeTransactionsByCustomerId(id);
            if (recipeTransaction == null)
            {
                return NotFound();
            }
            return Ok(recipeTransaction);
        }

        [HttpPost]
        public async Task AddRecipeTransaction([FromBody] RecipeTransaction recipeTransaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await recipeTransactionRepository.AddRecipeTransaction(recipeTransaction);
                //return Created(recipeTransaction);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeTransaction>> UpdateRecipeTransaction([FromODataUri] int id, [FromBody] RecipeTransaction recipeTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var recipeTransactionToUpdate = await recipeTransactionRepository.GetRecipeTransactionById(id);
            if (recipeTransactionToUpdate == null)
            {
                return NotFound();
            }
            recipeTransaction.RecipeTransactionId = recipeTransactionToUpdate.RecipeTransactionId;
            await recipeTransactionRepository.UpdateRecipeTransaction(recipeTransaction);
            return Updated(recipeTransaction);
        }
    }
}
