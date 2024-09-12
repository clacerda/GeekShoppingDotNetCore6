using System.Collections.Generic;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Data.ValueObjects;

namespace GeekShopping.ProductAPI.Repository
{
    public interface IImageRepository
    {
        Task<List<ImageVO>> GetByIdAsync(string id);
        Task<IEnumerable<ImageVO>> GetAllAsync();

        Task<IEnumerable<ImageVO>> CreateImages(IEnumerable<ImageVO> images);
        Task<ImageVO> UpdateImage(ImageVO image);
        Task<bool> DeleteImage(string id);
    }
}
