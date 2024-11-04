using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CustomerAddressDAO : SingletonBase<CustomerAddressDAO>
    {
        public async Task<IEnumerable<CustomerAddress>> GetAllCustomerAddress()
        {
            try
            {
                return await _context.CustomerAddresses.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve CusomerAddress", ex);
            }
        }


        public async Task<CustomerAddress?> GetCustomerAddressById(int id)
        {
            try
            {
                return await _context.CustomerAddresses.FirstOrDefaultAsync(x => x.AddressId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve cusomer address by id", ex);
            }
        }

        public async Task AddCustomerAddress(CustomerAddress customerAddress)
        {
            try
            {
                if (customerAddress != null)
                {
                    await _context.CustomerAddresses.AddAsync(customerAddress);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save customer address", ex);
            }
        }


        public async Task UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            try
            {
                var existingItem = await GetCustomerAddressById(customerAddress.AddressId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(customerAddress);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update customer address", ex);
            }
        }
    }
}
