using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookRateDAO : SingletonBase<BookRateDAO>
    {
        public async Task<IEnumerable<BookRate>> GetAllBookRates()
        {
            try
            {
                return await _context.BookRates.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book rates", ex);
            }
        }

        public async Task<IEnumerable<BookRate>> GetAllBookRatesByBookId(int id)
        {
            try
            {
                return await _context.BookRates.Where(x => x.BookId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book rates by book id", ex);
            }
        }

        public async Task<BookRate?> GetBookRateByCustomerIdAndBookId(int CustomerId, int BookId)
        {
            try
            {
                return await _context.BookRates.Where(x => x.CustomerId == CustomerId && x.BookId == BookId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book rate by customer id and book id", ex);
            }
        }

        public async Task AddBookRate(BookRate bookrate)
        {
            try
            {
                if (bookrate != null)
                {
                    await _context.BookRates.AddAsync(bookrate);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book rate", ex);
            }
        }

        public async Task UpdateBookRate(BookRate bookrate)
        {
            try
            {
                var existingItem = await GetBookRateByCustomerIdAndBookId(bookrate.CustomerId, bookrate.BookId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(bookrate);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book rate", ex);
            }
        }
    }
}
