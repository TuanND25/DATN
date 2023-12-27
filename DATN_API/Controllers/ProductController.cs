using DATN_API.Models;
using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
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
		[HttpGet("check_productCode_byCode")]
		public async Task<IActionResult> CheckProductCode_ByCode(string productCode)
		{
			var a = await _productsServices.CheckProductCode_ByCode(productCode);
			return Ok(a);
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
		public async Task<IActionResult> AddProduct(Products_VM products)
		{
			Products pr = new Products();
			pr.Id = products.Id;
			pr.Name = products.Name;
			pr.ProductCode = products.ProductCode;
			pr.Description = products.Description;
			pr.Status = products.Status;
			var a = await _productsServices.AddProducts(pr);
			return Ok(a);
		}
		[HttpPut("update_product")]
		public async Task<IActionResult> UpdatelProduct(Products_VM products)
		{
			Products pr = new Products();
			pr.Id = products.Id;
			pr.Name = products.Name;
			pr.ProductCode = products.ProductCode;
			pr.Description = products.Description;
			pr.Status = products.Status;
			var a = await _productsServices.UpdateProducts(pr);
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
