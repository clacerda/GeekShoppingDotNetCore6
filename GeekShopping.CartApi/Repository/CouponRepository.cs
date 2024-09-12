using GeekShopping.CartApi.Data.Value_Objects;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.CartApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client; 
        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponVO> GetCoupon(string couponCode, string token)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
                var response = await _client.GetAsync($"api/v1/Coupon/{couponCode}");
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine($"API Error: {response.StatusCode}, Content: {content}");
                    return new CouponVO();
                }

                return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw new Exception("Something went wrong when calling API", ex);
            }

        }
    }
}
