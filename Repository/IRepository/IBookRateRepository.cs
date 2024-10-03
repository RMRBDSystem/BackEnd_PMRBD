using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookRateRepository
    {
        Task<IEnumerable<BookRate>> GetAllBookRatesByBookId(int bookId);
        Task<BookRate> GetBookRateByCustomerIdAndBookId(int bookId, int userId);
        Task AddBookRate(BookRate bookRate);
        Task UpdateBookRate(BookRate bookRate);
    }
}
