using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Models.Components
{
    public class LayoutNavbarViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        public LayoutNavbarViewComponent(IAppUserService appUserService) => _appUserService = appUserService;
        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            if (userName != null)
            {
                var user = await _appUserService.GetByUser(userName);
                return View(user);
            }
            return View(new AppUserVM());
        }
    }
}
