using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookCategoryRepository
    {
        Task<IEnumerable<BookCategory>> GetAllBookCategories();
        Task<BookCategory> GetBookCategoryById(int id);
        Task AddBookCategory(BookCategory bookCategory);
        Task UpdateBookCategory(BookCategory bookCategory);
    }
}
