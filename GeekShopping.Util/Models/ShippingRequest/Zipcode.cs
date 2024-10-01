using System.Text.Json.Serialization;

namespace GeekShopping.Util.Models.ShippingRequest
{
    public class Zipcode
    {
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }
    }
}
