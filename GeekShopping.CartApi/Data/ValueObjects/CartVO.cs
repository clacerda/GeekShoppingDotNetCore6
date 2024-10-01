using GeekShopping.CartApi.Model;
using GeekShopping.Util.Models.ShippingCost;

namespace GeekShopping.CartApi.Data.ValueObjects;

public class CartVO
{
    public CartHeaderVO CartHeader { get; set; }
    public IEnumerable<CartDetailVO>? CartDetails { get; set; }
    public List<ShippingCost>?  shippingCosts { get; set; }
}
