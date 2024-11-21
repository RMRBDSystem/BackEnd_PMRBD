using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RecipeDAO : SingletonBase<RecipeDAO>
    {
        public async Task<IEnumerable<Recipe>> GetAllRecipes() => await _context.Recipes.Include(x => x.Images).Include(x => x.Accounts).Include(c => c.PersonalRecipes).Include(c => c.CreateBy).ToListAsync();

        public async Task<Recipe> GetRecipeById(int id)
        {
            var recipe = await _context.Recipes
                .Where(c => c.RecipeId == id).Include(x => x.RecipeTags).Include(c => c.PersonalRecipes)
                .FirstOrDefaultAsync();
            if (recipe == null) return null;
            return recipe;
        }

        public async Task Add(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Recipe recipe)
        {
            var existingItem = await GetRecipeById(recipe.RecipeId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(recipe);
            }
            else
            {
                _context.Recipes.Add(recipe);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var recipe = await GetRecipeById(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
