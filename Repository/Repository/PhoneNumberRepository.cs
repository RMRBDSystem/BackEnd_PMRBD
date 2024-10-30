using BusinessObject.Models;
using DataAccess.DAO;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        public async Task<IEnumerable<PhoneNumber>> GetAllPhoneNumbers() => await PhoneNumberDAO.Instance.GetAllPhoneNumber();
        public async Task<PhoneNumber> GetPhoneNumberById(int customerId) => await PhoneNumberDAO.Instance.GetPhoneNumberById(customerId);
        public async Task AddPhoneNumber(PhoneNumber phoneNumber) => await PhoneNumberDAO.Instance.AddPhoneNumber(phoneNumber);
        public async Task UpdatePhoneNumber(PhoneNumber phoneNumber) => await PhoneNumberDAO.Instance.UpdatePhoneNumber(phoneNumber);
    }
}
