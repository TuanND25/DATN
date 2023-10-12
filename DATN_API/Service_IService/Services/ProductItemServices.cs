using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
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
