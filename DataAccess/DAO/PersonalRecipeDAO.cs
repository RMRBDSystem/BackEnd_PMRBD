﻿using BusinessObject.Models;
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
        public async Task<IEnumerable<PersonalRecipe>> GetAllPersonalRecipes() => await _context.PersonalRecipes.Include(c => c.Recipe).AsNoTracking().ToListAsync();

        public async Task<PersonalRecipe> GetPersonalRecipeByCustomerIdAndRecipeId(int CustomerId, int RecipId)
        {
            var perrecipe = await _context.PersonalRecipes
                 .Where(c => c.RecipeId == RecipId && c.CustomerId == CustomerId).Include(c => c.Recipe).AsNoTracking()
                .FirstOrDefaultAsync();
            if (perrecipe == null) return null;
            return perrecipe;
        }

        public async Task<List<PersonalRecipe>> GetPersonalRecipesByCustomerId(int CustomerId)
        {
            return await _context.PersonalRecipes
                .Where(c => c.CustomerId == CustomerId).Include(c => c.Recipe)
                .ToListAsync();
        }

        public async Task Add(PersonalRecipe perrecipe)
        {
            await _context.PersonalRecipes.AddAsync(perrecipe);
            await _context.SaveChangesAsync();
        }

        public async Task Update(PersonalRecipe perrecipe)
        {
            var existingItem = await _context.PersonalRecipes.FirstOrDefaultAsync(c => c.RecipeId == perrecipe.RecipeId && c.CustomerId == perrecipe.CustomerId);
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
            var perrecipe = await _context.PersonalRecipes.FirstOrDefaultAsync(c => c.RecipeId == RecipeId && c.CustomerId == CustomerId);
            if (perrecipe != null)
            {
                _context.PersonalRecipes.Remove(perrecipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
