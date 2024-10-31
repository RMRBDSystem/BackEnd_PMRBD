using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICustomerAddressRepository
    {
        Task<IEnumerable<CustomerAddress>> GetAllCustomerAddress();
        Task<CustomerAddress> GetCustomerAddressById(int id);
        Task AddCustomerAddress(CustomerAddress customerAddress);
        Task UpdateCustomerAddress(CustomerAddress customerAddress);
    }
}
