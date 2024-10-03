using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookShelfRepository
    {
        Task<IEnumerable<BookShelf>> GetAllBookShelvesByCustomerId(int id);
        Task<BookShelf> GetBookShelfByEBookIdAndCustomerId(int EbookId, int Customerid);
        Task AddBookShelf(BookShelf bookShelf);
        Task UpdateBookShelf(BookShelf bookShelf);
    }
}
