using GeekShopping.Util.Models.ShippingRequest;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using GeekShopping.Util.Service.IService;

namespace GeekShopping.Util.Service
{
    public class ReadApiExternal : IReadApiExternal
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ReadApiExternal(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<HttpResponseMessage> ConfigureHttpClient(string url, string token, ShipmentRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Aplicacao/1.0");

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            return await client.PostAsync(url, content);
        }
    }
}
