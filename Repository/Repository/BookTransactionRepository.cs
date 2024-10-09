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
    public class BookTransactionRepository : IBookTransactionRepository
    {
        public async Task<IEnumerable<BookTransaction>> GetAllBookTransactions() => await BookTransactionDAO.Instance.GetAllBookTransactions();
        public async Task<IEnumerable<BookTransaction>> GetBookTransactionsByCustomerId(int id) => await BookTransactionDAO.Instance.GetBookTransactionsByCustomerId(id);
        public async Task<BookTransaction> GetBookTransactionById(int id) => await BookTransactionDAO.Instance.GetBookTransactionById(id);
        public async Task AddBookTransaction(BookTransaction ct) => await BookTransactionDAO.Instance.AddBookTransaction(ct);
        public async Task UpdateBookTransaction(BookTransaction ct) => await BookTransactionDAO.Instance.UpdateBookTransaction(ct);
    }
}
