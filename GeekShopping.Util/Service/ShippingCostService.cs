using GeekShopping.Util.Models.ShippingCost;
using GeekShopping.Util.Service.IService;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class ShippingCostService : IShippingCostService
{
    private readonly HttpClient _client;
    public const string BasePath = "https://sandbox.melhorenvio.com.br/api/v2/me/shipment/calculate";

    public ShippingCostService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<ShippingCost> FindShippingCost(ShippingCost model)
    {
        var response = await _client.PostAsJsonAsync($"{BasePath}", model);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ShippingCost>();
        else
            throw new Exception("Something went wrong when calling API.");
    }
}
