using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountProfileDAO : SingletonBase<AccountProfileDAO>
    {
        public async Task<IEnumerable<AccountProfile>> GetAllAccountProfiles()
        {
            try
            {
                return await _context.AccountProfiles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve AccountProfiles", ex);
            }
        }

        public async Task<AccountProfile?> GetAccountProfileByAccountId(int id)
        {
            try
            {
                return await _context.AccountProfiles.FirstOrDefaultAsync(x => x.AccountId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve AccountProfile by Account id", ex);
            }
        }

        public async Task AddAccountProfile(AccountProfile accountProfile)
        {
            try
            {
                if (accountProfile != null)
                {
                    await _context.AccountProfiles.AddAsync(accountProfile);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book category", ex);
            }
        }


        public async Task UpdateAccountProfile(AccountProfile accountProfile)
        {
            try
            {
                var existingItem = await GetAccountProfileByAccountId(accountProfile.AccountId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(existingItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update AccountProfile", ex);
            }
        }
    }
}
