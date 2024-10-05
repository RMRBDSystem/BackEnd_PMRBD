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
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BookOrderDetail>>> GetBookOrderDetailsByOrderId([FromODataUri] int id)
        {
            var list = await bookOrderDetailRepository.GetBookOrderDetailByOrderId(id);
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{OrderId}/{BookId}")]
        public async Task<ActionResult<BookOrderDetail>> GetBookOrderDetailByOrderIdAndBookId([FromODataUri] int OrderId, [FromODataUri] int BookId)
        {
            var bookOrderDetail = await bookOrderDetailRepository.GetBookOrderDetailByOrderIdAndBookId(OrderId, BookId);
            return Ok(bookOrderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<BookOrderDetail>> AddBookOrderDetail([FromBody] BookOrderDetail bookorderdetail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await bookOrderDetailRepository.AddBookOrderDetail(bookorderdetail);
                return Created(bookorderdetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{OrderId}/{BookId}")]
        public async Task<ActionResult<BookOrderDetail>> UpdateBookOrderDetail([FromODataUri] int OrderId, [FromODataUri] int Bookid, [FromBody] BookOrderDetail bookOrderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookOrderDetailToUpdate = await bookOrderDetailRepository.GetBookOrderDetailByOrderIdAndBookId(OrderId, Bookid);
            if (bookOrderDetail == null)
            {
                return NotFound();
            }
            bookOrderDetail.OrderId = OrderId;
            bookOrderDetail.BookId = Bookid;
            await bookOrderDetailRepository.UpdateBookOrderDetail(bookOrderDetail);
            return Updated(bookOrderDetail);
        }

        [HttpDelete("{OrderId}/{BookId}")]
        public async Task DeleteBookOrderDetail([FromODataUri] int OrderId, [FromODataUri] int BookId)
        {
            await bookOrderDetailRepository.DeleteBookOrderDetail(OrderId, BookId);
        }
    }
}
