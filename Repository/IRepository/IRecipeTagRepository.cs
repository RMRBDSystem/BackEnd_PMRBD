using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRecipeTagRepository
    {
        Task<IEnumerable<RecipeTag>> GetAllRecipeTags();
        Task<IEnumerable<RecipeTag>> GetRecipeTagsByRecipeId(int id);
        Task<RecipeTag?> GetRecipeTagByRecipeIdAndTagId(int recipeId, int tagId);
        Task AddRecipeTag(RecipeTag recipeTag);
        Task DeleteRecipeTag(int recipeId, int tagId);
    }
}
