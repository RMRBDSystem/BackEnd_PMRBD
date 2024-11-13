using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly RmrbdContext _context;
        public SearchController(RmrbdContext context)
        {
            _context = context;
        }

        [HttpGet("Recipe/{searchString}")]
        public async Task<ActionResult<List<Recipe>>> SearchRecipe([FromODataUri] string searchString)
        {
            try
            {
                var list = await _context.Recipes
                    .Where(x => x.RecipeName.Contains(searchString)
                               || x.Description.Contains(searchString)
                               || x.Ingredient.Contains(searchString))
                    .ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Book/{searchString}")]
        public async Task<ActionResult<List<Book>>> SearchBook([FromODataUri] string searchString)
        {
            try
            {
                var list = await _context.Books
                    .Where(x => x.BookName.Contains(searchString)
                               || x.Description.Contains(searchString))
                    .ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Ebook/{searchString}")]
        public async Task<ActionResult<List<Book>>> SearchEbook([FromODataUri] string searchString)
        {
            try
            {
                var list = await _context.Ebooks
                    .Where(x => x.EbookName.Contains(searchString)
                               || x.Description.Contains(searchString))
                    .ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
