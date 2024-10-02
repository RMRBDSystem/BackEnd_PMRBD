using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BookCategoryDAO : SingletonBase<BookCategoryDAO>
    {
        public async Task<IEnumerable<BookCategory>> GetAllBookCategories()
        {
            try
            {
                return await _context.BookCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book categories", ex);
            }
        }

        public async Task<BookCategory?> GetBookCategoryById(int id)
        {
            try
            {
                return await _context.BookCategories.FirstOrDefaultAsync(x => x.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book category by id", ex);
            }
        }

        public async Task SaveBookCategory(BookCategory bookCategory)
        {
            try
            {
                if (bookCategory != null)
                {
                    await _context.BookCategories.AddAsync(bookCategory);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book category", ex);
            }
        }

        public async Task UpdateBookCategory(BookCategory bookCategory)
        {
            try
            {
                _context.Entry(bookCategory).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book category", ex);
            }
        }
    }
}
