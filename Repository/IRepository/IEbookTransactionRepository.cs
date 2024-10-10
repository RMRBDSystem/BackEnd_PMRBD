using BusinessObject.Models;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IEbookTransactionRepository
    {
        Task<IEnumerable<EbookTransaction>> GetAllEbookTransactions();
        Task<IEnumerable<EbookTransaction>> GetEbookTransactionsByCustomerId(int id);
        Task<EbookTransaction> GetEbookTransactionById(int id);
        Task AddEbookTransaction(EbookTransaction ebookTransaction);
        Task UpdateEbookTransaction(EbookTransaction ebookTransaction);
    }
}
