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
        public async Task<IEnumerable<ServiceFeedback>> GetAllServiceFeedbacks() => await _context.ServiceFeedbacks.ToListAsync();

        public async Task<ServiceFeedback> GetServiceFeedBackById(int id)
        {
            var ServiceFeedBack = await _context.ServiceFeedbacks
                .Where(c => c.FeedBackId == id)
                .FirstOrDefaultAsync();
            if (ServiceFeedBack == null) return null;
            return ServiceFeedBack;
        }

        public async Task Add(ServiceFeedback feedBack)
        {
            _context.ServiceFeedbacks.AddAsync(feedBack);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ServiceFeedback feedBack)
        {
            var existingItem = await GetServiceFeedBackById(feedBack.FeedBackId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(feedBack);
            }
            else
            {
                _context.ServiceFeedbacks.Add(feedBack);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ServiceFeedBack = await GetServiceFeedBackById(id);
            if (ServiceFeedBack != null)
            {
                _context.ServiceFeedbacks.Remove(ServiceFeedBack);
                await _context.SaveChangesAsync();
            }
        }
    }
}
