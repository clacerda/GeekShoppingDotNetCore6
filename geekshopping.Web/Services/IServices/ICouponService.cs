using GeekShopping.web.Models;
using System.Threading.Tasks;

namespace GeekShopping.web.Services.IServices
{
    public interface ICouponService
    {
         Task<CouponViewModel> GetCoupon(string code, string token);
    }
}
