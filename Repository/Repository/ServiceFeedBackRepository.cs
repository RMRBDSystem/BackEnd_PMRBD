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
        public async Task<IEnumerable<ServiceFeedBack>> GetAllServiceFeedBacks() => await ServiceFeedBackDAO.Instance.GetAllServiceFeedBacks();
        public async Task<ServiceFeedBack> GetServiceFeedBackById(int id) => await ServiceFeedBackDAO.Instance.GetServiceFeedBackById(id);
        public async Task AddServiceFeedBack(ServiceFeedBack ServiceFeedBack) => await ServiceFeedBackDAO.Instance.Add(ServiceFeedBack);
        public async Task UpdateServiceFeedBack(ServiceFeedBack ServiceFeedBack) => await ServiceFeedBackDAO.Instance.Update(ServiceFeedBack);
    }
}
