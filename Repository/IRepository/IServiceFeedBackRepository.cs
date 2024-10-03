using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IServiceFeedBackRepository
    {
        Task<IEnumerable<ServiceFeedBack>> GetAllServiceFeedBacks();
        Task<ServiceFeedBack> GetServiceFeedBackById(int id);
        Task AddServiceFeedBack(ServiceFeedBack feedBack);
        Task UpdateServiceFeedBack(ServiceFeedBack feedBack);
    }
}
