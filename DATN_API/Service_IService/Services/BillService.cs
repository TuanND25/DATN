using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _context;
        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<Bill> DeleteBill(Guid Id)
        {
            try
            {
                var dte = await _context.Bills.FirstOrDefaultAsync(x => x.Id == Id);
                if (dte == null) return dte;
                _context.Bills.Remove(dte);
                await _context.SaveChangesAsync();
                return dte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Bill>> GetAllBill()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task<Bill> GetBillById(Guid Id)
        {
            return await _context.Bills.FindAsync(Id);
        }

        public async Task<IEnumerable<Bill>> GetBillByUserId(Guid UsedId)
        {
            var a = await _context.Bills.Where(x => x.UserId == UsedId).ToListAsync();
            return a;
        }

        public async Task<Bill> PostBill(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
            return bill;
        }

        public async Task<Bill> PutBill(Bill bill)
        {
            try
            {
                var a = await _context.Bills.FindAsync(bill.Id);
                if (a == null) return a;
                a=bill;
                //addressShip.UserId = a.UserId;
                //addressShip.Recipient = a.Recipient;
                //addressShip.DistrictID = a.DistrictID;
                //addressShip.ProvinceID = a.ProvinceID;
                //addressShip.WardCode = a.WardCode;
                //addressShip.ToAddress = a.ToAddress;
                //addressShip.NumberPhone = a.NumberPhone;
                //addressShip.Status = a.Status;
                _context.Bills.Update(a);
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
