using BusinessObject.Models;
using BussinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BookOrderStatusRepository : IBookOrderStatusRepository
    {
        public async Task<IEnumerable<BookOrderStatus>> GetAllBookOrderStatuses() => await BookOrderStatusDAO.Instance.GetAllBookOrderStatuses();
        public async Task<IEnumerable<BookOrderStatus>> GetBookOrderStatusesByBookOrderId(int id) => await BookOrderStatusDAO.Instance.GetBookOrderStatusesByBookOrderId(id);
        public async Task<BookOrderStatus> GetBookOrderStatusById(int id) => await BookOrderStatusDAO.Instance.GetBookOrderStatusById(id);
        public async Task AddBookOrderStatus(BookOrderStatus bookOrderStatus) => await BookOrderStatusDAO.Instance.AddBookOrderStatus(bookOrderStatus);
        public async Task UpdateBookOrderStatus(BookOrderStatus bookOrderStatus) => await BookOrderStatusDAO.Instance.UpdateBookOrderStatus(bookOrderStatus);
    }
}
