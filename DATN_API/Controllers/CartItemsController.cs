using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
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
		[HttpGet("{ID}")]
		public async Task<CartItems> GetCartItemsById(Guid ID)
		{
			var x = await _CartItems.GetCartItemsById(ID);
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
			CartItems.Price = rvm.Price;
			CartItems.Quantity = rvm.Quantity;
			CartItems.Status = rvm.Status;
			var x = await _CartItems.AddCartItems(CartItems);
			return Ok();
		}
		[HttpPut("update-CartItems")]
		public async Task<ActionResult<CartItems>> PutCartItems(CartItems_VM rvm)
		{
			CartItems CartItems = await _CartItems.GetCartItemsById(rvm.Id);
			CartItems.UserId = rvm.UserId;
			CartItems.ProductItemId = rvm.ProductItemId;
			CartItems.Quantity = rvm.Quantity;
			CartItems.Price = rvm.Price;
			CartItems.Status = rvm.Status;
			await _CartItems.UpdateCartItems(CartItems);
			return Ok();
		}
		[HttpDelete("delete-CartItems")]
		public async Task<ActionResult<CartItems>> Delete(Guid id)
		{
			await _CartItems.DeleteCartItems(id);
			return Ok();
		}
	}
}
