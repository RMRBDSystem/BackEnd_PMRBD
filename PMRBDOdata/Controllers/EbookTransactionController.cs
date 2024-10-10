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
    [Route("odata/EbookTransaction")]
    [ApiController]
    public class EbookTransactionController : ODataController
    {
        private readonly IEbookTransactionRepository ebookTransactionRepository;

        public EbookTransactionController()
        {
            ebookTransactionRepository = new EbookTransactionRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EbookTransaction>>> GetAllEbookTransactions()
        {
            var list = await ebookTransactionRepository.GetAllEbookTransactions();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<EbookTransaction>>> GetEbookTransactionByCusyomerId([FromODataUri] int id)
        {
            var ebookTransaction = await ebookTransactionRepository.GetEbookTransactionsByCustomerId(id);
            if (ebookTransaction == null)
            {
                return NotFound();
            }
            return Ok(ebookTransaction);
        }

        [HttpPost]
        public async Task<ActionResult<EbookTransaction>> AddEbookTransaction([FromBody] EbookTransaction ebookTransaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await ebookTransactionRepository.AddEbookTransaction(ebookTransaction);
                return Created(ebookTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EbookTransaction>> UpdateEbookTransaction([FromODataUri] int id, [FromBody] EbookTransaction ebookTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ebookTransactionToUpdate = await ebookTransactionRepository.GetEbookTransactionById(id);
            if (ebookTransactionToUpdate == null)
            {
                return NotFound();
            }
            ebookTransaction.EbookTransactionId = ebookTransactionToUpdate.EbookTransactionId;
            await ebookTransactionRepository.UpdateEbookTransaction(ebookTransaction);
            return Updated(ebookTransaction);
        }
    }
}
