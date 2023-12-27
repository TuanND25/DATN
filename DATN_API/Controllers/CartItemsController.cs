using DATN_API.Models;
using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/CartItems")]
	[ApiController]
	public class CartItemsController : ControllerBase
	{
		private readonly ICartItemsService _CartItems;
		public CartItemsController(ICartItemsService CartItems)
		{
			_CartItems = CartItems;
		}

		[HttpGet("get-CartItems")]
		public async Task<List<CartItems>> GetAllCartItems()
		{
			var CartItems = await _CartItems.GetAllCartItems();
			return CartItems;
		}
		[HttpGet("{UserID}")]
		public async Task<List<CartItems>> GetCartItemsByUserId(Guid UserID)
		{
			var x = await _CartItems.GetAllCartItemsByUserId(UserID);
			return x;
		}
		//public Guid Id { get; set; }
		//public Guid UserId { get; set; }
		//public Guid CartId { get; set; }
		//public Guid ProductItemId { get; set; }
		//public string Name { get; set; }
		//public int Status { get; set; }
		[HttpPost("add-CartItems")]
		public async Task<ActionResult<CartItems>> PostCartItems(CartItems_VM rvm)
		{
			CartItems CartItems = new CartItems();
			CartItems.Id = rvm.Id;
			CartItems.UserId = rvm.UserId;			
			CartItems.ProductItemId = rvm.ProductItemId;
			CartItems.Quantity = rvm.Quantity;
			CartItems.Status = rvm.Status;
			var x = await _CartItems.AddCartItems(CartItems);
			return Ok();
		}
		[HttpPut("update-CartItems")]
		public async Task<ActionResult<CartItems>> PutCartItems(CartItems_VM rvm)
		{
			CartItems CartItems = (await _CartItems.GetAllCartItems()).FirstOrDefault(c=>c.Id==rvm.Id);
			CartItems.UserId = rvm.UserId;
			CartItems.ProductItemId = rvm.ProductItemId;
			CartItems.Quantity = rvm.Quantity;
			CartItems.Status = rvm.Status;
			await _CartItems.UpdateCartItems(CartItems);
			return Ok();
		}
		[HttpDelete("delete-CartItems/{Id}")]
		public async Task<ActionResult<CartItems>> Delete(Guid Id)
		{
			await _CartItems.DeleteCartItems(Id);
			return Ok();
		}
	}
}
