using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICoinTransactionRepository
    {
        Task<IEnumerable<CoinTransaction>> GetAllCoinTransactions();    
        Task<IEnumerable<CoinTransaction>> GetCoinTransactionsByCustomerId(int id);
        Task<CoinTransaction> GetCoinTransactionById(int id);
        Task AddCoinTransaction(CoinTransaction coinTransaction);
        Task UpdateCoinTransaction(CoinTransaction coinTransaction);
    }
}
