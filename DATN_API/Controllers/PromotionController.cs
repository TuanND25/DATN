using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/promotion")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionServices _promotionsServices;
        public PromotionController(IPromotionServices promotionsServices)
        {
            _promotionsServices = promotionsServices;
        }
        [HttpGet("get_all_promotion")]
        public async Task<IActionResult> GetAllPromotion()
        {
            var a = await _promotionsServices.GetAllPromotions();
            return Ok(a);
        }
        [HttpGet("get_all_promotion_byID/{Id}")]
        public async Task<IActionResult> GetPromotionById(Guid Id)
        {
            var a = await _promotionsServices.GetAllPromotionsById(Id);
            return Ok(a);
        }
        [HttpPost("add_promotion")]
        public async Task<IActionResult> AddPromotion(Promotions promotions)
        {
            var a = await _promotionsServices.AddPromotions(promotions);
            return Ok(a);
        }
        [HttpPut("update_promotion")]
        public async Task<IActionResult> UpdatePromotion(Promotions promotions)
        {
            var a = await _promotionsServices.UpdatePromotions(promotions);
            return Ok(a);
        }
        [HttpDelete("delete_promotion")]
        public async Task<IActionResult> DeletePromotion(Guid Id)
        {
            var a = await _promotionsServices.DeletePromotions(Id);
            return Ok(a);
        }
    }
}
