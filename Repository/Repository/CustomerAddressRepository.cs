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
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        public async Task<IEnumerable<CustomerAddress>> GetAllCustomerAddress() => await CustomerAddressDAO.Instance.GetAllCustomerAddress();
        public async Task<CustomerAddress> GetCustomerAddressById(int id) => await CustomerAddressDAO.Instance.GetCustomerAddressById(id);
        public async Task AddCustomerAddress(CustomerAddress customerAddress) => await CustomerAddressDAO.Instance.AddCustomerAddress(customerAddress);
        public async Task UpdateCustomerAddress(CustomerAddress customerAddress) => await CustomerAddressDAO.Instance.UpdateCustomerAddress(customerAddress);

    }
}
