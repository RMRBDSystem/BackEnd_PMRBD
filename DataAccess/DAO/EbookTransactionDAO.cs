using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class EbookTransactionDAO : SingletonBase<EbookTransactionDAO>
    {
        public async Task<IEnumerable<EbookTransaction>> GetAllEbookTransactions()
        {
            try
            {
                return await _context.EbookTransactions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve ebook transactions", ex);
            }
        }

        public async Task<IEnumerable<EbookTransaction>> GetEbookTransactionsByCustomerId(int id)
        {
            try
            {
                return await _context.EbookTransactions.Where(x => x.CustomerId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve ebook transaction by Customer id", ex);
            }
        }

        public async Task<EbookTransaction?> GetEbookTransactionById(int id)
        {
            try
            {
                return await _context.EbookTransactions.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve ebook transaction by id", ex);
            }
        }

        public async Task AddEbookTransaction(EbookTransaction ebookTransaction)
        {
            try
            {
                if (ebookTransaction != null)
                {
                    await _context.EbookTransactions.AddAsync(ebookTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save ebook transaction", ex);
            }
        }

        public async Task UpdateEbookTransaction(EbookTransaction ebookTransaction)
        {
            try
            {
                var existingItem = await GetEbookTransactionById(ebookTransaction.EbookTransactionId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(ebookTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update ebook transaction", ex);
            }
        }
    }
}
