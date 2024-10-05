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
    public class BookOrderDetailRepository : IBookOrderDetailRepository
    {
        public async Task<IEnumerable<BookOrderDetail>> GetBookOrderDetailByOrderId(int id) => await BookOrderDetailDAO.Instance.GetBookOrderDetailByOrderId(id);
        public async Task<BookOrderDetail> GetBookOrderDetailByOrderIdAndBookId(int Orderid, int Bookid) => await BookOrderDetailDAO.Instance.GetBookOrderDetailByOrderIdAndBookId(Orderid, Bookid);
        public async Task AddBookOrderDetail(BookOrderDetail bookOrderDetail) => await BookOrderDetailDAO.Instance.AddBookOrderDetail(bookOrderDetail);
        public async Task UpdateBookOrderDetail(BookOrderDetail bookOrderDetail) => await BookOrderDetailDAO.Instance.UpdateBookOrderDetail(bookOrderDetail);
        public async Task DeleteBookOrderDetail(int Bookid, int Orderid) => await BookOrderDetailDAO.Instance.DeleteBookOrderDetail(Bookid, Orderid);
    }
}
