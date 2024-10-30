using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/BookOrderStatus")]
    [ApiController]
    public class BookOrderStatusController : ODataController
    {
        private readonly IBookOrderStatusRepository _bookOrderStatusRepository;

        public BookOrderStatusController()
        {
            _bookOrderStatusRepository = new BookOrderStatusRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOrderStatus>>> GetAllBookOrderStatuses()
        {
            var list = await _bookOrderStatusRepository.GetAllBookOrderStatuses();
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BookOrderStatus>>> GetBookOrderStatusByBookOrderId([FromODataUri] int id)
        {
            var bookOrderStatus = await _bookOrderStatusRepository.GetBookOrderStatusesByBookOrderId(id);
            if (bookOrderStatus == null)
            {
                return NotFound();
            }
            return Ok(bookOrderStatus);
        }

        [HttpPost]
        public async Task AddBookOrderStatus([FromBody] BookOrderStatus bookOrderStatus)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await _bookOrderStatusRepository.AddBookOrderStatus(bookOrderStatus);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookTransaction>> UpdateBookOrderStatus([FromODataUri] int id, [FromBody] BookOrderStatus bookOrderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookOrderStatusToUpdate = await _bookOrderStatusRepository.GetBookOrderStatusById(id);
            if (bookOrderStatusToUpdate == null)
            {
                return NotFound();
            }
            bookOrderStatus.BookOrderStatusId = bookOrderStatusToUpdate.BookOrderStatusId;
            await _bookOrderStatusRepository.UpdateBookOrderStatus(bookOrderStatus);
            return Updated(bookOrderStatus);
        }
    }
}
