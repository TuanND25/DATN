using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class CartService : ICartService
	{
		public ApplicationDbContext context;
		public CartService(ApplicationDbContext _context)
		{
			context = _context;
		}
		public async Task<Cart> AddCart(Cart Cart)
		{
			try
			{
				var a = await context.Carts.AddAsync(Cart);
				context.SaveChanges();
				return Cart;
			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<bool> DeleteCart(Guid Id)
		{
			try
			{
				var a = await context.Carts.FindAsync(Id);
				context.Carts.Remove(a);
				context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public async Task<List<Cart>> GetAllCart()
		{
			var a = await context.Carts.ToListAsync();
			return a;
		}
		public async Task<Cart> GetCartByUserId(Guid UserId)
		{
			var x = await context.Carts.FirstOrDefaultAsync(c => c.UserId == UserId);
			return x;
		}
		public async Task<List<Cart>> GetAllCartByUserId(Guid UserId)
		{
			var x = await context.Carts.Where(c => c.UserId == UserId).ToListAsync();
			return x;
		}
		//public Guid UserId { get; set; }
		//public string Description { get; set; }
		//public int Status { get; set; }
		public async Task<Cart> UpdateCart(Cart Cart)
		{
			try
			{
				var a = await context.Carts.FindAsync(Cart.UserId);
				a = Cart;
				//a.Description = Cart.Description;
				//a.Status = Cart.Status;
				context.Carts.Update(a);
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
