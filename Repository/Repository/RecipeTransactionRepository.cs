using BusinessObject.Models;
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
    public class RecipeTransactionRepository : IRecipeTransactionRepository
    {
        public async Task<IEnumerable<RecipeTransaction>> GetAllRecipeTransactions() => await RecipeTransactionDAO.Instance.GetAllRecipeTransactions();
        public async Task<IEnumerable<RecipeTransaction>> GetRecipeTransactionsByCustomerId(int id) => await RecipeTransactionDAO.Instance.GetRecipeTransactionsByCustomerId(id);
        public async Task<RecipeTransaction> GetRecipeTransactionById(int id) => await RecipeTransactionDAO.Instance.GetRecipeTransactionById(id);
        public async Task AddRecipeTransaction(RecipeTransaction ct) => await RecipeTransactionDAO.Instance.AddRecipeTransaction(ct);
        public async Task UpdateRecipeTransaction(RecipeTransaction ct) => await RecipeTransactionDAO.Instance.UpdateRecipeTransaction(ct);
    }
}
