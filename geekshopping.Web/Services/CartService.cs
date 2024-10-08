﻿using GeekShopping.Util.Models.ShippingCost;
using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using GeekShopping.web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        private const string BasePath = "api/v1/cart";
        private const string BasePathApiUtil = "https://localhost:7222/api/v1/Util/calculate";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel>();
        }
        public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BasePath}/add-cart", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Something went wrong when calling API. Method: AddItemToCart");
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson($"{BasePath}/update-cart", cart);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<CartViewModel>();
            else throw new Exception("Ocorreu um erro ao consultar a API. Method: UpdateCart");
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");
            if (response.IsSuccessStatusCode) 
                return await response.ReadContentAs<bool>();
            else throw new Exception("Ocorreu um erro ao consultar a API. Method: RemoveFromCart");
        }

        public async Task<bool> ApplyCoupon(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 

            var response = await _client.PostAsJson($"{BasePath}/apply-coupon", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else throw new Exception("Ocorreu um erro ao consultar a API. Method: ApplyCoupon");
        }

        public async Task<bool> RemoveCoupon(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{BasePath}/remove-coupon/{userId}");
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else throw new Exception("Ocorreu um erro ao consultar a API. Method: RemoveCoupon");
        }

        public async Task<object> Checkout(CartHeaderViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson($"{BasePath}/checkout", model);

            

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartHeaderViewModel>();
            }
            else if (response.StatusCode.ToString().Equals("PreconditionFailed"))
            {
                return "Coupon Price has changed, please confirm!";
            }
            else throw new Exception("Something went wrong when calling API");
        }

        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
