using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;
        public ImageService(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<Image> DeleteImage(Guid Id)
        {
            try
            {
                var dte = await _context.Images.FirstOrDefaultAsync(x => x.Id == Id);
                if (dte == null) return dte;
                _context.Remove(dte);
                await _context.SaveChangesAsync();
                return dte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Image>> GetAddressShipByProductItemId(Guid ProductItemId)
        {
            var a = await _context.Images.Where(x => x.ProductItemId == ProductItemId).ToListAsync();
            return a;
        }

        public async Task<IEnumerable<Image>> GetAllImage()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image> GetImageById(Guid Id)
        {
            return await _context.Images.FindAsync(Id);
        }

        public async Task<Image> PostImage(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image> PutImage(Image image)
        {
            try
            {
                var a = await _context.Images.FindAsync(image.Id);
                if (a == null) return a;
                a = image;
                //addressShip.UserId = a.UserId;
                //addressShip.Recipient = a.Recipient;
                //addressShip.DistrictID = a.DistrictID;
                //addressShip.ProvinceID = a.ProvinceID;
                //addressShip.WardCode = a.WardCode;
                //addressShip.ToAddress = a.ToAddress;
                //addressShip.NumberPhone = a.NumberPhone;
                //addressShip.Status = a.Status;
                _context.Images.Update(a);
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
