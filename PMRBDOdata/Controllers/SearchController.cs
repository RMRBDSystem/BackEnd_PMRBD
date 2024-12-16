using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using System.Text;

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
                var query = _context.Recipes.Where(x => x.Status == 1 && x.RecipeName.Contains(searchString) || x.Ingredient.Contains(searchString)).Include(x => x.Images);

                var list = query.ToList();
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
                var query = _context.Books.Where(x => x.Status == 1 && x.BookName.Contains(searchString) || x.Author.Contains(searchString)).Include(x => x.Images);

                var list = await query.ToListAsync();
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
                var query = _context.Ebooks.Where(x => x.Status == 1 && x.EbookName.Contains(searchString) || x.Author.Contains(searchString));

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
