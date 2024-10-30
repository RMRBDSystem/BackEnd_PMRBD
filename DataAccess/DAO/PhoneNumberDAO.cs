using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class PhoneNumberDAO : SingletonBase<PhoneNumberDAO>
    {
        public async Task<IEnumerable<PhoneNumber>> GetAllPhoneNumber()
        {
            try
            {
                return await _context.PhoneNumbers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve phone numbers", ex);
            }
        }


        public async Task<PhoneNumber> GetPhoneNumberById(int id)
        {
            try
            {
                return await _context.PhoneNumbers.FirstOrDefaultAsync(x => x.PhoneNumberId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve phone number by id", ex);
            }
        }

        public async Task AddPhoneNumber(PhoneNumber phoneNumber)
        {
            try
            {
                if (phoneNumber != null)
                {
                    await _context.PhoneNumbers.AddAsync(phoneNumber);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save phone number", ex);
            }
        }


        public async Task UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            try
            {
                var existingItem = await GetPhoneNumberById(phoneNumber.PhoneNumberId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(existingItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update phone number", ex);
            }
        }
    }
}
