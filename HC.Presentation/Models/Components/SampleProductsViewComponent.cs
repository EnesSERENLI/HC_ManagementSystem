using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HC.Presentation.Models.Components
{
    public class SampleProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public SampleProductsViewComponent(IProductService productService) => _productService = productService;
        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var products = await _productService.GetProductsByCategory(id);
            return View(products);
        }
    }
}
