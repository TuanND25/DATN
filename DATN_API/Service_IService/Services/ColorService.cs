using DATN_API.Service_IService.IServices;
using DATN_API.Data;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;
        public ColorService(ApplicationDbContext context)
        {
            _context = context;  
        }
        public async Task<Color> DeleteColor(Guid Id)
        {
            var dte = await _context.Colors.FindAsync(Id);
            if (dte == null) return dte;
            _context.Remove(dte);
            await _context.SaveChangesAsync();
            return dte;
        }

        public async Task<IEnumerable<Color>> GetAllColor()
        {
            return await _context.Colors.ToListAsync(); 
        }

        public async Task<Color> GetColorById(Guid Id)
        {
            return await _context.Colors.FindAsync(Id);
        }

        public Task<IEnumerable<Color>> GetColorByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Color> PostColor(Color color)
        {
            _context.Colors.Add (color);    
            await _context.SaveChangesAsync (); 
            return color;
        }

        public async Task<Color> PutColor(Color color)
        {
            var a = await _context.Colors.FindAsync (color.Id);
            if (a == null ) return color;
            a = color;
            //a.Name = color.Name;
            //a.Status = color.Status;
            _context.Colors.Update (a);
            await _context.SaveChangesAsync();
            return color;
        }
    }
}
