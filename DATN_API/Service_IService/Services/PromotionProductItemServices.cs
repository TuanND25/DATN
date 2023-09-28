using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class PromotionProductItemServices : IPromotionProductItemServices
    {
        private readonly ApplicationDbContext _context;
        public PromotionProductItemServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PromotionsProduct> AddPromotionsProduct(PromotionsProduct item)
        {
            try
            {
                var a = await _context.PromotionsProducts.AddAsync(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeletePromotionsProduct(Guid Id)
        {
            try
            {
                var a= await _context.PromotionsProducts.FindAsync(Id);
                a.Status = 0;
                _context.PromotionsProducts.Update(a);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PromotionsProduct>> GetAllPromotionProduct()
        {
            var a = await _context.PromotionsProducts.ToListAsync();
            return a;
        }

        public async Task<PromotionsProduct> GetAllPromotionsProductById(Guid Id)
        {
            var a = await _context.PromotionsProducts.FirstOrDefaultAsync(x=>x.Id==Id);
            return a;
        }

        public async Task<PromotionsProduct> GetAllPromotionsProductByPromotin(Guid PromotionId)
        {
            var a = await _context.PromotionsProducts.FirstOrDefaultAsync(x => x.PromotionsId==PromotionId);
            return a;
        }

        public async Task<PromotionsProduct> UpdatePromotionsProduct(PromotionsProduct item)
        {
            try
            {
                var a = await _context.PromotionsProducts.FindAsync(item.Id);
                _context.PromotionsProducts.Update(a);
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
