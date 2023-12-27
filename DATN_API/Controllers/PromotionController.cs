using DATN_API.Models;
using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/promotion")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
		private readonly IPromotionServices _Promotion;
		public PromotionController(IPromotionServices Promotion)
		{
			_Promotion = Promotion;
		}

		[HttpGet]
		public async Task<List<Promotions>> GetAllPromotion()
		{
			var Promotion = await _Promotion.GetAllPromotions();
			return Promotion;
		}
		[HttpGet("{Id}")]
		public async Task<Promotions> GetPromotionById(Guid Id)
		{
			var x = await _Promotion.GetAllPromotionsById(Id);
			return x;
		}
		//public Guid Id { get; set; }
		//public string Name { get; set; }
		//public string Code { get; set; }
		//public string Percent { get; set; }
		//public int Quantity { get; set; }
		//public DateTime StartDate { get; set; }
		//public DateTime EndDate { get; set; }
		//public string Description { get; set; }
		//public string Discount_Conditions { get; set; }
		//public int Status { get; set; }
		[HttpPost("Add")]
		public async Task<ActionResult<Promotions>> PostPromotion(Promotions_VM rvm)
		{
			Promotions Promotion = new Promotions();
			Promotion.Id = rvm.Id;
			Promotion.Name = rvm.Name;
			Promotion.Percent = rvm.Percent;
			Promotion.Quantity = rvm.Quantity;
			Promotion.StartDate = rvm.StartDate;
			Promotion.EndDate = rvm.EndDate;
			Promotion.Description = rvm.Description;
			Promotion.Status = rvm.Status;
			await _Promotion.AddPromotions(Promotion);
			return Ok();
		}
		[HttpPut("update")]
		public async Task<ActionResult<Promotions>> PutPromotion(Promotions_VM rvm)
		{
			Promotions Promotion = await _Promotion.GetAllPromotionsById(rvm.Id);
			Promotion.Name = rvm.Name;
			Promotion.Percent = rvm.Percent;
			Promotion.Quantity = rvm.Quantity;
			Promotion.StartDate = rvm.StartDate;
			Promotion.EndDate = rvm.EndDate;
			Promotion.Description = rvm.Description;
			Promotion.Status = rvm.Status;
			await _Promotion.UpdatePromotions(Promotion);
			return Ok();
		}

		[HttpPut("update_quantity_promotion/{productitemId}")]
        public async Task<ActionResult<Promotions>> UpdatePromotionQuantity(Guid productitemId)
        {
            await _Promotion.UpdateQuantityPromotion(productitemId);
			return Ok();
        }
        [HttpDelete("Id")]
		public async Task<ActionResult<Promotions>> Delete(Guid id)
		{
			await _Promotion.DeletePromotions(id);
			return Ok();
		}
	}
}
