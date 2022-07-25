using FluentValidation.Results;
using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.GetDepartments());
        }

        #region Create
        public IActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment(CreateDepartmentDTO model)
        {
            CreateDepartmentDTOValidation validator = new CreateDepartmentDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                var result = await _departmentService.Create(model);
                TempData["message"] = result;
                if (result == "Department Added!")
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
        public async Task<IActionResult> UpdateDepartment(Guid id)
        {
            var department = await _departmentService.GetById(id);
            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentDTO model)
        {
            UpdateDepartmentDTOValidation validator = new UpdateDepartmentDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                TempData["message"] = await _departmentService.Update(model);
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
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            TempData["message"] = await _departmentService.Delete(id);
            return RedirectToAction("index");
        } 
        #endregion
    }
}
