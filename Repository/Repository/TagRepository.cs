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
    public class TagRepository : ITagRepository
    {
        public async Task<IEnumerable<Tag>> GetAllTags() => await TagDAO.Instance.GetAllTags();
        public async Task<Tag> GetTagById(int id) => await TagDAO.Instance.GetTagById(id);
        public async Task AddTag(Tag tag) => await TagDAO.Instance.Add(tag);
        public async Task UpdateTag(Tag tag) => await TagDAO.Instance.Update(tag);
    }
}
