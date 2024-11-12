using BusinessObject.Models;
using BussinessObject.Models;
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
        public async Task<RecipeRate> GetRecipeRateById(int recipeid) => await RecipeRateDAO.Instance.GetRecipeRateById(recipeid);
        public async Task<RecipeRate> GetRecipeRateByRecipeIdAccountId(int recipeid, int accountid) => await RecipeRateDAO.Instance.GetRecipeRateByRecipeIdAccountId(recipeid, accountid);
        public async Task AddRecipeRate(RecipeRate perrecipe) => await RecipeRateDAO.Instance.Add(perrecipe);
        public async Task UpdateRecipeRate(RecipeRate perrecipe) => await RecipeRateDAO.Instance.Update(perrecipe);
    }
}
