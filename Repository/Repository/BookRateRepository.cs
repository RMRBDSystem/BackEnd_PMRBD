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
    public class BookRateRepository : IBookRateRepository
    {
        public async Task<IEnumerable<BookRate>> GetAllBookRatesByBookId(int bookId) => await BookRateDAO.Instance.GetAllBookRatesByBookId(bookId);
        public async Task<BookRate> GetBookRateByCustomerIdAndBookId(int BookId, int CustomerId) => await BookRateDAO.Instance.GetBookRateByCustomerIdAndBookId(BookId, CustomerId);
        public async Task AddBookRate(BookRate bookRate) => await BookRateDAO.Instance.AddBookRate(bookRate);
        public async Task UpdateBookRate(BookRate bookRate) => await BookRateDAO.Instance.UpdateBookRate(bookRate);
    }
}
