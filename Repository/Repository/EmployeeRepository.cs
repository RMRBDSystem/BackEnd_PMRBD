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
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetAllEmployees() => await EmployeeDAO.Instance.GetAllEmployees();
        public async Task<Employee> GetEmployeeById(int id) => await EmployeeDAO.Instance.GetEmployeeById(id);
        public async Task AddEmployee(Employee employee) => await EmployeeDAO.Instance.AddEmployee(employee);
        public async Task UpdateEmployee(Employee employee) => await EmployeeDAO.Instance.UpdateEmployee(employee);
    }
}
