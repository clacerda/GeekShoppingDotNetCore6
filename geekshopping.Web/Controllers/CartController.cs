using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.web.Controllers
{
    public class CartController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController( IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {            
            return View(await findUserCart());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(user => user.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.ApplyCoupon(model, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(user => user.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.RemoveCoupon(UserId, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }



        public async Task<IActionResult> Remove(int Id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var UserId = User.Claims.Where(user => user.Type == "sub")?.FirstOrDefault()?.Value;
            var response = await _cartService.RemoveFromCart(Id, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await findUserCart());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _cartService.Checkout(model.CartHeader, token);

            if (response != null && response.GetType() == typeof(string))
            {
                TempData["Error"] = response;
                return RedirectToAction(nameof(Checkout));
            }
            else if (response != null)
            {
                return RedirectToAction(nameof(Confirmation));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
            return View();
        }

        private async Task<CartViewModel> findUserCart()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var UserId = User.Claims.Where(user => user.Type == "sub")?.FirstOrDefault()?.Value;

            var response = await _cartService.FindCartByUserId(UserId, token);

            if (response?.CartHeader != null)
            {
                if (!string.IsNullOrEmpty(response.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon(response.CartHeader.CouponCode, token);

                    if (coupon?.CouponCode != null)
                    {
                        response.CartHeader.DiscountTotal = coupon.DiscountAmount;
                    }
                }
                foreach (var detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }
                response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountTotal;
            }

            return response;
        }
    }
}
