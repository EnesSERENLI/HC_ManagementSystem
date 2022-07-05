using HC.Application.Service.Interface;
using HC.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HC.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetDefaultProducts());
        }
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await _productService.GetById(id));
        }
    }
}