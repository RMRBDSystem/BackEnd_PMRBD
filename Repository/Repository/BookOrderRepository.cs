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
    public class BookOrderRepository : IBookOrderRepository
    {
        public async Task<IEnumerable<BookOrder>> GetAllBookOrders() => await BookOrderDAO.Instance.GetAllBookOrders();
        public async Task<BookOrder> GetBookOrderById(int id) => await BookOrderDAO.Instance.GetBookOrderById(id);
        public async Task AddBookOrder(BookOrder bookOrder) => await BookOrderDAO.Instance.AddBookOrder(bookOrder);
        public async Task UpdateBookOrder(BookOrder bookOrder) => await BookOrderDAO.Instance.UpdateBookOrder(bookOrder);
        public async Task DeleteBookOrder(int id) => await BookOrderDAO.Instance.DeleteBookOrder(id);
    }
}
