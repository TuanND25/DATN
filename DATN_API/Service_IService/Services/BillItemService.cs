using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class BillItemService : IBillItemService
	{
		private readonly ApplicationDbContext _context;
		public BillItemService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<BillItems> DeleteBillItems(Guid Id)
		{
			try
			{
				var a = await _context.BillItems.FindAsync(Id);
				if (a == null) return a;
				a.Status = 0;
				//addressShip.UserId = a.UserId;
				//addressShip.Recipient = a.Recipient;
				//addressShip.DistrictID = a.DistrictID;
				//addressShip.ProvinceID = a.ProvinceID;
				//addressShip.WardCode = a.WardCode;
				//addressShip.ToAddress = a.ToAddress;
				//addressShip.NumberPhone = a.NumberPhone;
				//addressShip.Status = a.Status;
				_context.BillItems.Remove(a);
				await _context.SaveChangesAsync();
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<IEnumerable<BillItems>> GetAllBillItems()
		{
			return await _context.BillItems.ToListAsync();
		}

		public async Task<List<BillDetailShow>> GetBillItemsByBillId(Guid BillId)
		{
			var _lst = (from a in _context.BillItems
						join b in _context.Bills on a.BillId equals b.Id
						join c in _context.ProductItems on a.ProductItemsId equals c.Id
						join q in _context.Products on c.ProductId equals q.Id
						join d in _context.Colors on c.ColorId equals d.Id
						join e in _context.Sizes on c.SizeId equals e.Id
						join f in _context.Categories on c.CategoryId equals f.Id
						join g in _context.PaymentMethods on b.PaymentMethodId equals g.Id
						where a.BillId == BillId
						select new BillDetailShow()
						{
							Id = a.Id,
							BillID = b.Id,
							ProductItemId = c.Id,
							ProductId = c.ProductId,
							ProductCode = q.ProductCode,
							Name = q.Name,
							ColorId = d.Id,
							ColorName = d.Name,
							SizeId = e.Id,
							SizeName = e.Name,
							CategoryID = f.Id,
							CategoryName = f.Name,
							Quantity = a.Quantity,
							PriceAfter = a.Price,
							CostPrice = c.CostPrice,
							Status = a.Status,
							PaymentMethod=g.Name,
						}).ToList();
			return _lst;
		}




		public async Task<List<BillDetailShow>> GetAllBillItemsByUserId(Guid UserId)
		{
			var _lst = (from a in _context.BillItems
						join b in _context.Bills on a.BillId equals b.Id
						join c in _context.ProductItems on a.ProductItemsId equals c.Id
						join q in _context.Products on c.ProductId equals q.Id
						join d in _context.Colors on c.ColorId equals d.Id
						join e in _context.Sizes on c.SizeId equals e.Id
						join f in _context.Categories on c.CategoryId equals f.Id
						join g in _context.PaymentMethods on b.PaymentMethodId equals g.Id
						where b.UserId == UserId
						select new BillDetailShow()
						{
							Id = a.Id,
							BillID = b.Id,
							ProductItemId = c.Id,
							ProductCode=q.ProductCode,
							Name = q.Name,
							ColorId = d.Id,
							ColorName = d.Name,
							SizeId = e.Id,
							SizeName = e.Name,
							CategoryID = f.Id,
							CategoryName = f.Name,
							Quantity = a.Quantity,
							PriceAfter = a.Price,
							CostPrice = c.CostPrice,
							Status = a.Status,
							PaymentMethod = g.Name
						}).ToList();
			return _lst;
		}
		public async Task<BillItems> GetBillItemsById(Guid Id)
		{
			return await _context.BillItems.FindAsync(Id);
		}

		public async Task<BillItems> PostBillItems(BillItems billItems)
		{
			await _context.BillItems.AddAsync(billItems);
			await _context.SaveChangesAsync();
			return billItems;
		}

		public async Task<BillItems> PutBillItems(BillItems billItems)
		{
			try
			{
				var a = await _context.BillItems.FindAsync(billItems.Id);
				if (a == null) return a;
				a = billItems;
				//addressShip.UserId = a.UserId;
				//addressShip.Recipient = a.Recipient;
				//addressShip.DistrictID = a.DistrictID;
				//addressShip.ProvinceID = a.ProvinceID;
				//addressShip.WardCode = a.WardCode;
				//addressShip.ToAddress = a.ToAddress;
				//addressShip.NumberPhone = a.NumberPhone;
				//addressShip.Status = a.Status;
				_context.BillItems.Update(a);
				await _context.SaveChangesAsync();
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

        public async Task<List<BillDetailShow>> GetBillItemsShow()
        {
			var _lst = (from a in _context.BillItems
						join b in _context.Bills on a.BillId equals b.Id
						join c in _context.ProductItems on a.ProductItemsId equals c.Id
						join q in _context.Products on c.ProductId equals q.Id
						join d in _context.Colors on c.ColorId equals d.Id
						join e in _context.Sizes on c.SizeId equals e.Id
						join f in _context.Categories on c.CategoryId equals f.Id
						join g in _context.PaymentMethods on b.PaymentMethodId equals g.Id
						select new BillDetailShow()
						{
							Id = a.Id,
							BillID = b.Id,
							ProductItemId = c.Id,
							ProductCode = q.ProductCode,
							Name = q.Name,
							ColorId = d.Id,
							ColorName = d.Name,
							SizeId = e.Id,
							SizeName = e.Name,
							CategoryID = f.Id,
							CategoryName = f.Name,
							Quantity = a.Quantity,
							PriceAfter = a.Price,
							CostPrice = c.CostPrice,
							Status = a.Status,
							PaymentMethod = g.Name,
							CreateDate = b.CreateDate
						}).ToList();
			return _lst;
		}
    }
}
