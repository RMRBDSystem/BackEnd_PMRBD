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
    public class AccountProfileRepository : IAccountProfileRepository
    {
        public async Task<IEnumerable<AccountProfile>> GetAllAccountProfiles() => await AccountProfileDAO.Instance.GetAllAccountProfiles();
        public async Task<AccountProfile> GetAccountProfileByAccountId(int accountId) => await AccountProfileDAO.Instance.GetAccountProfileByAccountId(accountId);
        public async Task UpdateAccountProfile(AccountProfile accountProfile) => await AccountProfileDAO.Instance.UpdateAccountProfile(accountProfile);
        public async Task AddAccountProfile(AccountProfile accountProfile) => await AccountProfileDAO.Instance.AddAccountProfile(accountProfile);
    }
}
