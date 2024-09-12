using GeekShopping.CartApi.Data.Value_Objects;

namespace GeekShopping.CartApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string CouponCode, string token);
    }
}
