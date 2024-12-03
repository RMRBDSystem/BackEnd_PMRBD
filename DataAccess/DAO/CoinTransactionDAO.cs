using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CoinTransactionDAO : SingletonBase<CoinTransactionDAO>
    {
        public async Task<IEnumerable<CoinTransaction>> GetAllCoinTransactions()
        {
            try
            {
                return await _context.CoinTransactions.Include(x => x.Customer).ThenInclude(x => x.AccountProfile).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve coin transactions", ex);
            }
        }

        public async Task<IEnumerable<CoinTransaction>> GetCoinTransactionsByCustomerId(int id)
        {
            try
            {
                return await _context.CoinTransactions.Include(x => x.Customer).ThenInclude(x => x.AccountProfile).AsNoTracking().Where(x => x.CustomerId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve coin transaction by Customer id", ex);
            }
        }

        public async Task<CoinTransaction?> GetCoinTransactionById(int id)
        {
            try
            {
                return await _context.CoinTransactions.Include(x => x.Customer).ThenInclude(x => x.AccountProfile).AsNoTracking().FirstOrDefaultAsync(x => x.CoinTransactionId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve coin transaction by id", ex);
            }
        }

        public async Task AddCoinTransaction(CoinTransaction coinTransaction)
        {
            try
            {
                if (coinTransaction != null)
                {
                    await _context.CoinTransactions.AddAsync(coinTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save coin transaction", ex);
            }
        }

        public async Task UpdateCoinTransaction(CoinTransaction coinTransaction)
        {
            try
            {
                var existingItem = await _context.CoinTransactions.FindAsync(coinTransaction.CoinTransactionId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(coinTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update coin transaction", ex);
            }
        }
    }
}
