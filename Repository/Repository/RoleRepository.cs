using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public async Task<IEnumerable<Role>> GetAllRoles() => await RoleDAO.Instance.GetAllRole();
        public async Task<Role> GetRoleById(int id) => await RoleDAO.Instance.GetRoleById(id);
        public async Task AddRole(Role role) => await RoleDAO.Instance.AddRole(role);
        public async Task UpdateRole(Role role) => await RoleDAO.Instance.UpdateRole(role);
    }
}
