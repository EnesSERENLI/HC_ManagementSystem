using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace HC.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;

        public HomeController(IProductService productService, ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _subCategoryService = subCategoryService;
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            ViewBag.SubCategories = await _subCategoryService.GetDefaultSubCategories();
            if (id != null)
            {
                return View(await _productService.GetProductsByCategory(id));
            }
            return View(await _productService.GetDefaultProducts());
        }
        public async Task<IActionResult> Details(Guid id)
        {
            ViewBag.SubCategories = await _subCategoryService.GetDefaultSubCategories();
            return View(await _productService.GetById(id));
        }

        public IActionResult Rezervation()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}