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
    [Route("odata/CoinTransaction")]
    [ApiController]
    public class CoinTransactionController : ODataController
    {
        private readonly ICoinTransactionRepository coinTransactionRepository;

        public CoinTransactionController()
        {
            coinTransactionRepository = new CoinTransactionRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoinTransaction>>> GetAllCoinTransactions()
        {
            var list = await coinTransactionRepository.GetAllCoinTransactions();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CoinTransaction>>> GetCoinTransactionByCusyomerId([FromODataUri] int id)
        {
            var coinTransaction = await coinTransactionRepository.GetCoinTransactionsByCustomerId(id);
            if (coinTransaction == null)
            {
                return NotFound();
            }
            return Ok(coinTransaction);
        }

        [HttpPost]
        public async Task AddCoinTransaction([FromBody] CoinTransaction coinTransaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await coinTransactionRepository.AddCoinTransaction(coinTransaction);
                //return Created(coinTransaction);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoinTransaction>> UpdateCoinTransaction([FromODataUri] int id, [FromBody] CoinTransaction coinTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var coinTransactionToUpdate = await coinTransactionRepository.GetCoinTransactionById(id);
            if (coinTransactionToUpdate == null)
            {
                return NotFound();
            }
            coinTransaction.CoinTransactionId = coinTransactionToUpdate.CoinTransactionId;
            await coinTransactionRepository.UpdateCoinTransaction(coinTransaction);
            return Updated(coinTransaction);
        }
    }
}
