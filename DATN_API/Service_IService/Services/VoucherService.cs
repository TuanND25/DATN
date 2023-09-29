using DATN_API.Data;
using DATN_Shared.Models;
using DATN_API.Service_IService.IServices;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly ApplicationDbContext _context;
        public VoucherService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Voucher> DeleteVoucher(Guid Id)
        {
            var dte = await _context.Vouchers.FindAsync(Id);
            if (dte == null) return dte;
            _context.Remove(dte);
            await _context.SaveChangesAsync();
            return dte;
        }

        public async Task<IEnumerable<Voucher>> GetAllVoucher()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public async Task<Voucher> GetVoucherById(Guid Id)
        {
            return await _context.Vouchers.FindAsync(Id);
        }

        public Task<IEnumerable<Voucher>> GetVoucherByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Voucher> PostVoucher(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }

        public async Task<Voucher> PutVoucher(Voucher voucher)
        {
            var a = await _context.Vouchers.FindAsync(voucher.Id);
            if (a == null) return voucher;
            a.Name = voucher.Name;
            a.Code = voucher.Code;
            a.Reduced_Value = voucher.Reduced_Value;
            a.Quantity = voucher.Quantity;
            a.StartDate = voucher.StartDate;
            a.EndDate = voucher.EndDate;
            a.Discount_Conditions = voucher.Discount_Conditions;
            a.Status = voucher.Status;
            _context.Vouchers.Update(a);
            await _context.SaveChangesAsync();
            return voucher;
        }
    }
}
