using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RecipeTagDAO : SingletonBase<RecipeTagDAO>
    {
        public async Task<IEnumerable<RecipeTag>> GetAllRecipeTags()
        {
            try
            {
                return await _context.RecipeTags.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipe tags", ex);
            }
        }

        public async Task<IEnumerable<RecipeTag>> GetRecipeTagsByRecipeId(int id)
        {
            try
            {
                return await _context.RecipeTags.Where(x => x.RecipeId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipe tags by recipe id", ex);
            }
        }

        public async Task<RecipeTag?> GetRecipeTagByRecipeIdAndTagId(int RecipeId, int TagId)
        {
            try
            {
                return await _context.RecipeTags.FirstOrDefaultAsync(x => x.RecipeId == RecipeId && x.TagId == TagId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipe tag by recipe id and tag id", ex);
            }
        }

        public async Task AddRecipeTag(RecipeTag recipeTag)
        {
            try
            {
                await _context.RecipeTags.AddAsync(recipeTag);
                await _context.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                throw new Exception("Failed to add recipe tag", ex);
            }
        }


        public async Task DeleteRecipeTag(int RecipeId, int TagId)
        {
            try
            {
                var recipeTag = await _context.RecipeTags.FirstOrDefaultAsync(x => x.RecipeId == RecipeId && x.TagId == TagId);
                if (recipeTag != null)
                {
                    _context.RecipeTags.Remove(recipeTag);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete recipe tag", ex);
            }
        }
    }
}

