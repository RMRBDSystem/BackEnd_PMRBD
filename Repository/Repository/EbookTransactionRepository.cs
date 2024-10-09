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
    public class EbookTransactionRepository : IEbookTransactionRepository
    {
        public async Task<IEnumerable<EbookTransaction>> GetAllEbookTransactions() => await EbookTransactionDAO.Instance.GetAllEbookTransactions();
        public async Task<IEnumerable<EbookTransaction>> GetEbookTransactionsByCustomerId(int id) => await EbookTransactionDAO.Instance.GetEbookTransactionsByCustomerId(id);
        public async Task<EbookTransaction> GetEbookTransactionById(int id) => await EbookTransactionDAO.Instance.GetEbookTransactionById(id);
        public async Task AddEbookTransaction(EbookTransaction ct) => await EbookTransactionDAO.Instance.AddEbookTransaction(ct);
        public async Task UpdateEbookTransaction(EbookTransaction ct) => await EbookTransactionDAO.Instance.UpdateEbookTransaction(ct);
    }
}
