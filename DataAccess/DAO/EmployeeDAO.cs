using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeDAO : SingletonBase<EmployeeDAO>
    {
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employees", ex);
            }
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            try
            {
                return await _context.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employee by id", ex);
            }
        }

        public async Task SaveEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    await _context.Employees.AddAsync(employee);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save employee", ex);
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update employee", ex);
            }
        }
    }
}
