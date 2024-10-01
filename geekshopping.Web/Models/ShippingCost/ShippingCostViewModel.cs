using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class ShippingCostViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("custom_price")]
        public string? CustomPrice { get; set; }
        
        [JsonPropertyName("discount")]
        public string? Discount { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("delivery_time")]
        public decimal? DeliveryTime { get; set; }

        [JsonPropertyName("delivery_range")]
        public DeliveryRangeViewModel? DeliveryRange { get; set; }

        [JsonPropertyName("custom_delivery_range")]
        public DeliveryRangeViewModel? CustomDeliveryRange { get; set; }        

        [JsonPropertyName("packages")]
        public List<PackegesViewModel>? Packeges { get; set; }

        [JsonPropertyName("additional_services")]
        public AdditionalServicesViewModel? AdditionalServices { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("company")]
        public CompanyViewModel Company { get; set; }
    }
}
