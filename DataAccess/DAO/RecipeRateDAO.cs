using BusinessObject.Models;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RecipeRateDAO : SingletonBase<RecipeRateDAO>
    {
        public async Task<IEnumerable<RecipeRate>> GetAllRecipeRates() => await _context.RecipeRates.ToListAsync();

        public async Task<RecipeRate> GetRecipeRateById(int id)
        {
            var reciperate = await _context.RecipeRates
                .Where(c => c.RecipeId == id)
                .FirstOrDefaultAsync();
            if (reciperate == null) return null;
            return reciperate;
        }

        public async Task Add(RecipeRate reciperate)
        {
            _context.RecipeRates.AddAsync(reciperate);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RecipeRate reciperate)
        {
            var existingItem = await GetRecipeRateById(reciperate.RecipeId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(reciperate);
            }
            else
            {
                _context.RecipeRates.Add(reciperate);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var reciperate = await GetRecipeRateById(id);
            if (reciperate != null)
            {
                _context.RecipeRates.Remove(reciperate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
