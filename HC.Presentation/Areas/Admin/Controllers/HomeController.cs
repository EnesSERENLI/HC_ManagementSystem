using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;

        public HomeController(IProductService productService,ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _subCategoryService = subCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProducts());
        }

        public async Task<IActionResult> AddProduct(Guid id)
        {
            ViewBag.SubCategories =await _subCategoryService.GetDefaultSubCategories();
            return View(await _productService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDTO model)
        {
            ViewBag.SubCategories = await _subCategoryService.GetDefaultSubCategories();
            CreateProductDTOValidation validator = new CreateProductDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                var result = await _productService.Create(model);
                TempData["message"] = result;
                if (result == "Product added!")
                {
                    return RedirectToAction("index"); 
                }
                return View();
            }
            else
            {                
                foreach (var item in results.Errors)
                {
                    TempData["message"] += "Error message: " + item.ErrorMessage +"\n";
                }
                return View(model);
            }
        }
    }
}
