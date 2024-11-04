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
    [Route("odata/Ebook")]
    [ApiController]
    public class EbookController : ODataController
    {
        private readonly IEbookRepository ebookRepository;
        public EbookController()
        {
            ebookRepository = new EbookRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ebook>>> GetAllEbooks()
        {
            var list = await ebookRepository.GetAllEbooks();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ebook>> GetEbookById([FromODataUri] int id)
        {
            var ebook = await ebookRepository.GetEbookById(id);
            if (ebook == null)
            {
                return NotFound();
            }
            return Ok(ebook);
        }

        [HttpPost]
        public async Task<IActionResult> AddEbook([FromBody] Ebook ebook)
        { 

            try
            {
                if (!ModelState.IsValid)
                {
                     BadRequest(ModelState);
                }
                await ebookRepository.AddEbook(ebook);
                var ebookId = ebook.EbookId;
                return CreatedAtAction(nameof(GetEbookById), new { id = ebookId }, ebook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ebook>> UpdateEbook([FromODataUri] int id, [FromBody] Ebook ebook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ebookToUpdate = await ebookRepository.GetEbookById(id);
            if (ebookToUpdate == null)
            {
                return NotFound();
            }
            ebook.EbookId = ebookToUpdate.EbookId;
            await ebookRepository.UpdateEbook(ebook);
            return Updated(ebook);
        }
    }
}
