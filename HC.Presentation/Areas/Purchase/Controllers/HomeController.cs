using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    [Authorize(Roles = "Purchase")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetDefaultProducts();
            return View(products);
        }

        public async Task<IActionResult> BuyProduct(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product != null)
                return View(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BuyProduct(UpdateProductDTO model)
        {
            if (model != null)
            {
                TempData["message"] = await _productService.Update(model);
                return RedirectToAction("Index");
            }
            TempData["message"] = "No such product found!";
            return RedirectToAction("Index");
        }
    }
}
