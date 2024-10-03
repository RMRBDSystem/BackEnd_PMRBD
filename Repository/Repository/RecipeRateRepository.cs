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
    public class RecipeRateRepository : IRecipeRateRepository
    {
        public async Task<IEnumerable<RecipeRate>> GetAllRecipeRates() => await RecipeRateDAO.Instance.GetAllRecipeRates();
        public async Task<RecipeRate> GetRecipeRateById(int id) => await RecipeRateDAO.Instance.GetRecipeRateById(id);
        public async Task AddRecipeRate(RecipeRate perrecipe) => await RecipeRateDAO.Instance.Add(perrecipe);
        public async Task UpdateRecipeRate(RecipeRate perrecipe) => await RecipeRateDAO.Instance.Update(perrecipe);
    }
}
