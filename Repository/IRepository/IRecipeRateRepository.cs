using BusinessObject.Models;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRecipeRateRepository
    {
        Task<IEnumerable<RecipeRate>> GetAllRecipeRates();
        Task<RecipeRate> GetRecipeRateById(int id);
        Task AddRecipeRate(RecipeRate recipeRate);
        Task UpdateRecipeRate(RecipeRate recipeRate);
    }
}
