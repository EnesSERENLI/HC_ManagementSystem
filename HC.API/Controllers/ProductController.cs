using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HC.API.Controllers
{
    [Route("api/[controller]s")] //Using s in API controllers can be nice for clarity.
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetDefaultProducts());
        }
    }
}
