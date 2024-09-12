using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using GeekShopping.web.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GeekShopping.web.Services
{
    public class ProductService : IProductService
    {

        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";
        public const string AccessApiProduct = "https://localhost:4440/api/v1/product/GetImageBase/";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProductModels()
        {
            var response = await _client.GetAsync(BasePath);

            var products = await response.ReadContentAs<List<ProductViewModel>>();

            return products;
        }

        public async Task<ProductViewModel> FindProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/ {id}");
            return await response.ReadContentAs<ProductViewModel>();
        }
        public async Task<ProductViewModel> CreateProduct(ProductViewModel productName, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, productName);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductViewModel>();
            else throw new Exception("Ocorreu um erro ao consultar a API.");
        }       
         
        public async Task<ProductViewModel> UpdateProduct(ProductViewModel productModel, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, productModel);
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<ProductViewModel>();
            else throw new Exception("Ocorreu um erro ao consultar a API.");
        }

        public async Task<bool> DeleteProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/ {id}");
            if (response.IsSuccessStatusCode) return await response.ReadContentAs<bool>();
            else throw new Exception("Ocorreu um erro ao consultar a API.");
        }

    }
}
