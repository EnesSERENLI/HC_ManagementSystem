using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using HC.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<AppUserRole> _roleManager;

        public RoleController(IRoleService roleService,RoleManager<AppUserRole> roleManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_roleService.GetRolesList());
        }

        #region CreateRole
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDTO model)
        {
            CreateRoleDTOValidation validator = new CreateRoleDTOValidation();
            var resultValidate = validator.Validate(model);
            if (resultValidate.IsValid)
            {
                var message = await _roleService.Create(model);
                TempData["message"] = message;
                if (message == "Role has been created!")
                    return RedirectToAction("Index");
                else
                    return View();
            }
            foreach (var item in resultValidate.Errors)
            {
                TempData["message"] += item.ErrorMessage + "\n";
            }
            return View(model);
        }
        #endregion

        #region DeleteRole
        public async Task<IActionResult> Delete(string id)
        {
            TempData["message"] = await _roleService.Delete(id);

            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleService.GetById(id);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoleDTO model)
        {
            UpdateRoleDTOValidation validator = new UpdateRoleDTOValidation();
            var updatedRoleValidate = validator.Validate(model);
            if (updatedRoleValidate.IsValid)
            {
                TempData["message"] = await _roleService.Update(model);
                return RedirectToAction("Index");
            }
            foreach (var item in updatedRoleValidate.Errors)
            {
                TempData["message"] = item.ErrorMessage + "\n";
            }
            return View(model);
        } 
        #endregion


    }
}
