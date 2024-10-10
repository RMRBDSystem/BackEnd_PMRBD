using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CoinTransactionRepository : ICoinTransactionRepository
    {
        public async Task<IEnumerable<CoinTransaction>> GetAllCoinTransactions() => await CoinTransactionDAO.Instance.GetAllCoinTransactions();
        public async Task<IEnumerable<CoinTransaction>> GetCoinTransactionsByCustomerId(int id) => await CoinTransactionDAO.Instance.GetCoinTransactionsByCustomerId(id);
        public async Task<CoinTransaction> GetCoinTransactionById(int id) => await CoinTransactionDAO.Instance.GetCoinTransactionById(id);
        public async Task AddCoinTransaction(CoinTransaction ct) => await CoinTransactionDAO.Instance.AddCoinTransaction(ct);
        public async Task UpdateCoinTransaction(CoinTransaction ct) => await CoinTransactionDAO.Instance.UpdateCoinTransaction(ct);
    }
}
