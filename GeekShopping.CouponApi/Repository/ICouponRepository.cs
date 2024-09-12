using GeekShopping.CouponApi.Data.Value_Objects;

namespace GeekShopping.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string CouponCode);
    }
}
