using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRecipeTransactionRepository
    {
        Task<IEnumerable<RecipeTransaction>> GetAllRecipeTransactions();
        Task<IEnumerable<RecipeTransaction>> GetRecipeTransactionsByCustomerId(int id);
        Task<RecipeTransaction> GetRecipeTransactionById(int id);
        Task AddRecipeTransaction(RecipeTransaction recipeTransaction);
        Task UpdateRecipeTransaction(RecipeTransaction recipeTransaction);
    }
}
