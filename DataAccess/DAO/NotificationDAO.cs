using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class NotificationDAO : SingletonBase<NotificationDAO>
    {
        public async Task<IEnumerable<Notification>> GetAllNotifications() => await _context.Notifications.ToListAsync();

        public async Task<Notification> GetNotificationById(int id)
        {
            var noti = await _context.Notifications
                .Where(c => c.NotificationId == id)
                .FirstOrDefaultAsync();
            if (noti == null) return null;
            return noti;
        }

        public async Task Add(Notification noti)
        {
            await _context.Notifications.AddAsync(noti);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Notification noti)
        {
            var existingItem = await _context.Notifications.FirstOrDefaultAsync(x => x.NotificationId == noti.NotificationId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(noti);
            }
            else
            {
                _context.Notifications.Add(noti);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var noti = await _context.Notifications.FirstOrDefaultAsync(x => x.NotificationId == id);
            if (noti != null)
            {
                _context.Notifications.Remove(noti);
                await _context.SaveChangesAsync();
            }
        }
    }
}
