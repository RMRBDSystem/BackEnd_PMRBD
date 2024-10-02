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
                throw new Exception("Failed to retrieve book order detail by id", ex);
            }
        }

        public async Task SaveBookOrderDetail(BookOrderDetail bookorderdetail)
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

        public async Task UpdateBook(BookOrderDetail bookorderdetail)
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

        public async Task DeleteBookOrderDetail(BookOrderDetail bookOrderDetail)
        {
            try
            {
                var bookOrderDetailToDelete = await _context.BookOrderDetails
                    .FirstOrDefaultAsync(x => x.BookId == bookOrderDetail.BookId && x.OrderId == bookOrderDetail.OrderId);

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
