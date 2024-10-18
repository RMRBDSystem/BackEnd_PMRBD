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
    [Route("odata/BookTransaction")]
    [ApiController]
    public class BookTransactionController : ODataController
    {
        private readonly IBookTransactionRepository bookTransactionRepository;

        public BookTransactionController()
        {
            bookTransactionRepository = new BookTransactionRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookTransaction>>> GetAllBookTransactions()
        {
            var list = await bookTransactionRepository.GetAllBookTransactions();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BookTransaction>>> GetBookTransactionByCusyomerId([FromODataUri] int id)
        {
            var bookTransaction = await bookTransactionRepository.GetBookTransactionsByCustomerId(id);
            if (bookTransaction == null)
            {
                return NotFound();
            }
            return Ok(bookTransaction);
        }

        [HttpPost]
        public async Task AddBookTransaction([FromBody] BookTransaction bookTransaction)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await bookTransactionRepository.AddBookTransaction(bookTransaction);
                //return Created(bookTransaction);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookTransaction>> UpdateBookTransaction([FromODataUri] int id, [FromBody] BookTransaction bookTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookTransactionToUpdate = await bookTransactionRepository.GetBookTransactionById(id);
            if (bookTransactionToUpdate == null)
            {
                return NotFound();
            }
            bookTransaction.BookTransactionId = bookTransactionToUpdate.BookTransactionId;
            await bookTransactionRepository.UpdateBookTransaction(bookTransaction);
            return Updated(bookTransaction);
        }
    }
}
