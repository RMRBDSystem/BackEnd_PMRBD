using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountProfileDAO : SingletonBase<AccountProfileDAO>
    {
        public async Task<IEnumerable<AccountProfile>> GetAllAccountProfiles()
        {
            try
            {
                return await _context.AccountProfiles.Include(x => x.Account).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve AccountProfiles", ex);
            }
        }

        public async Task<AccountProfile?> GetAccountProfileByAccountId(int id)
        {
            try
            {
                return await _context.AccountProfiles.Include(x => x.Account).FirstOrDefaultAsync(x => x.AccountId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve AccountProfile by Account id", ex);
            }
        }

        public async Task AddAccountProfile(AccountProfile accountProfile)
        {
            try
            {
                if (accountProfile != null)
                {

                    // Kiểm tra xem AccountProfile đã tồn tại chưa và ngắt theo dõi nếu cần thiết
                    var existingProfile = await _context.AccountProfiles
                        .AsNoTracking()  // Ngắt theo dõi để tránh xung đột khi kiểm tra
                        .FirstOrDefaultAsync(a => a.AccountId == accountProfile.AccountId);


                    if (existingProfile != null)
                    {
                        // Nếu đã tồn tại, có thể ném ra ngoại lệ hoặc tiến hành cập nhật
                        throw new Exception("Account profile already exists.");
                    }


                    // Tách thực thể nếu DbContext đang theo dõi
                    var trackedProfile = _context.AccountProfiles.Local
                        .FirstOrDefault(entry => entry.AccountId == accountProfile.AccountId);
                    if (trackedProfile != null)
                    {
                        _context.Entry(trackedProfile).State = EntityState.Detached;
                    }


                    // Thêm mới AccountProfile
                    await _context.AccountProfiles.AddAsync(accountProfile);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw new Exception("Failed to save account profile", ex);
            }
        }


        public async Task UpdateAccountProfile(AccountProfile accountProfile)
        {
            try
            {
                var existingItem = await _context.AccountProfiles.FirstOrDefaultAsync(e => e.AccountId == accountProfile.AccountId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(accountProfile);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update AccountProfile", ex);
            }
        }

        public async Task DeleteAccountProfile(int id)
        {
            try
            {
                var AccountProfile = await _context.AccountProfiles.FirstOrDefaultAsync(x => x.AccountId == id);
                if (AccountProfile != null)
                {
                    _context.AccountProfiles.Remove(AccountProfile);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to delete AccountProfile", ex);
            }
            
        }
    }
}
