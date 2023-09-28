using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/Cart")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService _Cart;
		public CartController(ICartService Cart)
		{
			_Cart = Cart;
		}

		[HttpGet("get-cart")]
		public async Task<List<Cart>> GetAllCart()
		{
			var Cart = await _Cart.GetAllCart();
			return Cart;
		}
		[HttpGet("{UserID}")]
		public async Task<Cart> GetCartById(Guid UserID)
		{
			var x = await _Cart.GetCartByUserId(UserID);
			return x;
		}
		//public Guid UserId { get; set; }
		//public string Description { get; set; }
		//public int Status { get; set; }
		[HttpPost("add-cart")]
		public async Task<ActionResult<Cart>> PostCart(Cart_VM rvm)
		{
			Cart Cart = new Cart();
			Cart.UserId = rvm.UserId;
			Cart.Description = rvm.Description;
			Cart.Status = rvm.Status;
			await _Cart.AddCart(Cart);
			return Ok();
		}
		[HttpPut("update-cart")]
		public async Task<ActionResult<Cart>> PutCart(Cart_VM rvm)
		{
			Cart Cart = await _Cart.GetCartByUserId(rvm.UserId);
			Cart.Description = rvm.Description;
			Cart.Status = rvm.Status;
			await _Cart.UpdateCart(Cart);
			return Ok();
		}
		[HttpDelete("delete-cart")]
		public async Task<ActionResult<Cart>> Delete(Guid id)
		{
			Cart Cart = await _Cart.GetCartByUserId(id);
			Cart.Status = 0;
			await _Cart.UpdateCart(Cart);
			return Ok();
		}
	}
}
