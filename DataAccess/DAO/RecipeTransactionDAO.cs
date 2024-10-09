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
    public class RecipeTransactionDAO : SingletonBase<RecipeTransactionDAO>
    {
        public async Task<IEnumerable<RecipeTransaction>> GetAllRecipeTransactions()
        {
            try
            {
                return await _context.RecipeTransactions.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipe transactions", ex);
            }
        }

        public async Task<IEnumerable<RecipeTransaction>> GetRecipeTransactionsByCustomerId(int id)
        {
            try
            {
                return await _context.RecipeTransactions.Where(x => x.CustomerId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipe transaction by Customer id", ex);
            }
        }

        public async Task<RecipeTransaction?> GetRecipeTransactionById(int id)
        {
            try
            {
                return await _context.RecipeTransactions.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipe transaction by id", ex);
            }
        }

        public async Task AddRecipeTransaction(RecipeTransaction recipeTransaction)
        {
            try
            {
                if (recipeTransaction != null)
                {
                    await _context.RecipeTransactions.AddAsync(recipeTransaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save recipe transaction", ex);
            }
        }

        public async Task UpdateRecipeTransaction(RecipeTransaction recipeTransaction)
        {
            try
            {
                _context.Entry(recipeTransaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update recipe transaction", ex);
            }
        }
    }
}
