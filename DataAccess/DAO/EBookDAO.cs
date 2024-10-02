using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class EBookDAO : SingletonBase<EBookDAO>
    {
        public async Task<IEnumerable<Ebook>> GetAllEbooks()
        {
            try
            {
                return await _context.Ebooks.ToListAsync();
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
                return await _context.Ebooks.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve ebook by id", ex);
            }
        }

        public async Task SaveEbook(Ebook ebook)
        {
            try
            {
                if (ebook != null)
                {
                    await _context.Ebooks.AddAsync(ebook);
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
                _context.Entry(ebook).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update ebook", ex);
            }
        }
    }
}
