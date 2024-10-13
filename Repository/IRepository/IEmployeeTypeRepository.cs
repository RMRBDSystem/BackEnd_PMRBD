using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IEmployeeTypeRepository
    {
        Task<IEnumerable<EmployeeType>> GetAllEmployeeTypes();
        Task<EmployeeType> GetEmployeeTypeById(int id);
        Task AddEmployeeType(EmployeeType emptype);
        Task UpdateEmployeeType(EmployeeType emptype);
    }
}
