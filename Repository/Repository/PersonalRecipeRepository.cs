using BusinessObject.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PersonalRecipeRepository :IPersonalRecipeRepository
    {
        public async Task<IEnumerable<PersonalRecipe>> GetAllPersonalRecipes() => await PersonalRecipeDAO.Instance.GetAllPersonalRecipes();
        public async Task<List<PersonalRecipe>> GetPersonalRecipesByCustomerId(int customerId) => await PersonalRecipeDAO.Instance.GetPersonalRecipesByCustomerId(customerId);
        public async Task<PersonalRecipe> GetPersonalRecipeByCustomerIdAndRecipeId(int customerId, int recipeId) => await PersonalRecipeDAO.Instance.GetPersonalRecipeByCustomerIdAndRecipeId(customerId, recipeId);
        
        public async Task AddPersonalRecipe(PersonalRecipe perrecipe) => await PersonalRecipeDAO.Instance.Add(perrecipe);
        public async Task UpdatePersonalRecipe(PersonalRecipe perrecipe) => await PersonalRecipeDAO.Instance.Update(perrecipe);
    }
}
