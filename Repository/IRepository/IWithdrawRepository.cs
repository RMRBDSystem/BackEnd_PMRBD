using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IWithdrawRepository
    {
        Task<IEnumerable<Withdraw>> GetAllWithdraws();
        Task<Withdraw> GetWithdrawByWithdrawId(int id);
        Task<IEnumerable<Withdraw>> GetWithdrawByAccountId(int id);
        Task AddWithdraw(Withdraw withdraw);
        Task UpdateWithdraw(Withdraw withdraw);
    }
}
