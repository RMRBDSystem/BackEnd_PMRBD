using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class WithdrawDAO : SingletonBase<WithdrawDAO>
    {
        public async Task<IEnumerable<Withdraw>> GetAllWithdraws() => await _context.Withdraws.ToListAsync();

        public async Task<Withdraw?> GetWithdrawByWithdrawId(int id) => await _context.Withdraws.FirstOrDefaultAsync(x => x.WithdrawId == id);

        public async Task<IEnumerable<Withdraw>> GetWithdrawByAccountId(int id) => await _context.Withdraws.Where(x => x.AccountId == id).ToListAsync();

        public async Task CreateWithdraw(Withdraw withdraw)
        {
            _context.Withdraws.Add(withdraw);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWithdraw(Withdraw withdraw)
        {
            var existingItem = await GetWithdrawByWithdrawId(withdraw.WithdrawId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(withdraw);
            }
            else
            {
                _context.Withdraws.Add(withdraw);
            }
            await _context.SaveChangesAsync();
        }
    }
}
