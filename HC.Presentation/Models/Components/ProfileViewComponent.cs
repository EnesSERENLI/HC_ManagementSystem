using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Models.Components
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        public ProfileViewComponent(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var user = await _appUserService.GetByUser(userName);
            return View(user);
        }
    }
}
