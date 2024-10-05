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
    [Route("odata/Transaction")]
    [ApiController]
    public class TransactionController : ODataController
    {
        private readonly ITransactionRepository transactionRepository;
        public TransactionController()
        {
            transactionRepository = new TransactionRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            var list = await transactionRepository.GetAllTransactions();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById([FromODataUri] int id)
        {
            var transaction = await transactionRepository.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction([FromBody] Transaction transaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await transactionRepository.AddTransaction(transaction);
                return Created(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Transaction>> UpdateTransaction([FromODataUri] int id, [FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionToUpdate = await transactionRepository.GetTransactionById(id);
            if (transactionToUpdate == null)
            {
                return NotFound();
            }
            transaction.TransactionId = transactionToUpdate.TransactionId;
            await transactionRepository.UpdateTransaction(transaction);
            return Updated(transaction);
        }
    }
}
