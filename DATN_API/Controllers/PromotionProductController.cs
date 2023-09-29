using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/promotionproduct")]
    [ApiController]
    public class PromotionProductController : ControllerBase
    {
        private readonly IPromotionProductItemServices _promotionProductItemServices;
        public PromotionProductController(IPromotionProductItemServices promotionProductItemServices)
        {
            _promotionProductItemServices = promotionProductItemServices;
        }
        [HttpGet("get_all_productitem")]
        public async Task<IActionResult> GetAllPromotionProduct()
        {
            var a = await _promotionProductItemServices.GetAllPromotionProduct();
            return Ok(a);
        }
        [HttpGet("get_all_productitem_byID/{Id}")]
        public async Task<IActionResult> GetAllPromotionProductById(Guid Id)
        {
            var a = await _promotionProductItemServices.GetAllPromotionsProductById(Id);
            return Ok(a);
        }
        [HttpGet("get_all_productitem_byProduct/{ProductId}")]
        public async Task<IActionResult> GetAllPromotionProductByPromotion(Guid ProductId)
        {
            var a = await _promotionProductItemServices.GetAllPromotionsProductByPromotin(ProductId);
            return Ok(a);
        }
        [HttpPost("add_product")]
        public async Task<IActionResult> AddPromotionItem(PromotionProduct_VM promotionsProduct)
        {
            PromotionsProduct promotions= new PromotionsProduct();
            promotions.Id = promotionsProduct.Id;
            promotions.ProductItemsId = promotionsProduct.ProductItemsId;
            promotions.PromotionsId = promotionsProduct.PromotionsId;
            promotions.Status = promotionsProduct.Status;
            var a = await _promotionProductItemServices.AddPromotionsProduct(promotions);
            return Ok(a);
        }
        [HttpPut("update_product")]
        public async Task<IActionResult> UpdatePromotionItem(PromotionProduct_VM promotionsProduct)
        {
            PromotionsProduct promotions = new PromotionsProduct();
            promotions.Id = promotionsProduct.Id;
            promotions.ProductItemsId = promotionsProduct.ProductItemsId;
            promotions.PromotionsId = promotionsProduct.PromotionsId;
            promotions.Status = promotionsProduct.Status;
            var a = await _promotionProductItemServices.UpdatePromotionsProduct(promotions);
            return Ok(a);
        }
        [HttpDelete("delete_product")]
        public async Task<IActionResult> DeletePromotionItem(Guid Id)
        {
            var a = await _promotionProductItemServices.DeletePromotionsProduct(Id);
            return Ok(a);
        }



    }
}
