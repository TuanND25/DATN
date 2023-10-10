using DATN_API.Data;
using DATN_Shared.Models;
using DATN_API.Service_IService.IServices;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class SizeService : ISizeService
    {
        private readonly ApplicationDbContext _context;
        public SizeService(ApplicationDbContext context)
        {
            _context = context;                    
        }
        public async Task<Size> DeleteSize(Guid Id)
        {
            var dte = await _context.Sizes.FirstOrDefaultAsync(x => x.Id == Id);
            if (dte == null) return dte;
            _context.Remove(dte);
            await _context.SaveChangesAsync();
            return dte;
        }

        public async Task<IEnumerable<Size>> GetAllSize()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size> GetSizeById(Guid Id)
        {
            return await _context.Sizes.FindAsync(Id);
        }

        public Task<IEnumerable<Size>> GetSizeByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Size> PostSize(Size size)
        {
            _context.Sizes.Add(size);
            await _context.SaveChangesAsync();
            return size;
        }
        public async Task<Size> PutSize(Size size)
        {
            var put = await _context.Sizes.FindAsync(size.Id);
            if (put == null) return put;
            //_context.Update(put);
            put.Name = size.Name;
            put.Status = size.Status;
            await _context.SaveChangesAsync();
            return put;
        }
    }
}
