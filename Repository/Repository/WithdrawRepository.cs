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
    public class WithdrawRepository : IWithdrawRepository
    {
        public async Task<IEnumerable<Withdraw>> GetAllWithdraws() => await  WithdrawDAO.Instance.GetAllWithdraws();
        public async Task<Withdraw?> GetWithdrawByWithdrawId(int id) => await WithdrawDAO.Instance.GetWithdrawByWithdrawId(id);
        public async Task<IEnumerable<Withdraw>> GetWithdrawByAccountId(int id) => await WithdrawDAO.Instance.GetWithdrawByAccountId(id);
        public async Task AddWithdraw(Withdraw withdraw) => await WithdrawDAO.Instance.CreateWithdraw(withdraw);
        public async Task UpdateWithdraw(Withdraw withdraw) => await WithdrawDAO.Instance.UpdateWithdraw(withdraw);

    }
}
