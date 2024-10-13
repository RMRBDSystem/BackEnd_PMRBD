using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookOrderStatusRepository
    {
        Task<IEnumerable<BookOrderStatus>> GetAllBookOrderStatuses();
        Task<IEnumerable<BookOrderStatus>> GetBookOrderStatusesByBookOrderId(int bookId);
        Task<BookOrderStatus> GetBookOrderStatusById(int id);
        Task AddBookOrderStatus(BookOrderStatus bookOrderStatus);
        Task UpdateBookOrderStatus(BookOrderStatus bookOrderStatus);
    }
}
