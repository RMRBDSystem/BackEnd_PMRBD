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
    [Route("odata/BookOrderDetail")]
    [ApiController]
    public class BookOrderDetailController : ODataController
    {
        private readonly IBookOrderDetailRepository bookOrderDetailRepository;
        public BookOrderDetailController()
        {
            bookOrderDetailRepository = new BookOrderDetailRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOrderDetail>>> GetAllBookOrderDetails()
        {
            var list = await bookOrderDetailRepository.GetAllBookOrderDetails();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookOrderDetail>> GetBookOrderDetailByOrderId([FromODataUri] int id)
        {
            var bookOrderDetail = await bookOrderDetailRepository.GetBookOrderDetailByOrderId(id);
            if (bookOrderDetail == null)
            {
                return NotFound();
            }
            return Ok(bookOrderDetail);
        }

        [HttpGet("{OrderId}/{BookId}")]
        public async Task<ActionResult<BookOrderDetail>> GetBookOrderDetailByOrderIdAndBookId([FromODataUri] int OrderId, [FromODataUri] int BookId)
        {
            var bookOrderDetail = await bookOrderDetailRepository.GetBookOrderDetailByOrderIdAndBookId(OrderId, BookId);
            if (bookOrderDetail == null)
            {
                return NotFound();
            }
            return Ok(bookOrderDetail);
        }

        [HttpPost]
        public async Task AddBookOrderDetail([FromBody] BookOrderDetail bookorderdetail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await bookOrderDetailRepository.AddBookOrderDetail(bookorderdetail);
                // return Created(bookorder);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{OrderId}/{BookId}")]
        public async Task<ActionResult<BookOrderDetail>> UpdateBookOrderDetail([FromODataUri] int OrderId, [FromODataUri] int BookId, [FromBody] BookOrderDetail bookorderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookOrderDetailToUpdate = await bookOrderDetailRepository.GetBookOrderDetailByOrderIdAndBookId(OrderId, BookId);
            if (bookOrderDetailToUpdate == null)
            {
                return NotFound();
            }
            bookorderdetail.OrderId = bookOrderDetailToUpdate.OrderId;
            bookorderdetail.BookId = bookOrderDetailToUpdate.BookId;
            await bookOrderDetailRepository.UpdateBookOrderDetail(bookorderdetail);
            return Updated(bookorderdetail);
        }

        [HttpDelete("{OrderId}/{BookId}")]
        public async Task<IActionResult> DeleteBookOrder([FromODataUri] int OrderId, [FromODataUri] int BookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookOrderDetailToDelete = await bookOrderDetailRepository.GetBookOrderDetailByOrderIdAndBookId(OrderId, BookId);
            if (bookOrderDetailToDelete == null)
            {
                return NotFound();
            }
            await bookOrderDetailRepository.DeleteBookOrderDetail(bookOrderDetailToDelete.OrderId, bookOrderDetailToDelete.BookId);
            return NoContent();
        }
    }
}
