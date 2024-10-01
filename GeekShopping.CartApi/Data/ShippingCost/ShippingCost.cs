using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class ShippingCost
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
        public DeliveryRange? DeliveryRange { get; set; }

        [JsonPropertyName("custom_delivery_range")]
        public DeliveryRange? CustomDeliveryRange { get; set; }        

        [JsonPropertyName("packages")]
        public List<Packeges>? Packeges { get; set; }

        [JsonPropertyName("additional_services")]
        public AdditionalServices? AdditionalServices { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("company")]
        public Company Company { get; set; }
    }
}
