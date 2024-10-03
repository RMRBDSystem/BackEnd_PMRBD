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
    public class BookRepository : IBookRepository
    {
        public async Task<IEnumerable<Book>> GetAllBooks() => await BookDAO.Instance.GetAllBooks();
        public async Task<Book> GetBookById(int id) => await BookDAO.Instance.GetBookById(id);
        public async Task AddBook(Book book) => await BookDAO.Instance.AddBook(book);
        public async Task UpdateBook(Book book) => await BookDAO.Instance.UpdateBook(book);
    }
}
