using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookShelfDAO : SingletonBase<BookShelfDAO>
    {
        public async Task<IEnumerable<BookShelf>> GetAllBookShelfByCustomerId(int id)
        {
            try
            {
                return await _context.BookShelves.Where(x => x.CustomerId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve bookshelfs", ex);
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

        public async Task SaveBookShelf(BookShelf bookshelf)
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
                if (bookshelf != null)
                {
                    _context.Entry(bookshelf).State = EntityState.Modified;
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
