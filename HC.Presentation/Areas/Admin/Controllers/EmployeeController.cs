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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _employeeService.GetEmployees());
        }

        #region Create
        public async Task<IActionResult> AddEmployee()
        {
            ViewBag.Departments = await _departmentService.GetDefaultDepartments();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(CreateEmployeeDTO model)
        {
            ViewBag.Departments = await _departmentService.GetDefaultDepartments();
            CreateEmployeeDTOValidation validator = new CreateEmployeeDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                var result = await _employeeService.Create(model);
                TempData["message"] = result;
                if (result == "Employee added!")
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
        public async Task<IActionResult> UpdateEmployee(Guid id)
        {
            ViewBag.Departments = await _departmentService.GetDefaultDepartments();
            var employee = await _employeeService.GetById(id);
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO model)
        {
            ViewBag.Departments = await _departmentService.GetDefaultDepartments();
            UpdateEmployeeDTOValidation validator = new UpdateEmployeeDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                TempData["message"] = await _employeeService.Update(model);
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
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            TempData["message"] = await _employeeService.Delete(id);
            return RedirectToAction("index");
        } 
        #endregion
    }
}
