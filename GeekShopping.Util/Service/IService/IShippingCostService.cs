using GeekShopping.Util.Models.ShippingCost;

namespace GeekShopping.Util.Service.IService
{
    public interface IShippingCostService
    {
        Task<ShippingCost> FindShippingCost(ShippingCost shippingCost);
    }
}
