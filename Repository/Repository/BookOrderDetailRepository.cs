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
    public class BookOrderDetailRepository : IBookOrderDetailRepository
    {
        public async Task<IEnumerable<BookOrderDetail>> GetAllBookOrderDetails() => await BookOrderDetailDAO.Instance.GetAllBookOrderDetails();

        public async Task<BookOrderDetail> GetBookOrderDetailById(int id) => await BookOrderDetailDAO.Instance.GetBookOrderDetailById(id);

        public async Task<BookOrderDetail> GetBookOrderDetailByOrderIdAndBookId(int OrderId, int BookId) => await BookOrderDetailDAO.Instance.GetBookOrderDetailByOrderIdAndBookId(OrderId, BookId);

        public async Task<IEnumerable<BookOrderDetail>> GetBookOrderDetailByOrderId(int OrderId) => await BookOrderDetailDAO.Instance.GetBookOrderDetailByOrderId(OrderId);

        public async Task AddBookOrderDetail(BookOrderDetail bookOrderDetail) => await BookOrderDetailDAO.Instance.AddBookOrderDetail(bookOrderDetail);

        public async Task UpdateBookOrderDetail(BookOrderDetail bookOrderDetail) => await BookOrderDetailDAO.Instance.UpdateBookOrderDetail(bookOrderDetail);

        public async Task DeleteBookOrderDetail(int OrderId, int BookId) => await BookOrderDetailDAO.Instance.DeleteBookOrderDetail(OrderId, BookId);
    }
}
