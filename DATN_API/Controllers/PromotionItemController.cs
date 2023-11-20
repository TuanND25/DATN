using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/PromotionItem")]
	[ApiController]
	public class PromotionItemController : ControllerBase
	{
		private readonly IPromotionItemServices _PromotionItem;
		public PromotionItemController(IPromotionItemServices PromotionItem)
		{
			_PromotionItem = PromotionItem;
		}

		[HttpGet]
		public async Task<List<PromotionsItem>> GetAllPromotionItem()
		{
			var PromotionItem = await _PromotionItem.GetAllPromotionsItemt();
			return PromotionItem;
		}
		[HttpGet("getPromotionItem_Percent_by_productItemID/{id}")]
		public async Task<PromotionItem_VM> GetAllPromotion(Guid id)
		{
			PromotionItem_VM x = await _PromotionItem.GetPercentPromotionItem(id);
			if (x==null)
			{
				//PromotionItem_VM promotionItem_VM = new PromotionItem_VM();
				//promotionItem_VM.Percent = -1;
				return new PromotionItem_VM();
			}
			return x;
		}

		[HttpGet("{Id}")]
		public async Task<PromotionsItem> GetPromotionItemById(Guid Id)
		{
			var x = await _PromotionItem.GetPromotionItemById(Id);
			return x;
		}

		[HttpGet("getLstPromotionItem_Percent_by_productItemID")]
		public async Task<List<PromotionItem_VM>> GetLstPercentPromotionItem()
		{
			var x = await _PromotionItem.GetLstPercentPromotionItem();
			return x;
		}
		[HttpGet("PromotionItem_By_Promotion/{Id}")]
		public async Task<List<PromotionsItem>> GetAllPromotionItemById(Guid Id)
		{
			var x = await _PromotionItem.GetAllPromotionItemById(Id);
			return x;
		}
		//public Guid Id { get; set; }
		//public Guid PromotionsId { get; set; }
		//public Guid ProductItemsId { get; set; }
		//public int Status { get; set; }
		[HttpPost("Add")]
		public async Task<ActionResult<PromotionsItem>> PostPromotionItem(PromotionItem_VM rvm)
		{
			PromotionsItem PromotionItem = new PromotionsItem();
			PromotionItem.Id = rvm.Id;
			PromotionItem.PromotionsId = rvm.PromotionsId;
			PromotionItem.ProductItemsId = rvm.ProductItemsId;
			PromotionItem.Status = rvm.Status;
			await _PromotionItem.AddPromotionsItem(PromotionItem);
			return Ok();
		}
		[HttpPut("update")]
		public async Task<ActionResult<PromotionsItem>> PutPromotionItem(PromotionItem_VM rvm)
		{
			PromotionsItem PromotionItem = await _PromotionItem.GetPromotionItemById(rvm.Id);
			PromotionItem.PromotionsId = rvm.PromotionsId;
			PromotionItem.ProductItemsId = rvm.ProductItemsId;
			PromotionItem.Status = rvm.Status;
			await _PromotionItem.UpdatePromotionsItem(PromotionItem);
			return Ok();
		}
		[HttpDelete("Id")]
		public async Task<ActionResult<PromotionsItem>> Delete(Guid id)
		{
			await _PromotionItem.DeletePromotionsItem(id);
			return Ok();
		}
		[HttpDelete("PromotionItemByProductItem/{Id}")]
		public async Task<ActionResult<PromotionsItem>> DeletePromotionItemByProductItemId(Guid Id)
		{
			await _PromotionItem.DeletePromotionItemByProductItemId(Id);
			return Ok();
		}


		[HttpDelete("PromotionItemByPromotionId/{Id}")]
		public async Task<ActionResult<PromotionsItem>> DeletePromotionItemByPomotionId(Guid Id)
		{
			await _PromotionItem.DeletePromotionItemByPomotionId(Id);
			return Ok();
		}
	}
}
