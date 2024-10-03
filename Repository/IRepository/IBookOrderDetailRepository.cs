using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookOrderDetailRepository
    {
        Task<IEnumerable<BookOrderDetail>> GetBookOrderDetailByOrderId(int orderId);
        Task AddBookOrderDetail(BookOrderDetail bookOrderDetail);
        Task DeleteBookOrderDetail(int bookId, int orderId);
        Task UpdateBookOrderDetail(BookOrderDetail bookOrderDetail);
    }
}
