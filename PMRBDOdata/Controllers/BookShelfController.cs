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
    [Route("odata/BookShelf")]
    [ApiController]
    public class BookShelfController : ODataController
    {
        private readonly IBookShelfRepository bookShelfRepository;
        public BookShelfController()
        {
            bookShelfRepository = new BookShelfRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookShelf>>> GetAllBookShelves()
        {
            var list = await bookShelfRepository.GetAllBookShelves();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookShelf>> GetAllBookShelvesByCustomerId([FromODataUri] int id)
        {
            var bookShelf = await bookShelfRepository.GetAllBookShelvesByCustomerId(id);
            if (bookShelf == null)
            {
                return NotFound();
            }
            return Ok(bookShelf);
        }


        [HttpGet("{EBookId}/{CustomerId}")]
        public async Task<ActionResult<BookShelf>> GetBookShelfByEBookIdAndCustomerId([FromODataUri] int EbookId,[FromODataUri] int CustomerId, [FromODataUri] int BookId)
        {
            var bookShelf = await bookShelfRepository.GetBookShelfByEBookIdAndCustomerId(EbookId, CustomerId);
            if (bookShelf == null)
            {
                return NotFound();
            }
            return Ok(bookShelf);
        }

        [HttpPost]
        public async Task AddBookShelf([FromBody] BookShelf bookShelf)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await bookShelfRepository.AddBookShelf(bookShelf);
                //return Created(bookShelf);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{EbookId}/{CustomerId}")]
        public async Task<ActionResult<BookShelf>> UpdateBookShelf([FromODataUri] int EbookId,[FromODataUri] int CustomerId, [FromBody] BookShelf bookShelf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookShelfToUpdate = await bookShelfRepository.GetBookShelfByEBookIdAndCustomerId(EbookId, CustomerId);
            if (bookShelfToUpdate == null)
            {
                return NotFound();
            }
            bookShelf.CustomerId = CustomerId;
            bookShelf.EbookId = EbookId;
            await bookShelfRepository.UpdateBookShelf(bookShelf);
            return Updated(bookShelf);
        }
    }
}
