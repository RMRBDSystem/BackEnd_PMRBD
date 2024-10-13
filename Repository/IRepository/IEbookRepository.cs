using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IEbookRepository
    {
        Task<IEnumerable<Ebook>> GetAllEbooks();
        Task<Ebook> GetEbookById(int id);
        Task AddEbook(Ebook ebook);
        Task UpdateEbook(Ebook ebook);
    }
}
