using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class PromotionItemServices : IPromotionItemServices
	{
		private readonly ApplicationDbContext _context;
		public PromotionItemServices(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<PromotionsItem> AddPromotionsItem(PromotionsItem item)
		{
			try
			{
				var a = await _context.PromotionsItem.AddAsync(item);
				_context.SaveChanges();
				return item;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> DeletePromotionItemByPomotionId(Guid Id)
		{
			try
			{
				var a = await _context.PromotionsItem.FirstOrDefaultAsync(x => x.PromotionsId == Id);
				_context.PromotionsItem.Remove(a);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeletePromotionItemByProductItemId(Guid Id)
		{
			try
			{
				var a = await _context.PromotionsItem.FirstOrDefaultAsync(x => x.ProductItemsId == Id);
				_context.PromotionsItem.Remove(a);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<bool> DeletePromotionsItem(Guid Id)
		{
			try
			{
				var a = await _context.PromotionsItem.FindAsync(Id);
				a.Status = 0;
				_context.PromotionsItem.Update(a);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<List<PromotionsItem>> GetAllPromotionItemById(Guid Id)
		{
			var x = await _context.PromotionsItem.Where(c => c.PromotionsId == Id).ToListAsync();
			return x;
		}

		public async Task<List<PromotionsItem>> GetAllPromotionsItemt()
		{
			var a = await _context.PromotionsItem.ToListAsync();
			return a;
		}

		public async Task<PromotionsItem> GetAllPromotionsItemById(Guid Id)
		{
			var a = await _context.PromotionsItem.FirstOrDefaultAsync(x => x.Id == Id);
			return a;
		}

		public async Task<PromotionsItem> GetAllPromotionsItemByPromotin(Guid PromotionId)
		{
			var a = await _context.PromotionsItem.FirstOrDefaultAsync(x => x.PromotionsId == PromotionId);
			return a;
		}


		public Task<PromotionsItem> GetPromotionItemById(Guid Id)
		{
			throw new NotImplementedException();
		}

		public async Task<PromotionsItem> UpdatePromotionsItem(PromotionsItem item)
		{
			try
			{
				var a = await _context.PromotionsItem.FindAsync(item.Id);
				a.ProductItemsId = item.ProductItemsId;
				a.PromotionsId = item.PromotionsId;
				a.Status = item.Status;
				_context.PromotionsItem.Update(a);
				_context.SaveChanges();
				return item;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<PromotionItem_VM> GetPercentPromotionItem(Guid id)
		{
			var _lst = await (from a in _context.PromotionsItem
							  join b in _context.Promotions on a.PromotionsId equals b.Id
							  join c in _context.ProductItems on a.ProductItemsId equals c.Id
							  select new PromotionItem_VM
							  {
								  Id = a.Id,
								  ProductItemsId = a.ProductItemsId,
								  Percent = b.Percent,
								  PromotionsId = a.Id,
								  Status = a.Status,
								  ProductId = c.ProductId,
								  Quantity = b.Quantity
							  }).FirstOrDefaultAsync(a => (a.ProductItemsId == id || a.ProductId == id)
														&& a.Status == 1);
			return _lst;
		}

		public async Task<List<PromotionItem_VM>> GetLstPercentPromotionItem()
		{
			var _lst = await (from a in _context.PromotionsItem
							  join b in _context.Promotions on a.PromotionsId equals b.Id
							  join c in _context.ProductItems on a.ProductItemsId equals c.Id
							  select new PromotionItem_VM
							  {
								  Id = a.Id,
								  ProductItemsId = a.ProductItemsId,
								  Percent = b.Percent,
								  PromotionsId = a.Id,
								  Status = a.Status,
								  ProductId = c.ProductId
							  }).ToListAsync();
			return _lst;
		}


	}
}
