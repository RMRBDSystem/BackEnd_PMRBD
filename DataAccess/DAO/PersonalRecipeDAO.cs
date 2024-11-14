using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PersonalRecipeDAO : SingletonBase<PersonalRecipeDAO>
    {
        public async Task<IEnumerable<PersonalRecipe>> GetAllPersonalRecipes() => await _context.PersonalRecipes.ToListAsync();

        public async Task<PersonalRecipe> GetPersonalRecipeByCustomerIdAndRecipeId(int CustomerId, int RecipId)
        {
            var perrecipe = await _context.PersonalRecipes
                .Where(c => c.RecipeId == RecipId && c.CustomerId == CustomerId)
                .FirstOrDefaultAsync();
            if (perrecipe == null) return null;
            return perrecipe;
        }

        public async Task Add(PersonalRecipe perrecipe)
        {
            await _context.PersonalRecipes.AddAsync(perrecipe);
            await _context.SaveChangesAsync();
        }

        public async Task Update(PersonalRecipe perrecipe)
        {
            var existingItem = await GetPersonalRecipeByCustomerIdAndRecipeId(perrecipe.CustomerId, perrecipe.RecipeId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(perrecipe);
            }
            else
            {
                _context.PersonalRecipes.Add(perrecipe);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int CustomerId, int RecipeId)
        {
            var perrecipe = await GetPersonalRecipeByCustomerIdAndRecipeId(CustomerId, RecipeId);
            if (perrecipe != null)
            {
                _context.PersonalRecipes.Remove(perrecipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
