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
    [Route("odata/BookOrder")]
    [ApiController]
    public class BookOrderController : ODataController
    {
        private readonly IBookOrderRepository bookOrderRepository;
        public BookOrderController()
        {
            bookOrderRepository = new BookOrderRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookOrder>>> GetAllBookOrders()
        {
            var list = await bookOrderRepository.GetAllBookOrders();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookOrder>> GetBookOrderById([FromODataUri] int id)
        {
            var bookOrder = await bookOrderRepository.GetBookOrderById(id);
            if (bookOrder == null)
            {
                return NotFound();
            }
            return Ok(bookOrder);
        }

        [HttpPost]
        public async Task AddBookOrder([FromBody] BookOrder bookorder)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await bookOrderRepository.AddBookOrder(bookorder);
                // return Created(bookorder);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBookOrder([FromODataUri] int id, [FromBody] BookOrder bookorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookOrderToUpdate = await bookOrderRepository.GetBookOrderById(id);
            if (bookOrderToUpdate == null)
            {
                return NotFound();
            }
            bookorder.OrderId = bookOrderToUpdate.OrderId;
            await bookOrderRepository.UpdateBookOrder(bookorder);
            return Updated(bookorder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookOrder([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bookOrderToDelete = await bookOrderRepository.GetBookOrderById(id);
            if (bookOrderToDelete == null)
            {
                return NotFound();
            }
            await bookOrderRepository.DeleteBookOrder(bookOrderToDelete.OrderId);
            return NoContent();
        }
    }
}
