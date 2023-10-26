using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class CartItemsService : ICartItemsService
	{
		public ApplicationDbContext context;
		public CartItemsService(ApplicationDbContext _context)
		{
			context = _context;
		}
		public async Task<CartItems> AddCartItems(CartItems CartItems)
		{
			try
			{
				var a = await context.CartItems.AddAsync(CartItems);
				context.SaveChanges();
				return CartItems;
			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<bool> DeleteCartItems(Guid Id)
		{
			try
			{
				var a = await context.CartItems.FindAsync(Id);
				context.CartItems.Remove(a);
				context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public async Task<List<CartItems>> GetAllCartItems()
		{
			var a = await context.CartItems.ToListAsync();
			return a;
		}

		public async Task<List<CartItems>> GetAllCartItemsByUserId(Guid UserID)
		{
			var a = await context.CartItems.Where(c=>c.UserId==UserID).ToListAsync();
			return a;
		}

		public async Task<CartItems> UpdateCartItems(CartItems CartItems)
		{
			try
			{
				var a = await context.CartItems.FindAsync(CartItems.Id);
				a = CartItems;
				//a.UserId = CartItems.UserId;
				//a.ProductItemId = CartItems.ProductItemId;
				//a.Price = CartItems.Price;
				//a.Quantity = CartItems.Quantity;
				//a.Status = CartItems.Status;
				context.CartItems.Update(a);
				context.SaveChanges();
				return a;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
