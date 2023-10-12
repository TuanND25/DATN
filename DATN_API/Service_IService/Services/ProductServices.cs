using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class ProductServices : IProductsServices
    {
        private readonly ApplicationDbContext _context;
        public ProductServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Products> AddProducts(Products item)
        {
            try
            {
                var a= await _context.Products.AddAsync(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProducts(Guid Id)
        {
            try
            {
                var a = await _context.Products.FindAsync(Id);
                a.Status = 0;
                _context.Products.Update(a);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Products>> GetAllProducts()
        {
            var a = await _context.Products.ToListAsync();
            return a;
        }

        public async Task<Products> GetAllProductsById(Guid Id)
        {
            var a = await _context.Products.FirstOrDefaultAsync(a=>a.Id==Id);
            return a;
        }

        public async Task<Products> UpdateProducts(Products item)
        {
            try
            {
                var a = await _context.Products.FindAsync(item.Id);
                a.Status = item.Status;
                a.Name = item.Name;
                _context.Products.Update(a);
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
