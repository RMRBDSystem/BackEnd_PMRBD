﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IPersonalRecipeRepository
    {
        Task<IEnumerable<PersonalRecipe>> GetAllPersonalRecipes();
        Task<PersonalRecipe> GetPersonalRecipeById(int id);
        Task AddPersonalRecipe(PersonalRecipe perrecipe);
        Task UpdatePersonalRecipe(PersonalRecipe perrecipe);
    }
}