using GeekShopping.Util.Models.ShippingCost;

namespace GeekShopping.web.Models;

public class CartViewModel
{
    public CartHeaderViewModel CartHeader { get; set; }
    public IEnumerable<CartDetailViewModel>? CartDetails { get; set; }
    public List<ShippingCostViewModel>? ShippingCosts { get; set; }
}
