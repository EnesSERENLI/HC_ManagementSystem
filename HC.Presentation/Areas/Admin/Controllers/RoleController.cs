using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using HC.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<AppUserRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(IRoleService roleService,RoleManager<AppUserRole> roleManager,UserManager<AppUser> userManager)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _userManager = userManager;
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

        #region AssignedRole
        public async Task<IActionResult> AssignedRoleToUsers(string id)
        {
            AppUserRole role = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNoRole = new List<AppUser>();

            foreach (AppUser user in _userManager.Users)
            {
                var result = await _userManager.IsInRoleAsync(user, role.Name); //Bu işlem için ConnectionString içerisine eklenmeli! => MultipleActiveResultSets=True; 
                if (result)
                    hasRole.Add(user);
                else
                    hasNoRole.Add(user);
            }

            return View(new AssignedRoleToUsersDTO { Role = role, HasRole = hasRole, HasNoRole = hasNoRole });
        }
        [HttpPost]
        public async Task<IActionResult> AssignedRoleToUsers(AssignedRoleToUsersDTO model)
        {
            IdentityResult result;


            foreach (string userId in model.UsersToBeAdded ?? new string[] { }) //Eğer kullanıcı eklenmezse array boş gelicek bu durumda null ref yememek için new string olarak yeni array açıldı.
            {
                AppUser user = await _userManager.FindByIdAsync(userId); //Arraydaki id den kullanıcıyı çek
                result = await _userManager.AddToRoleAsync(user, model.Role.Name); // çektiğin kullanıcıya belirtilen rolü ver.
            }

            foreach (string userId in model.UsersToBeDeleted ?? new string[] { })
            {
                AppUser user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(user, model.Role.Name); // kullanıcıdan belirtilen rolü sil.
            }

            return RedirectToAction("Index");
        } 
        #endregion

    }
}
