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
    public class EmployeeTypeRepository: IEmployeeTypeRepository
    {
        public async Task<IEnumerable<EmployeeType>> GetAllEmployeeTypes() => await EmployeeTypeDAO.Instance.GetAllEmployeeTypes();
        public async Task<EmployeeType> GetEmployeeTypeById(int id) => await EmployeeTypeDAO.Instance.GetEmployeeTypeById(id);
        public async Task AddEmployeeType(EmployeeType emptype) => await EmployeeTypeDAO.Instance.Add(emptype);
        public async Task UpdateEmployeeType(EmployeeType emptype) => await EmployeeTypeDAO.Instance.Update(emptype);
    }
}
