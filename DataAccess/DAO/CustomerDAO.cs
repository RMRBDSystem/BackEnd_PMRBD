using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO : SingletonBase<CustomerDAO>
    {
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve customers", ex);
            }
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            try
            {
                return await _context.Customers.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve Customer by id", ex);
            }
        }

        public async Task AddCustomer(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save customer", ex);
            }
        }

        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update customer", ex);
            }
        }
    }
}
