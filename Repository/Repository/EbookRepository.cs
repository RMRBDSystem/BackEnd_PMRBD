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
    public class EbookRepository : IEbookRepository
    {
        public async Task<IEnumerable<Ebook>> GetAllEbooks() => await EBookDAO.Instance.GetAllEbooks();
        public async Task<Ebook> GetEbookById(int id) => await EBookDAO.Instance.GetEbookById(id);
        public async Task AddEbook(Ebook ebook) => await EBookDAO.Instance.AddEbook(ebook);
        public async Task UpdateEbook(Ebook ebook) => await EBookDAO.Instance.UpdateEbook(ebook);
    }
}
