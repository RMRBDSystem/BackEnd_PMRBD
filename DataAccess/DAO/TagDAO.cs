using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TagDAO : SingletonBase<TagDAO>
    {
        public async Task<IEnumerable<Tag>> GetAllTags() => await _context.Tags.ToListAsync();

        public async Task<Tag> GetTagById(int id)
        {
            var tag = await _context.Tags
                .Where(c => c.TagId == id)
                .FirstOrDefaultAsync();
            if (tag == null) return null;
            return tag;
        }

        public async Task Add(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tag tag)
        {
            var existingItem = await _context.Tags.FirstOrDefaultAsync(x => x.TagId == tag.TagId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(tag);
            }
            else
            {
                _context.Tags.Add(tag);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.TagId == id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}
