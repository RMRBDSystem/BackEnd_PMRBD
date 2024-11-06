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
            _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Image image)
        {
            var existingItem = await GetImageById(image.ImageId);
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
            var image = await GetImageById(id);
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
    }
}
