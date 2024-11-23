using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookOrderDetailDAO : SingletonBase<BookOrderDetailDAO>
    {
        public async Task<IEnumerable<BookOrderDetail>> GetAllBookOrderDetails()
        {
            try
            {
                return await _context.BookOrderDetails.Include(x => x.BookOrder).Include(x => x.Book).ThenInclude(x => x.Images).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order detail", ex);
            }
        }

        public async Task<IEnumerable<BookOrderDetail?>> GetBookOrderDetailByOrderId(int id)
        {
            try
            {
                return await _context.BookOrderDetails.Where(x => x.OrderId == id).Include(x => x.BookOrder).Include(x => x.Book).ThenInclude(x => x.Images).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order detail by Order Id", ex);
            }
        }

        public async Task<BookOrderDetail?> GetBookOrderDetailByOrderIdAndBookId(int orderid, int bookid)
        {
            try
            {
                return await _context.BookOrderDetails
     .Include(x => x.BookOrder)
     .Include(x => x.Book)
     .ThenInclude(x => x.Images)
     .FirstOrDefaultAsync(x => x.OrderId == orderid && x.BookId == bookid);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order detail by Order Id and Book Id", ex);
            }
        }

        public async Task AddBookOrderDetail(BookOrderDetail bookorderdetail)
        {
            try
            {
                if (bookorderdetail != null)
                {
                    await _context.BookOrderDetails.AddAsync(bookorderdetail);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book order detail", ex);
            }
        }

        public async Task UpdateBookOrderDetail(BookOrderDetail bookorderdetail)
        {
            try
            {
                var existingItem = await GetBookOrderDetailByOrderIdAndBookId(bookorderdetail.OrderId, bookorderdetail.BookId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(bookorderdetail);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book order detail", ex);
            }
        }

        public async Task DeleteBookOrderDetail(int OrderId, int BookId)
        {
            try
            {
                var bookOrderDetailToDelete = await GetBookOrderDetailByOrderIdAndBookId(OrderId, BookId);

                if (bookOrderDetailToDelete != null)
                {
                    _context.BookOrderDetails.Remove(bookOrderDetailToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete book order detail", ex);
            }
        }
    }
}
