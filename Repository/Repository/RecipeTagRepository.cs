using BussinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RecipeTagRepository : IRecipeTagRepository
    {
        public async Task<IEnumerable<RecipeTag>> GetAllRecipeTags() => await RecipeTagDAO.Instance.GetAllRecipeTags();
        public async Task<IEnumerable<RecipeTag>> GetRecipeTagsByRecipeId(int id) => await RecipeTagDAO.Instance.GetRecipeTagsByRecipeId(id);
        public async Task<RecipeTag> GetRecipeTagByRecipeIdAndTagId(int recipeId, int tagId) => await RecipeTagDAO.Instance.GetRecipeTagByRecipeIdAndTagId(recipeId, tagId);
        public async Task AddRecipeTag(RecipeTag recipeTag) => await RecipeTagDAO.Instance.AddRecipeTag(recipeTag);
        public async Task DeleteRecipeTag(int recipeId, int tagId) => await RecipeTagDAO.Instance.DeleteRecipeTag(recipeId, tagId);
    }
}
