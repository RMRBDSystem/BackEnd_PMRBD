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
    public class TransactionRepository :ITransactionRepository
    {
        public async Task<IEnumerable<Transaction>> GetAllTransactions() => await TransactionDAO.Instance.GetAllTransactions();
        public async Task<Transaction> GetTransactionById(int id) => await TransactionDAO.Instance.GetTransactionById(id);
        public async Task AddTransaction(Transaction transaction) => await TransactionDAO.Instance.Add(transaction);
        public async Task UpdateTransaction(Transaction transaction) => await TransactionDAO.Instance.Update(transaction);
    }
}
