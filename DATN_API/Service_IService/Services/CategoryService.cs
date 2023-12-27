using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DATN_API.Models;

namespace DATN_API.Service_IService.Services
{
	public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> DeleteCategory(Guid Id)
        {
            try
            {
                var dte = await _context.Categories.FindAsync(Id);
                if (dte == null) return dte;
                _context.Categories.Remove(dte);
                await _context.SaveChangesAsync();
                return dte;
            }
            catch (Exception ex)
            {
                return null ;
            }
            
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Categories.ToListAsync(); 
        }

        public async Task<Category> GetCategoryById(Guid Id)
        {
            return await _context.Categories.FindAsync(Id);
        }

        public Task<IEnumerable<Category>> GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> PostCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Category> PutCategory(Category category)
        {
            try
            {
                var a = await _context.Categories.FindAsync(category.Id);
                if (a == null) return category;
                a = category;
                //a.Name = category.Name;
                //a.Status = category.Status;
                _context.Categories.Update(a);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ) 
            {
                return null;
            }
           
        }
    }
}
