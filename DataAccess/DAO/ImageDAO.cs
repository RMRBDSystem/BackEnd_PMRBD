using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ImageDAO : SingletonBase<ImageDAO>
    {
        public async Task<IEnumerable<Image>> GetAllImages() => await _context.Images.ToListAsync();

        public async Task<Image> GetImageById(int id)
        {
            var image = await _context.Images
                .Where(c => c.ImageId == id)
                .FirstOrDefaultAsync();
            if (image == null) return null;
            return image;
        }

        public async Task Add(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Image image)
        {
            var existingItem = await _context.Images.FindAsync(image.ImageId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(image);
            }
            else
            {
                _context.Images.Add(image);
            }
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Image> GetFirstImageByRecipeId(int recipeId)
        {
            return await _context.Images.Where(i => i.RecipeId == recipeId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Image>> GetImagesByRecipeId(int recipeId)
        {
            return await _context.Images
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<Image?> GetFirstImageByBookId(int bookId)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.BookId == bookId);
        }

        public async Task<IEnumerable<Image>> GetImagesByBookId(int bookId)
        {
            return await _context.Images
                .Where(i => i.BookId == bookId)
                .ToListAsync();
        }
    }
}
