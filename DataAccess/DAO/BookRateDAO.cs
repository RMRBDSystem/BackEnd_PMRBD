using BusinessObject.Models;
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
        public async Task<IEnumerable<BookRate?>> GetAllBookRateByBookId(int id)
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

        public async Task SaveBookRate(BookRate bookrate)
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
                if (bookrate != null)
                {
                    _context.Entry(bookrate).State = EntityState.Modified;
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
