using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TransactionDAO : SingletonBase<TransactionDAO>
    {
        public async Task<IEnumerable<Transaction>> GetAllTransactions() => await _context.Transactions.ToListAsync();

        public async Task<Transaction> GetTransactionById(int id)
        {
            var tran = await _context.Transactions
                .Where(c => c.TransactionId == id)
                .FirstOrDefaultAsync();
            if (tran == null) return null;
            return tran;
        }

        public async Task Add(Transaction tran)
        {
            _context.Transactions.AddAsync(tran);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Transaction tran)
        {
            var existingItem = await GetTransactionById(tran.TransactionId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(tran);
            }
            else
            {
                _context.Transactions.Add(tran);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tran = await GetTransactionById(id);
            if (tran != null)
            {
                _context.Transactions.Remove(tran);
                await _context.SaveChangesAsync();
            }
        }
    }
}
