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
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Customer> GetCustomerById(int id) => await CustomerDAO.Instance.GetCustomerById(id);
        public async Task<IEnumerable<Customer>> GetAllCustomers() => await CustomerDAO.Instance.GetAllCustomers();
        public async Task AddCustomer(Customer customer) => await CustomerDAO.Instance.AddCustomer(customer);
        public async Task UpdateCustomer(Customer customer) => await CustomerDAO.Instance.UpdateCustomer(customer);
    }
}
