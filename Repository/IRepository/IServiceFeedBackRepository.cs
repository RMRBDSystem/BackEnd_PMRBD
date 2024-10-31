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
        Task<IEnumerable<ServiceFeedback>> GetAllServiceFeedBacks();
        Task<ServiceFeedback> GetServiceFeedBackById(int id);
        Task AddServiceFeedBack(ServiceFeedback feedBack);
        Task UpdateServiceFeedBack(ServiceFeedback feedBack);
    }
}
