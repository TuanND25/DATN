﻿using DATN_API.Models;
using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
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
		[HttpGet("get_all_productitem_show")]
		public async Task<IActionResult> GetAllProductItem_Show()
		{
			var a = await _productItemServices.GetAllProductItems_Show();
			return Ok(a);
		}

        [HttpGet("get_all_product_home")]
		public async Task<IActionResult> GetAllProductShowHome()
		{
			var a = await _productItemServices.GetAllProductShowHome();
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


        [HttpGet("ProductItem_By_PromotionId/{Id}")]
        public async Task<List<ProductItem_Show_VM>> GetAllProductItemPromotionItem_Show(Guid Id)
        {
            var x = await _productItemServices.GetAllProductItemPromotionItem_Show(Id);
            return x;
        }

        [HttpPost("add_productitem")]
        public async Task<IActionResult> AddProductItem(ProductItem_VM productItems)
        {
            ProductItems product  = new ProductItems();
            product.Id = productItems.Id;
            product.ProductId = productItems.ProductId;
            product.ColorId = productItems.ColorId;
            product.SizeId = productItems.SizeId;
            product.CategoryId = productItems.CategoryId;
            product.AvaiableQuantity = productItems.AvaiableQuantity;
            product.PriceAfterReduction = productItems.CostPrice;
            product.CostPrice = productItems.CostPrice;
            product.Status= productItems.Status;
            var a = await _productItemServices.AddProductItem(product);
            return Ok(a);
        }
        [HttpPut("update_productitem")]
        public async Task<IActionResult> UpdateProductItem(ProductItem_VM productItems)
        {
            ProductItems product = new ProductItems();
            product.Id = productItems.Id;
            product.ProductId = productItems.ProductId;
            product.ColorId = productItems.ColorId;
            product.SizeId = productItems.SizeId;
            product.CategoryId = productItems.CategoryId;
            product.AvaiableQuantity = productItems.AvaiableQuantity;
            product.PriceAfterReduction = productItems.PriceAfterReduction;
            product.CostPrice = productItems.CostPrice;
            product.Status = productItems.Status;
            var a = await _productItemServices.UpdateProductItem(product);
            return Ok(a);
        }
        [HttpDelete("delete_productitem")]
        public async Task<IActionResult> DeleteProductItem(Guid Id)
        {
            var a = await _productItemServices.DeleteProductItem(Id);
            return Ok();
        }

    }
}
