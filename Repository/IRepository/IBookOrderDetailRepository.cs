using BusinessObject.Models;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookOrderDetailRepository
    {
        Task<IEnumerable<BookOrderDetail>> GetAllBookOrderDetails();
        Task<IEnumerable<BookOrderDetail>> GetBookOrderDetailByOrderId(int OrderId);
        Task<BookOrderDetail> GetBookOrderDetailByOrderIdAndBookId(int OrderId, int Bookid);
        Task AddBookOrderDetail(BookOrderDetail bookOrderDetail);
        Task UpdateBookOrderDetail(BookOrderDetail bookOrderDetail);
        Task DeleteBookOrderDetail(int OrderId, int BookId);
    }
}
