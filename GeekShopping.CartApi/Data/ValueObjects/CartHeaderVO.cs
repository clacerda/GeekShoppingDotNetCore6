namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class CartHeaderVO
    {
        public long Id { get; set; }
        public string userId { get; set; } 
        public string? CouponCode { get; set; }
    }
}
