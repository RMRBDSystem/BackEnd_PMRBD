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
    [Route("odata/Comment")]
    [ApiController]
    public class CommentController : ODataController
    {
        private readonly ICommentRepository commentRepository;
        public CommentController()
        {
            commentRepository = new CommentRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            var list = await commentRepository.GetAllComments();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentById([FromODataUri] int id)
        {
            var comment = await commentRepository.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task AddComment([FromBody] Comment comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await commentRepository.AddComment(comment);
                //return Created(comment);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> UpdateComment([FromODataUri] int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentToUpdate = await commentRepository.GetCommentById(id);
            if (commentToUpdate == null)
            {
                return NotFound();
            }
            comment.CommentId = commentToUpdate.CommentId;
            await commentRepository.UpdateComment(comment);
            return Updated(comment);
        }
    }
}
