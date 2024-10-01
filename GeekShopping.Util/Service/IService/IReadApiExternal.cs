using GeekShopping.Util.Models.ShippingRequest;

namespace GeekShopping.Util.Service.IService
{
    public interface IReadApiExternal
    {
        Task<HttpResponseMessage> ConfigureHttpClient(string url, string token, ShipmentRequest request);
    }
}
