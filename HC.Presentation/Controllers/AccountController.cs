using HC.Application.Extensions;
using HC.Application.Models.DTO;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
using HC.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAppUserService appUserService, UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            RegisterDTOValidation validator = new RegisterDTOValidation();
            var results = validator.Validate(model);
            if (results.IsValid)
            {
                var result = await _appUserService.Register(model);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email); //kullaniciyi email üzerinde kontrol et.
                    MailSender.SendMail(user.Email, "<h2 style='color:green;'>Hotcat Activation</h2> <hr />", " Please click the link below to activate your subscription <br />https://localhost:7149/Account/Activation/" + user.Id);
                    return RedirectToAction("Pending");
                }
            }
            return View(model);
        }

        public IActionResult Pending()
        {
            return View();
        }

        public async Task<IActionResult> Activation(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirm = await _userManager.ConfirmEmailAsync(user, token);

                if (confirm.Succeeded)
                {
                    TempData["message"] = "Congratulations, your subscription has been completed. You can login here.";
                    return RedirectToAction("SignIn");
                }
                TempData["message"] = "An error has occurred. Please check your information.";
                return RedirectToAction("Register");
            }
            return RedirectToAction("Register");
        }

    }
}
