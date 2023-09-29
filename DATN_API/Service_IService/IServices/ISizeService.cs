using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface ISizeService
    {
        public Task<Size> PostSize(Size size);
        public Task<Size> PutSize(Size size);
        public Task<Size> DeleteSize(Guid Id);
        public Task<Size> GetSizeById(Guid Id);
        public Task<IEnumerable<Size>> GetSizeByName(string name);
        public Task<IEnumerable<Size>> GetAllSize();
    }
}
