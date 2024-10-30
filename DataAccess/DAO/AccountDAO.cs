using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO : SingletonBase<AccountDAO>
    {
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            try
            {
                return await _context.Accounts.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book categories", ex);
            }
        }

        public async Task<Account?> GetAccountById(int id)
        {
            try
            {
                return await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book category by id", ex);
            }
        }

        public async Task AddAccount(Account account)
        {
            try
            {
                if (account != null)
                {
                    await _context.Accounts.AddAsync(account);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book category", ex);
            }
        }


        public async Task UpdateAccount(Account account)
        {
            try
            {
                var existingItem = await GetAccountById(account.AccountId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(existingItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book category", ex);
            }
        }
    }
}
