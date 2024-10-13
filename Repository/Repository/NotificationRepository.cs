using BusinessObject.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NotificationRepository :INotificationRepository
    {
        public async Task<IEnumerable<Notification>> GetAllNotifications() => await NotificationDAO.Instance.GetAllNotifications();
        public async Task<Notification> GetNotificationById(int id) => await NotificationDAO.Instance.GetNotificationById(id);
        public async Task AddNotification(Notification notification) => await NotificationDAO.Instance.Add(notification);
        public async Task UpdateNotification(Notification notification) => await NotificationDAO.Instance.Update(notification);
    }
}
