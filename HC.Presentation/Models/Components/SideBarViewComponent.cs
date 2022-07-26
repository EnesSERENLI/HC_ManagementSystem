using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Models.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ISubCategoryService _subCategoryService;
        public SideBarViewComponent(ISubCategoryService subCategoryService) => _subCategoryService = subCategoryService;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var subCategories = await _subCategoryService.GetDefaultSubCategories();
            return View(subCategories);
        }
    }
}
