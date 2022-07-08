﻿using HC.Application.Extensions;
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

        #region Register
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
                    var user = await _userManager.FindByEmailAsync(model.Email); //kullaniciyi email üzerinde kontrol et.
                    //MailSender.SendMail(user.Email, "<h2 style='color:green;'>Hotcat Activation</h2> <hr />", " Please click the link below to activate your subscription <br />https://localhost:7149/Account/Activation/" + user.Id);
                    //Mail atma olayında sıkıntı var güvenlik onaylarından dolayı şimdilik pending sayfasından onaylatma işlemini yaptırıyorum.
                    return RedirectToAction("Pending", "Account", user, null);
                }
                foreach (var item in result.Errors)
                {
                    TempData["message"] = item.Description;
                }
                return View();
            }
            foreach (var item in results.Errors)
            {
                TempData["message"] = item.ErrorMessage;
            }
            return View(model);
        }
        #endregion

        #region Pending
        public IActionResult Pending(AppUser user)
        {
            return View(user);
        } 
        #endregion

        #region Activation
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
        #endregion

        public async Task<IActionResult> SignIn()
        {
            return View();
        }
    }
}
