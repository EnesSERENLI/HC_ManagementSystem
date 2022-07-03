using FluentValidation.Results;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using HC.Domain.Entities.Concrete;
using HC.Domain.Repositories.BaseRepository;
using HC.Domain.Repositories.EntityTypeRepositoy;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _departmentService.GetFilteredFirstOrDefaults(selector: x => new DepartmentVM
        //    {
        //        ID = x.ID,
        //        DepartmentName = x.DepartmentName
        //    },
        //    expression: x => x.Status == Domain.Enums.Status.Active
        //    ));
        //}

        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.GetDepartment());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDTO model)
        {
            CreateDepartmentDTOValidation validator = new CreateDepartmentDTOValidation();
            ValidationResult results = validator.Validate(model);
            if (results.IsValid)
            {
                //Department department = new Department();
                //department.DepartmentName = departmentDTO.DepartmentName;
                var result = await _departmentService.Create(model);
                TempData["message"] = result;
                return RedirectToAction("index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    TempData["message"] = "Error message: " + item.ErrorMessage;
                }
                return View(model);
            }
        }
    }
}
