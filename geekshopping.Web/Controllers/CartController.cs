using GeekShopping.Util.Models.ShippingCost;
using GeekShopping.Util.Models.ShippingRequest;
using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using GeekShopping.web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeekShopping.web.Controllers
{
    public class CartController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client; 
        private const string BasePathApiUtil = "https://localhost:7222/api/v1/Util/calculate";
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, 
                              ICartService cartService, 
                              ICouponService couponService, 
                              HttpClient client,
                              IConfiguration configuration)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _configuration = configuration;
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

        public async Task<CartViewModel> findUserCart()
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

        [HttpPost]
        public async Task<IActionResult> CalculateShipping([FromBody] ShipmentRequest request)
        { 
            var response = await _client.PostAsJson(BasePathApiUtil, request);

           if (response.IsSuccessStatusCode)
            {
                var ShippimentValues = _configuration.GetSection("ServicesShipping:Name").Get<string[]>();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                var shippingCost = JsonSerializer.Deserialize<List<ShippingCostViewModel>>(jsonResponse).Where(sc => ShippimentValues.Contains(sc.Name)).ToList();

                return Ok(shippingCost);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error calculating shipping: {errorMessage}");
            } 
        }
    }
}
