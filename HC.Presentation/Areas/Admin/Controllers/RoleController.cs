using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
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
                TempData["message"] = await _roleService.Create(model);
                return RedirectToAction("Index");
            }
            foreach (var item in resultValidate.Errors)
            {
                TempData["mesage"] += item.ErrorMessage + "\n";
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
