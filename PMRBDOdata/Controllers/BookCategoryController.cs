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
    [Route("odata/BookCategory")]
    [ApiController]
    public class BookCategoryController : ODataController
    {
        private readonly IBookCategoryRepository bookcategoryRepository;
        public BookCategoryController()
        {
            bookcategoryRepository = new BookCategoryRepository();
        }

        
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<BookCategory>>> GetAllBookCategories()
        {
            var list = await bookcategoryRepository.GetAllBookCategories();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookCategory>> GetBookCategoryById([FromODataUri] int id)
        {
            var book = await bookcategoryRepository.GetBookCategoryById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task AddBook([FromBody] BookCategory bookcategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await bookcategoryRepository.AddBookCategory(bookcategory);
                //return Created(bookcategory);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }
        

        [HttpPut("{id}")]
        public async Task<ActionResult<BookCategory>> UpdateBookCategory([FromODataUri] int id, [FromBody] BookCategory bookcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookToUpdate = await bookcategoryRepository.GetBookCategoryById(id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            bookcategory.CategoryId = bookToUpdate.CategoryId;
            await bookcategoryRepository.UpdateBookCategory(bookcategory);
            return Updated(bookcategory);
        }
    }
}
