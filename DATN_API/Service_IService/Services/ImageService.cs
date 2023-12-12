using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class ImageService : IImageService
	{
		private readonly ApplicationDbContext _context;
		public ImageService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<Image> DeleteImage(Guid Id)
		{
			try
			{
				var dte = await _context.Images.FirstOrDefaultAsync(x => x.Id == Id);
				if (dte == null) return dte;
				_context.Remove(dte);
				await _context.SaveChangesAsync();
				return dte;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<IEnumerable<Image>> GetAddressShipByProductItemId(Guid ProductItemId)
		{
			var a = await _context.Images.Where(x => x.ProductItemId == ProductItemId).ToListAsync();
			return a;
		}

		public async Task<IEnumerable<Image>> GetAllImage()
		{
			return await _context.Images.ToListAsync();
		}

		public async Task<List<Image_Join_ProductItem>> GetAllImage_PrductItem()
		{
			//public Guid Id { get; set; }
			//public Guid? ReviewId { get; set; }
			//public string Name { get; set; }
			//public int STT { get; set; }
			//public string PathImage { get; set; }
			//public Guid ProductItemId { get; set; }
			//public int Status { get; set; }
			//public Guid ProductId { get; set; }

			var list = (from img in await _context.Images.ToListAsync()
						join prI in await _context.ProductItems.ToListAsync() on img.ProductItemId equals prI.Id
						select new Image_Join_ProductItem()
						{
							Id = img.Id,
							ReviewId = img.ReviewId,
							Name = img.Name,
							STT = img.STT,
							PathImage = img.PathImage,
							ProductItemId = img.ProductItemId,
							Status = img.Status,
							ProductId = prI.ProductId,
							ColorId = prI.ColorId
						}).ToList();
			return list;
		}

		public async Task<List<Image_Join_ProductItem>> GetAllImage_PrductItem_ByProductId(Guid ProductId)
		{
			var list = (from img in _context.Images
						join prI in _context.ProductItems on img.ProductItemId equals prI.Id
						select new Image_Join_ProductItem()
						{
							Id = img.Id,
							ReviewId = img.ReviewId,
							Name = img.Name,
							STT = img.STT,
							PathImage = img.PathImage,
							ProductItemId = img.ProductItemId,
							Status = img.Status,
							ProductId = prI.ProductId,
							ColorId = prI.ColorId
						}).Where(c=>c.ProductId==ProductId).ToList();
			return list;
		}

		public async Task<Image> GetImageById(Guid Id)
		{
			return await _context.Images.FindAsync(Id);
		}

		public async Task<int> GetImage_STT_Max()
		{
			try
			{
				var sttmax = await _context.Images.MaxAsync(c => c.STT);
				return sttmax + 1;
			}
			catch (Exception)
			{
				return 1;
			}
		}

		public async Task<Image> PostImage(Image image)
		{
			await _context.Images.AddAsync(image);
			await _context.SaveChangesAsync();
			return image;
		}

		public async Task<Image> PutImage(Image image)
		{
			try
			{
				var a = await _context.Images.FindAsync(image.Id);
				if (a == null) return a;
				a = image;
				//addressShip.UserId = a.UserId;
				//addressShip.Recipient = a.Recipient;
				//addressShip.DistrictID = a.DistrictID;
				//addressShip.ProvinceID = a.ProvinceID;
				//addressShip.WardCode = a.WardCode;
				//addressShip.ToAddress = a.ToAddress;
				//addressShip.NumberPhone = a.NumberPhone;
				//addressShip.Status = a.Status;
				_context.Images.Update(a);
				await _context.SaveChangesAsync();
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
