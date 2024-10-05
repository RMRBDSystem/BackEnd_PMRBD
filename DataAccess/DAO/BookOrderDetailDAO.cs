using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookOrderDetailDAO : SingletonBase<BookOrderDetailDAO>
    {
        public async Task<IEnumerable<BookOrderDetail>> GetBookOrderDetailByOrderId(int id)
        {
            try
            {
                return await _context.BookOrderDetails.Where(x => x.OrderId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order detail by Orderid", ex);
            }
        }

        public async Task<BookOrderDetail> GetBookOrderDetailByOrderIdAndBookId(int OrderId, int BookId)
        {
            if (!await _context.BookOrderDetails.AnyAsync(x => x.BookId == BookId && x.OrderId == OrderId))
            {
                return null;
            }

            try
            {
                return await _context.BookOrderDetails
                    .Where(x => x.BookId == BookId && x.OrderId == OrderId)
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order detail by OrderId and BookId", ex);
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
                _context.Entry(bookorderdetail).State = EntityState.Modified;
                await _context.SaveChangesAsync();
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
                var bookOrderDetailToDelete = await _context.BookOrderDetails
                    .FirstOrDefaultAsync(x => x.BookId == BookId && x.OrderId == OrderId);

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
