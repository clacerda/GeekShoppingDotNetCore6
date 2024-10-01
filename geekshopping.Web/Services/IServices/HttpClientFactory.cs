using System.Net.Http.Headers;

namespace geekshopping.Web.Services.IServices
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public HttpClient CreateClient(string name)
        {
            var client = new HttpClient();

            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
