using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IPhoneNumberRepository
    {
        Task<IEnumerable<PhoneNumber>> GetAllPhoneNumbers();
        Task<PhoneNumber> GetPhoneNumberById(int id);
        Task AddPhoneNumber(PhoneNumber phoneNumber);
        Task UpdatePhoneNumber(PhoneNumber phoneNumber);
    }
}
