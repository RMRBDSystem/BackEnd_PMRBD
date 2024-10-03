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
    public class PersonalRecipeRepository :IPersonalRecipeRepository
    {
        public async Task<IEnumerable<PersonalRecipe>> GetAllPersonalRecipes() => await PersonalRecipeDAO.Instance.GetAllPersonalRecipes();
        public async Task<PersonalRecipe> GetPersonalRecipeById(int id) => await PersonalRecipeDAO.Instance.GetPersonalRecipeById(id);
        public async Task AddPersonalRecipe(PersonalRecipe perrecipe) => await PersonalRecipeDAO.Instance.Add(perrecipe);
        public async Task UpdatePersonalRecipe(PersonalRecipe perrecipe) => await PersonalRecipeDAO.Instance.Update(perrecipe);
    }
}
