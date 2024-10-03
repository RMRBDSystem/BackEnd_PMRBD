using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ServiceFeedBackDAO : SingletonBase<ServiceFeedBackDAO>
    {
        public async Task<IEnumerable<ServiceFeedBack>> GetAllServiceFeedBacks() => await _context.ServiceFeedBacks.ToListAsync();

        public async Task<ServiceFeedBack> GetServiceFeedBackById(int id)
        {
            var ServiceFeedBack = await _context.ServiceFeedBacks
                .Where(c => c.FeedBackId == id)
                .FirstOrDefaultAsync();
            if (ServiceFeedBack == null) return null;
            return ServiceFeedBack;
        }

        public async Task Add(ServiceFeedBack feedBack)
        {
            _context.ServiceFeedBacks.AddAsync(feedBack);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ServiceFeedBack feedBack)
        {
            var existingItem = await GetServiceFeedBackById(feedBack.FeedBackId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(feedBack);
            }
            else
            {
                _context.ServiceFeedBacks.Add(feedBack);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ServiceFeedBack = await GetServiceFeedBackById(id);
            if (ServiceFeedBack != null)
            {
                _context.ServiceFeedBacks.Remove(ServiceFeedBack);
                await _context.SaveChangesAsync();
            }
        }
    }
}
