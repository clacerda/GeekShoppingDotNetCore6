using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingCost
{
    public class Packeges
    {
        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("discount")]
        public string Discount { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonPropertyName("weight")]
        public string Weight { get; set; }

        [JsonPropertyName("insurance_value")]
        public string InsuranceValue { get; set; }
    }
}
