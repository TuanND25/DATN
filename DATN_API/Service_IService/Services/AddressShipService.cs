using DATN_API.Data;
using DATN_API.Models;
using DATN_API.Service_IService.IServices;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class AddressShipService : IAddressShipService
    {
        private readonly ApplicationDbContext _context;
        public AddressShipService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AddressShip> DeleteAddressShip(Guid Id)
        {
            try
            {
                var dte = await _context.AddressShips.FirstOrDefaultAsync(x => x.Id == Id);
                if (dte == null) return dte;
                _context.AddressShips.Remove(dte);
                await _context.SaveChangesAsync();
                return dte;
            }
            catch (Exception ex)
            {
                return null;
            }
             
        }

        public async Task<AddressShip> GetAddressShipById(Guid Id)
        {
            return await _context.AddressShips.FindAsync(Id);
        }

        public async Task<IEnumerable<AddressShip>> GetAddressShipByUserId(Guid UsedId)
        {
            var a = await _context.AddressShips.Where(x =>x.UserId == UsedId).ToListAsync();    
            return a;
        }

        public async Task<IEnumerable<AddressShip>> GetAllAddressShip()
        {
            return await _context.AddressShips.ToListAsync();
        }

        public async Task<AddressShip> PostAddressShip(AddressShip addressShip)
        {
           await _context.AddressShips.AddAsync(addressShip);   
           await _context.SaveChangesAsync();
           return addressShip;
        }

        public async Task<AddressShip> PutAddressShip(AddressShip addressShip)
        {
            try
            {
                var a = await _context.AddressShips.FindAsync(addressShip.Id);
                if (a == null) return a;
                addressShip = a;
                //addressShip.Recipient = a.Recipient;
                //addressShip.DistrictID = a.DistrictID;
                //addressShip.ProvinceID = a.ProvinceID;
                //addressShip.WardCode = a.WardCode;
                //addressShip.ToAddress = a.ToAddress;
                //addressShip.NumberPhone = a.NumberPhone;
                //addressShip.Status = a.Status;
                _context.AddressShips.Update(addressShip);
                await _context.SaveChangesAsync();
                return addressShip;
            }
            catch (Exception ex) 
            {
                return null;
            }

        }
    }
}
