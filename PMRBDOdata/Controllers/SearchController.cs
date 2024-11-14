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

        [HttpGet("Recipe/{searchString?}")]
        public async Task<ActionResult<List<Recipe>>> SearchRecipe([FromODataUri] string? searchString = "")
        {
            try
            {
                var query = _context.Recipes.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    query = query.Where(x => x.RecipeName.Contains(searchString)
                                           || x.Ingredient.Contains(searchString));
                }

                var list = await query.Where(x => x.Status == 1).Include(x=> x.Images).ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Book/{searchString?}")]
        public async Task<ActionResult<List<Book>>> SearchBook([FromODataUri] string? searchString = "")
        {
            try
            {
                var query = _context.Books.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    query = query.Where(x => x.BookName.Contains(searchString)
                                           || x.Author.Contains(searchString));
                }

                var list = await query.Where(x => x.Status == 1).Include(x => x.Images).ToListAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Ebook/{searchString?}")]
        public async Task<ActionResult<List<Recipe>>> SearchEbook([FromODataUri] string? searchString = "")
        {
            try
            {
                var query = _context.Ebooks.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    query = query.Where(x => x.EbookName.Contains(searchString)
                                           || x.Author.Contains(searchString));
                }

                var list = await query.ToListAsync();
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
