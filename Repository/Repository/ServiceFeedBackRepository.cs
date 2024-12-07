using BusinessObject.Models;
using DataAccess;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ServiceFeedBackRepository : IServiceFeedBackRepository
    {
        public async Task<IEnumerable<ServiceFeedback>> GetAllServiceFeedBacks() => await ServiceFeedBackDAO.Instance.GetAllServiceFeedbacks();
        public async Task<ServiceFeedback> GetServiceFeedBackById(int id) => await ServiceFeedBackDAO.Instance.GetServiceFeedBackById(id);
        public async Task AddServiceFeedBack(ServiceFeedback ServiceFeedBack) => await ServiceFeedBackDAO.Instance.Add(ServiceFeedBack);
        public async Task UpdateServiceFeedBack(ServiceFeedback ServiceFeedBack) => await ServiceFeedBackDAO.Instance.Update(ServiceFeedBack);
        public async Task DeleteServiceFeedBack(int id) => await ServiceFeedBackDAO.Instance.Delete(id);
    }
}
