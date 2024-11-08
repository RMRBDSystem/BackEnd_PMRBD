using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookDAO : SingletonBase<BookDAO>
    {
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                return await _context.Books.Include(b => b.Images).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve books", ex);
            }
        }

        public async Task<Book?> GetBookById(int id)
        {
            try
            {
                return await _context.Books.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book by id", ex);
            }
        }

        public async Task AddBook(Book book)
        {
            try
            {
                if (book != null)
                {
                    await _context.Books.AddAsync(book);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book", ex);
            }
        }
       
        public async Task UpdateBook(Book book)
        {
            try
            {
                var existingItem = await GetBookById(book.BookId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(book);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book", ex);
            }
        }
    }
}
