using BusinessObject.Models;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IBookTransactionRepository
    {
        Task<IEnumerable<BookTransaction>> GetAllBookTransactions();
        Task<IEnumerable<BookTransaction>> GetBookTransactionsByCustomerId(int id);
        Task<BookTransaction> GetBookTransactionById(int id);
        Task AddBookTransaction(BookTransaction bookTransaction);
        Task UpdateBookTransaction(BookTransaction bookTransaction);
    }
}
