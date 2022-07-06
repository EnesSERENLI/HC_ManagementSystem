using FluentValidation.Results;
using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService, ICategoryService categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _subCategoryService.GetSubCategories());
        }

        #region Create
        public async Task<IActionResult> AddSubCategory()
        {
            ViewBag.Categories = await _categoryService.GetDefaultCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategory(CreateSubCategoryDTO model)
        {
            ViewBag.Categories = await _categoryService.GetDefaultCategories();
            CreateSubCategoryDTOValidation validator = new CreateSubCategoryDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                var result = await _subCategoryService.Create(model);
                TempData["message"] = result;
                if (result == "SubCategory added!")
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

        #region Update
        public async Task<IActionResult> UpdateSubCategory(Guid id)
        {
            ViewBag.Categories = await _categoryService.GetDefaultCategories();
            var subCategory = await _subCategoryService.GetById(id);
            return View(subCategory);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSubCategory(UpdateSubCategoryDTO model)
        {
            ViewBag.Categories = await _categoryService.GetDefaultCategories();
            UpdateSubCategoryDTOValidation validator = new UpdateSubCategoryDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                TempData["message"] = await _subCategoryService.Update(model);
                return RedirectToAction("index");
            }
            foreach (var item in results.Errors)
            {
                TempData["message"] = "Error message: " + item.ErrorMessage + "\n";
            }
            return View(model);
        } 
        #endregion

        #region Delete
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            TempData["message"] = await _subCategoryService.Delete(id);
            return RedirectToAction("index");
        } 
        #endregion
    }
}
