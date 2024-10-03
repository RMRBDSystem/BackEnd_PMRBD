using BusinessObject.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RecipeRepository :IRecipeRepository
    {
        public async Task<IEnumerable<Recipe>> GetAllRecipes() => await RecipeDAO.Instance.GetAllRecipes();
        public async Task<Recipe> GetRecipeById(int id) => await RecipeDAO.Instance.GetRecipeById(id);
        public async Task AddRecipe(Recipe perrecipe) => await RecipeDAO.Instance.Add(perrecipe);
        public async Task UpdateRecipe(Recipe perrecipe) => await RecipeDAO.Instance.Update(perrecipe);
    }
}
