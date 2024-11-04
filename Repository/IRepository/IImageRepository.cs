﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImages();
        Task<Image> GetImageById(int id);
        Task AddImage(Image image);
        Task UpdateImage(Image image);
        Task<Image> GetFirstImageByRecipeId(int recipeId);
        Task<IEnumerable<Image>> GetImagesByRecipeId(int recipeId);
    }
}
