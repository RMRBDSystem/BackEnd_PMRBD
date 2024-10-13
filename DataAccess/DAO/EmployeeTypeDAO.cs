using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeTypeDAO : SingletonBase<EmployeeTypeDAO>
    {
        public async Task<IEnumerable<EmployeeType>> GetAllEmployeeTypes() => await _context.EmployeeTypes.ToListAsync();

        public async Task<EmployeeType> GetEmployeeTypeById(int id)
        {
            var emtype = await _context.EmployeeTypes
                .Where(c => c.EmployeeTypeId == id)
                .FirstOrDefaultAsync();
            if (emtype == null) return null;
            return emtype;
        }

        public async Task Add(EmployeeType emtype)
        {
            _context.EmployeeTypes.AddAsync(emtype);
            await _context.SaveChangesAsync();
        }

        public async Task Update(EmployeeType emtype)
        {
            var existingItem = await GetEmployeeTypeById(emtype.EmployeeTypeId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(emtype);
            }
            else
            {
                _context.EmployeeTypes.Add(emtype);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var emtype = await GetEmployeeTypeById(id);
            if (emtype != null)
            {
                _context.EmployeeTypes.Remove(emtype);
                await _context.SaveChangesAsync();
            }
        }
    }
}
