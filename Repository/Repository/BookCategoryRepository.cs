using BusinessObject.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        public async Task<IEnumerable<BookCategory>> GetAllBookCategories() => await BookCategoryDAO.Instance.GetAllBookCategories();
        public async Task<BookCategory> GetBookCategoryById(int id) => await BookCategoryDAO.Instance.GetBookCategoryById(id);
        public async Task AddBookCategory(BookCategory bookCategory) => await BookCategoryDAO.Instance.AddBookCategory(bookCategory);
        public async Task UpdateBookCategory(BookCategory bookCategory) => await BookCategoryDAO.Instance.UpdateBookCategory(bookCategory);
    }
}
