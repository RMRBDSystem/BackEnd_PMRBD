using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookOrderDAO : SingletonBase<BookOrderDAO>
    {
        public async Task<IEnumerable<BookOrder>> GetAllBookOrders()
        {
            try
            {
                return await _context.BookOrders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book orders", ex);
            }
        }

        public async Task<BookOrder?> GetBookOrderById(int id)
        {
            try
            {
                return await _context.BookOrders.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book by id", ex);
            }
        }

        public async Task AddBookOrder(BookOrder bookorder)
        {
            try
            {
                if (bookorder != null)
                {
                    await _context.BookOrders.AddAsync(bookorder);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book", ex);
            }
        }

        public async Task UpdateBookOrder(BookOrder bookorder)
        {
            try
            {
                _context.Entry(bookorder).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book", ex);
            }
        }

        public async Task DeleteBookOrder(int id)
        {
            try
            {
                var bookOrderToDelete = await _context.BookOrders.FindAsync(id);

                if (bookOrderToDelete != null)
                {
                    _context.BookOrders.Remove(bookOrderToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete book order", ex);
            }
        }
    }
}
