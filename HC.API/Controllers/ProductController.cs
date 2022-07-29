using FluentValidation;
using System.Web;
using HC.Application.Models.DTO;
using HC.Application.Models.VM;
using HC.Application.Service.Interface;
using HC.Application.Validation.FluentValidation;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetById(id);
            if(product == null)
                return NotFound();
            else
                return Ok(product);
        }
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDTO model)
        {            
            CreateProductDTOValidation validator = new CreateProductDTOValidation();
            try
            {
                validator.ValidateAndThrow(model);
                var result = await _productService.Create(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO model)
        {
            try
            {
                UpdateProductDTOValidation validator = new UpdateProductDTOValidation();
                validator.ValidateAndThrow(model);
                var result = await _productService.Update(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                string result = await _productService.Delete(id);
                return Ok(result);
            }
              
        }
    }
}
