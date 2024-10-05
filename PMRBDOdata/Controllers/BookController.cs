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
    [Route("odata/Book")]
    [ApiController]
    public class BookController : ODataController
    {
        private readonly IBookRepository bookRepository;
        public BookController()
        {
            bookRepository = new BookRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var list = await bookRepository.GetAllBooks();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById([FromODataUri] int id)
        {
            var book = await bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await bookRepository.AddBook(book);
                return Created(book);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook([FromODataUri]int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookToUpdate = await bookRepository.GetBookById(id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            book.BookId = bookToUpdate.BookId;
            await bookRepository.UpdateBook(book);
            return Updated(book);
        }
    }
}
