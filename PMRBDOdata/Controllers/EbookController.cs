using BusinessObject.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;
using System.Runtime.InteropServices;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Ebook")]
    [ApiController]
    public class EbookController : ODataController
    {
        private readonly IEbookRepository ebookRepository;
        private readonly IConfiguration _configuration;

        public EbookController(IConfiguration configuration)
        {
            ebookRepository = new EbookRepository();
            _configuration = configuration;
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

        [HttpPut("{ebookId}")]
        public async Task<IActionResult> UpdateEBook([FromBody] Ebook ebook, [FromODataUri] int ebookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ebookToUpdate = await ebookRepository.GetEbookById(ebookId);
            if (ebookToUpdate == null)
            {
                return NotFound();
            }
            ebook.EbookId = ebookToUpdate.EbookId;
            await ebookRepository.UpdateEbook(ebook);
            return Ok();

        }


    }
}
