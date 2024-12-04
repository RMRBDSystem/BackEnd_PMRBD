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
    public class BookTransactionDAO : SingletonBase<BookTransactionDAO>
    {
        public async Task<IEnumerable<BookTransaction>> GetAllBookTransactions()
        {
            try
            {
                return await _context.BookTransactions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book transactions", ex);
            }
        }

        public async Task<IEnumerable<BookTransaction>> GetBookTransactionsByCustomerId(int id)
        {
            try
            {
                return await _context.BookTransactions.Where(x => x.CustomerId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book transaction by Customer id", ex);
            }
        }

        public async Task<BookTransaction?> GetBookTransactionById(int id)
        {
            try
            {
                return await _context.BookTransactions.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve book transaction by id", ex);
            }
        }

        public async Task AddBookTransaction(BookTransaction bookTransaction)
        {
            try
            {
                if (bookTransaction != null)
                {
                    await _context.BookTransactions.AddAsync(bookTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save book transaction", ex);
            }
        }

        public async Task UpdateBookTransaction(BookTransaction bookTransaction)
        {
            try
            {
                var existingItem = await _context.BookTransactions.FindAsync(bookTransaction.BookTransactionId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(bookTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book transaction", ex);
            }
        }
    }
}
