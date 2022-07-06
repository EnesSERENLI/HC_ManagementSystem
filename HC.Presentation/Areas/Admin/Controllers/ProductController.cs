using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISubCategoryService _subCategoryService;        
        public ProductController(IProductService productService,ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _subCategoryService = subCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProducts());
        }

        #region CreateProduct
        public async Task<IActionResult> AddProduct()
        {
            ViewBag.SubCategories = await _subCategoryService.GetDefaultSubCategories();
            return View();
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
                return View(model);
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    TempData["message"] += "Error message: " + item.ErrorMessage + "\n";
                }
                return View(model);
            }
        } 
        #endregion

        #region UpdateProduct
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            var product = await _productService.GetById(id);
            ViewBag.SubCategories = await _subCategoryService.GetDefaultSubCategories();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO model)
        {
            ViewBag.SubCategories = await _subCategoryService.GetDefaultSubCategories();
            UpdateProductDTOValidation validator = new UpdateProductDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                TempData["message"] = await _productService.Update(model);
                return RedirectToAction("index");
            }
            foreach (var item in results.Errors)
            {
                TempData["message"] = "Error message: " + item.ErrorMessage + "\n";
            }
            return View(model);
        }
        #endregion

        #region DeleteProduct
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            TempData["message"] = await _productService.Delete(id);
            return RedirectToAction("index");
        } 
        #endregion

    }
}
