using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsServices _productsServices;
        public ProductController(IProductsServices productsServices)
        {
            _productsServices = productsServices;
        }
        [HttpGet("get_allProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var a = await _productsServices.GetAllProducts();
            return Ok(a);
        }

        [HttpGet("get_product_byid/{Id}")]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            var a = await _productsServices.GetAllProductsById(Id);
            return Ok(a);
        }
        [HttpPost("add_product")]
        public async Task<IActionResult> AddProduct(Products products)
        {
            var a = await _productsServices.GetAllProducts();
            return Ok(a);
        }
        [HttpPut("update_product")]
        public async Task<IActionResult> UpdatelProduct(Products products)
        {
            var a = await _productsServices.GetAllProducts();
            return Ok(a);
        }
        [HttpDelete("delete_product/{Id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var a = await _productsServices.DeleteProducts(id);
            return Ok(a);
        }

    }
}
