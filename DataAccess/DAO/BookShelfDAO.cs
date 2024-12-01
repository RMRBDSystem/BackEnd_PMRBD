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
    public class BookShelfDAO : SingletonBase<BookShelfDAO>
    {
        public async Task<IEnumerable<BookShelf>> GetAllBookShelves()
        {
            try
            {
                return await _context.BookShelves.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve bookshelfs", ex);
            }
        }
        public async Task<IEnumerable<BookShelf>> GetAllBookShelvesByCustomerId(int id)
        {
            try
            {
                return await _context.BookShelves.Where(x => x.CustomerId == id).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve bookshelfs by customer id", ex);
            }
        }

        public async Task<BookShelf?> GetBookShelfByEBookIdAndCustomerId(int EBookid, int Customerid)
        {
            try
            {
                return await _context.BookShelves.FirstOrDefaultAsync(x => x.EbookId == EBookid && x.CustomerId == Customerid);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book by id", ex);
            }
        }

        public async Task AddBookShelf(BookShelf bookshelf)
        {
            try
            {
                if (bookshelf != null)
                {
                    await _context.BookShelves.AddAsync(bookshelf);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save bookshelf", ex);
            }
        }

        public async Task UpdateBookShelf(BookShelf bookshelf)
        {
            try
            {
                var existingItem = await GetBookShelfByEBookIdAndCustomerId(bookshelf.EbookId, bookshelf.CustomerId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(bookshelf);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update bookshelf", ex);
            }
        }
    }
}
