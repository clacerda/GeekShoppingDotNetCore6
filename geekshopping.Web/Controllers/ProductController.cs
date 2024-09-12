using GeekShopping.web.Models;
using GeekShopping.web.Services.IServices;
using GeekShopping.web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAllProductModels();

            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel model, List<string> ImagesBase64)
        {
            if (ModelState.IsValid)
            {
                model.Images = ImagesBase64.Select(img => new ImageViewModel { BaseImage = img, IdProduct = model.Id.ToString() }).ToList();
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct(model, token);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id, token);
            if (product != null) return View(product);
            return NotFound();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductViewModel product)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(product, token);
                if (response != null) return RedirectToAction(nameof(ProductIndex));
            }

            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindProductById(id, token);
            if (product != null) return View(product);
            return NotFound();
        }
        [Authorize (Roles = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductViewModel product)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(product.Id, token);
            if (response) return RedirectToAction(nameof(ProductIndex));
             
            return View(product);
        }


    }
}
