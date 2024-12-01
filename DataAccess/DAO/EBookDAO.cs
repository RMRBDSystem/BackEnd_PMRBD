using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EBookDAO : SingletonBase<EBookDAO>
    {
        public async Task<IEnumerable<Ebook>> GetAllEbooks()
        {
            try
            {
                return await _context.Ebooks.Include(e => e.CreateBy).Include(e => e.Category).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve ebooks", ex);
            }
        }

        public async Task<Ebook?> GetEbookById(int id)
        {
            try
            {
                return await _context.Ebooks.Include(e => e.CreateBy).Include(e => e.Category).AsNoTracking().FirstOrDefaultAsync(e => e.EbookId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve ebook by id", ex);
            }
        }

        public async Task AddEbook(Ebook ebook)
        {
            try
            {
                if (ebook != null)
                {
                    _context.Ebooks.AddAsync(ebook);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save ebook", ex);
            }
        }

        public async Task UpdateEbook(Ebook ebook)
        {
            try
            {
                var existingItem = await GetEbookById(ebook.EbookId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(ebook);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update ebook", ex);
            }
        }
    }
}
