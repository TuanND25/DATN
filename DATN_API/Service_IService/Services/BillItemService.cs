using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class BillItemService : IBillItemService
    {
        private readonly ApplicationDbContext _context;
        public BillItemService(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task<BillItems> DeleteBillItems(Guid Id)
        {
            try
            {
                var a = await _context.BillItems.FindAsync(Id);
                if (a == null) return a;
                a.Status = 0;
                //addressShip.UserId = a.UserId;
                //addressShip.Recipient = a.Recipient;
                //addressShip.DistrictID = a.DistrictID;
                //addressShip.ProvinceID = a.ProvinceID;
                //addressShip.WardCode = a.WardCode;
                //addressShip.ToAddress = a.ToAddress;
                //addressShip.NumberPhone = a.NumberPhone;
                //addressShip.Status = a.Status;
                _context.BillItems.Update(a);
                await _context.SaveChangesAsync();
                return a;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<BillItems>> GetAllBillItems()
        {
            return await _context.BillItems.ToListAsync();
        }

        public async Task<IEnumerable<BillItems>> GetBillItemsByBillId(Guid BillId)
        {
            var a = await _context.BillItems.Where(x => x.BillId == BillId).ToListAsync();
            return a;
        }

        public async Task<BillItems> GetBillItemsById(Guid Id)
        {
            return await _context.BillItems.FindAsync(Id);
        }

        public async Task<BillItems> PostBillItems(BillItems billItems)
        {
            await _context.BillItems.AddAsync(billItems);
            await _context.SaveChangesAsync();
            return billItems;
        }

        public async Task<BillItems> PutBillItems(BillItems billItems)
        {
            try
            {
                var a = await _context.BillItems.FindAsync(billItems.Id);
                if (a == null) return a;
                a = billItems;
                //addressShip.UserId = a.UserId;
                //addressShip.Recipient = a.Recipient;
                //addressShip.DistrictID = a.DistrictID;
                //addressShip.ProvinceID = a.ProvinceID;
                //addressShip.WardCode = a.WardCode;
                //addressShip.ToAddress = a.ToAddress;
                //addressShip.NumberPhone = a.NumberPhone;
                //addressShip.Status = a.Status;
                _context.BillItems.Update(a);
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
