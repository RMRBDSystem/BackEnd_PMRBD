using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IAccountProfileRepository
    {
        Task<IEnumerable<AccountProfile>> GetAllAccountProfiles(); 
        Task<AccountProfile> GetAccountProfileByAccountId(int id);
        Task AddAccountProfile(AccountProfile accountProfile);
        Task UpdateAccountProfile(AccountProfile accountProfile);
        Task DeleteAccountProfile(int id);
    }
}
