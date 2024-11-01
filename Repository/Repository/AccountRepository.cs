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
    public class AccountRepository : IAccountRepository
    {
        public async Task<IEnumerable<Account>> GetAllAccounts() => await AccountDAO.Instance.GetAllAccounts(); 
        public async Task<Account> GetAccountById(int id) => await AccountDAO.Instance.GetAccountById(id);
        public async Task AddAccount(Account account) => await AccountDAO.Instance.AddAccount(account);
        public async Task UpdateAccount(Account account) => await AccountDAO.Instance.UpdateAccount(account);
    }
}
