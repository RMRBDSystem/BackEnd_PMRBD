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
    [Route("odata/Tag")]
    [ApiController]
    public class TagController : ODataController
    {
        private readonly ITagRepository tagRepository;
        public TagController()
        {
            tagRepository = new TagRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetAllTags()
        {
            var list = await tagRepository.GetAllTags();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTagById([FromODataUri] int id)
        {
            var tag = await tagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> AddTag([FromBody] Tag tag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await tagRepository.AddTag(tag);
                return Created(tag);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> UpdateTag([FromODataUri] int id, [FromBody] Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tagToUpdate = await tagRepository.GetTagById(id);
            if (tagToUpdate == null)
            {
                return NotFound();
            }
            tag.TagId = tagToUpdate.TagId;
            await tagRepository.UpdateTag(tag);
            return Updated(tag);
        }
    }
}
