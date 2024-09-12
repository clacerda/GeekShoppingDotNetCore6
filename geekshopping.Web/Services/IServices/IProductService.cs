using GeekShopping.web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekShopping.web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProductModels();
        Task<ProductViewModel> FindProductById(long id, string token);
        Task<ProductViewModel> CreateProduct(ProductViewModel productName, string token);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productModel, string token);
        Task<bool> DeleteProductById(long id, string token);
    }
}
