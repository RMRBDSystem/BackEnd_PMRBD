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
                var query = _context.Recipes.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    var normalizedSearchString = searchString.Normalize(NormalizationForm.FormD).ToLowerInvariant();

                    query = query.Where(x => x.RecipeName.ToLower().Contains(normalizedSearchString)
                                           || x.Ingredient.ToLower().Contains(searchString));
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
                    var normalizedSearchString = searchString.Normalize(NormalizationForm.FormD).ToLowerInvariant();

                    query = query.Where(x => x.BookName.ToLower().Contains(normalizedSearchString)
                                           || x.Author.ToLower().Contains(normalizedSearchString));
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
                    var normalizedSearchString = searchString.Normalize(NormalizationForm.FormD).ToLowerInvariant();
                    query = query.Where(x => x.EbookName.ToLower().Contains(normalizedSearchString)
                                           || x.Author.ToLower().Contains(normalizedSearchString));
                }

                var list = await query.Where(x => x.Status == 1).ToListAsync();
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
