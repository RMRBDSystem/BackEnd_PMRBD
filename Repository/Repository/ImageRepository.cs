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
    public class ImageRepository :IImageRepository
    {
        public async Task<IEnumerable<Image>> GetAllImages() => await ImageDAO.Instance.GetAllImages();
        public async Task<Image> GetImageById(int id) => await ImageDAO.Instance.GetImageById(id);
        public async Task AddImage(Image image) => await ImageDAO.Instance.Add(image);
        public async Task UpdateImage(Image image) => await ImageDAO.Instance.Update(image);
    }
}
}
