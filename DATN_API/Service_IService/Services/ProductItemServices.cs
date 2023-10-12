using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class ProductItemServices : IProductItemServices
    {
        private readonly ApplicationDbContext _context;
        public ProductItemServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductItems> AddProductItem(ProductItems item)
        {
            try
            {
                var a = await _context.ProductItems.AddAsync(item);
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProductItem(Guid Id)
        {
            try
            {
                var a = await _context.ProductItems.FindAsync(Id);
                a.Status = 0;
                _context.ProductItems.Update(a);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductItems> GetAllProductItemById(Guid Id)
        {
            var a = await _context.ProductItems.FirstOrDefaultAsync(a=>a.Id==Id);
            return a;
        }

        public async Task<ProductItems> GetAllProductItemByProduct(Guid ProductId)
        {
            var a = await _context.ProductItems.FirstOrDefaultAsync(a => a.ProductId==ProductId);
            return a; 
        }

        public async Task<List<ProductItems>> GetAllProductItems()
        {
            var a = await _context.ProductItems.ToListAsync();
            return a;
        }

		public async Task<List<ProductItem_Show_VM>> GetAllProductItems_Show()
		{
			var list = (from prI in await _context.ProductItems.ToListAsync()
						join pr in await _context.Products.ToListAsync() on prI.ProductId equals pr.Id
						join s in await _context.Sizes.ToListAsync() on prI.SizeId equals s.Id
						join c in await _context.Colors.ToListAsync() on prI.ColorId equals c.Id
						join cate in await _context.Categories.ToListAsync() on prI.CategoryId equals cate.Id
						select new ProductItem_Show_VM()
						{
							Id = prI.Id,
							ProductId = prI.ProductId,
							Name = pr.Name,
							ColorId = prI.ColorId,
							ColorName = c.Name,
							SizeId = prI.SizeId,
							SizeName = s.Name,
							CategoryID = prI.CategoryId,
							CategoryName = cate.Name,
							AvaiableQuantity = prI.AvaiableQuantity,
							PurchasePrice = prI.PurchasePrice,
							CostPrice = prI.CostPrice,
							Status = prI.Status,
						}).ToList();
			return list;
		}

		public async Task<ProductItems> UpdateProductItem(ProductItems item)
        {
            try
            {
                var a = await _context.ProductItems.FindAsync(item.Id);
                a.AvaiableQuantity = item.AvaiableQuantity;
                a.CategoryId = item.CategoryId;
                a.ColorId= item.ColorId;
                a.SizeId = item.SizeId;
                a.ProductId= item.ProductId;
                a.PurchasePrice = item.PurchasePrice;
                a.CostPrice= item.CostPrice;
                a.Status = item.Status;
                _context.ProductItems.Update(a);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
