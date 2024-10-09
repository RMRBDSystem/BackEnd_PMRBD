using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookOrderStatusDAO : SingletonBase<BookOrderStatusDAO>
    {
        public async Task<IEnumerable<BookOrderStatus>> GetAllBookOrderStatuses()
        {
            try
            {
                return await _context.BookOrderStatuses.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order statuses", ex);
            }
        }

        public async Task<IEnumerable<BookOrderStatus>> GetBookOrderStatusesByBookOrderId(int id)
        {
            try
            {
                return await _context.BookOrderStatuses.Where(x => x.BookOrderId == id).ToListAsync();
            }catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order statuses by book order id", ex);
            }
        }

        public async Task<BookOrderStatus?> GetBookOrderStatusById(int id)
        {
            try
            {
                return await _context.BookOrderStatuses.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book order status by id", ex);
            }
        }

        public async Task AddBookOrderStatus(BookOrderStatus bookOrderStatus)
        {
            try
            {
                if(bookOrderStatus != null)
                {
                    await _context.BookOrderStatuses.AddAsync(bookOrderStatus);
                    await _context.SaveChangesAsync();
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add book order status", ex);
            }
        }

        public async Task UpdateBookOrderStatus(BookOrderStatus bookOrderStatus)
        {
            try
            {
                _context.Entry(bookOrderStatus).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book order status", ex);
            }
        }
    }
}
