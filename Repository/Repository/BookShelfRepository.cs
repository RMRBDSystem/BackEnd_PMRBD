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
    public class BookShelfRepository : IBookShelfRepository
    {
        public async Task<IEnumerable<BookShelf>> GetAllBookShelvesByCustomerId(int id) => await BookShelfDAO.Instance.GetAllBookShelvesByCustomerId(id);
        public async Task<BookShelf> GetBookShelfByEBookIdAndCustomerId(int EBookId, int Customerid) => await BookShelfDAO.Instance.GetBookShelfByEBookIdAndCustomerId(EBookId, Customerid);
        public async Task AddBookShelf(BookShelf bookShelf) => await BookShelfDAO.Instance.AddBookShelf(bookShelf);
        public async Task UpdateBookShelf(BookShelf bookShelf) => await BookShelfDAO.Instance.UpdateBookShelf(bookShelf);
    }
}
