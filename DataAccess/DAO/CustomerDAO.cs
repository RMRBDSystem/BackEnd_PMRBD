﻿using BusinessObject.Models;
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
                return await _context.Customers.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
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
                var existingItem = await GetCustomerById(customer.CustomerId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(customer);
                    await _context.SaveChangesAsync();
                }             
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update customer", ex);
            }
        }


        public async Task<Customer?> GetCustomerByGoogleId(string googleId)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.GoogleId == googleId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve customer", ex);
            }
        }

    }
}
