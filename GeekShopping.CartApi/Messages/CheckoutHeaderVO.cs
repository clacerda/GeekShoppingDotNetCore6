using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.MessageBus;
using GeekShopping.Util.Models.ShippingCost;

namespace GeekShopping.CartApi.Messages
{
    public class CheckoutHeaderVO : BaseMessage
    {
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal DiscountTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Time { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpireMothYear { get; set; }
        public int CartTotalItens { get; set; }
        public IEnumerable<CartDetailVO>? CartDetails { get; set; }
        public string Address { get; set; }
        public string NumberAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
