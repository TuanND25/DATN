using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
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
        public async Task<IActionResult> AddPromotion(Promotions_VM promotions)
        {
            Promotions promotions1 = new Promotions();
            promotions1.Id = promotions.Id;
            promotions1.Name = promotions.Name;
            promotions1.Percent = promotions.Percent;
            promotions1.StartDate = promotions.StartDate;
            promotions1.EndDate = promotions.EndDate;
            promotions1.Description = promotions.Description;
            promotions1.Status = promotions.Status;

            var a = await _promotionsServices.AddPromotions(promotions1);
            return Ok(a);
        }
        [HttpPut("update_promotion")]
        public async Task<IActionResult> UpdatePromotion(Promotions_VM promotions)
        {
            Promotions promotions1 = new Promotions();
            promotions1.Id = promotions.Id;
            promotions1.Name = promotions.Name;
            promotions1.Percent = promotions.Percent;
            promotions1.StartDate = promotions.StartDate;
            promotions1.EndDate = promotions.EndDate;
            promotions1.Description = promotions.Description;
            promotions1.Status = promotions.Status;
            var a = await _promotionsServices.UpdatePromotions(promotions1);
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
