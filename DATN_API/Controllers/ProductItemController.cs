using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/productitem")]
    [ApiController]
    public class ProductItemController : ControllerBase
    {
        private readonly IProductItemServices _productItemServices;
        public ProductItemController(IProductItemServices productItemServices)
        {
            _productItemServices = productItemServices;
        }
        [HttpGet("get_all_productitem")]
        public async Task<IActionResult> GetAllProductItem()
        {
            var a = await _productItemServices.GetAllProductItems();
            return Ok(a);
        }
        [HttpGet("get_all_productitem_byID/{Id}")]
        public async Task<IActionResult> GetAllProductItemById(Guid Id)
        {
            var a = await _productItemServices.GetAllProductItemById(Id);
            return Ok(a);
        }
        [HttpGet("get_all_productitem_byProduct/{ProductId}")]
        public async Task<IActionResult> GetAllProductItemByProduct(Guid ProductId)
        {
            var a = await _productItemServices.GetAllProductItemByProduct(ProductId);
            return Ok(a);
        }
        [HttpPost("add_productitem")]
        public async Task<IActionResult> AddProductItem(ProductItems productItems)
        {
            var a = await _productItemServices.AddProductItem(productItems);
            return Ok(productItems);
        }
        [HttpPut("update_productitem")]
        public async Task<IActionResult> UpdateProductItem(ProductItems productItems)
        {
            var a = await _productItemServices.UpdateProductItem(productItems);
            return Ok(productItems);
        }
        [HttpDelete("delete_productitem")]
        public async Task<IActionResult> DeleteProductItem(Guid Id)
        {
            var a = await _productItemServices.DeleteProductItem(Id);
            return Ok();
        }

    }
}
