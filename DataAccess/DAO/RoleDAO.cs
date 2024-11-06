using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoleDAO : SingletonBase<RoleDAO>
    {
        public async Task<IEnumerable<Role>> GetAllRole()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve role", ex);
            }
        }


        public async Task<Role?> GetRoleById(int id)
        {
            try
            {
                return await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve Role by id", ex);
            }
        }

        public async Task AddRole(Role role)
        {
            try
            {
                if (role != null)
                {
                    await _context.Roles.AddAsync(role);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save role", ex);
            }
        }


        public async Task UpdateRole(Role role)
        {
            try
            {
                var existingItem = await GetRoleById(role.RoleId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(role);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update role", ex);
            }
        }
    }
}
