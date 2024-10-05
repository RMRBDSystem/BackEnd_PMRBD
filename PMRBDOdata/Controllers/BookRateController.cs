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
    [Route("odata/BookRate")]
    [ApiController]
    public class BookRateController : ODataController
    {
        private readonly IBookRateRepository bookRateRepository;
        public BookRateController()
        {
            bookRateRepository = new BookRateRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookRate>>> GetAllBookRates()
        {
            var list = await bookRateRepository.GetAllBookRates();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookRate>> GetBookRateByBookId([FromODataUri] int id)
        {
            var bookRate = await bookRateRepository.GetAllBookRatesByBookId(id);
            if (bookRate == null)
            {
                return NotFound();
            }
            return Ok(bookRate);
        }

        [EnableQuery]
        [HttpGet("{CustomerId}/{BookId}")]
        public async Task<ActionResult<BookRate>> GetBookRateByCustomerIdAndBookId([FromODataUri] int CustomerId, [FromODataUri] int BookId)
        {
            var bookRate = await bookRateRepository.GetBookRateByCustomerIdAndBookId(CustomerId, BookId);
            if (bookRate == null)
            {
                return NotFound();
            }
            return Ok(bookRate);
        }

        [HttpPost]
        public async Task<ActionResult<BookRate>> AddBookRate([FromBody] BookRate bookRate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await bookRateRepository.AddBookRate(bookRate);
                return Created(bookRate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{CustomerId}/{BookId}")]
        public async Task<ActionResult<BookRate>> UpdateBookRate([FromODataUri] int CustomerId, [FromODataUri] int BookId, [FromBody] BookRate bookRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookRateToUpdate = await bookRateRepository.GetBookRateByCustomerIdAndBookId(CustomerId, BookId);
            if (bookRateToUpdate == null)
            {
                return NotFound();
            }
            bookRate.CustomerId = CustomerId;
            bookRate.BookId = BookId;
            await bookRateRepository.UpdateBookRate(bookRate);
            return Updated(bookRate);
        }
    }
}
