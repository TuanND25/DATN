using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface IImageService
    {
        public Task<Image> PostImage(Image image);
        public Task<Image> PutImage(Image image);
        public Task<Image> DeleteImage(Guid Id);
        public Task<Image> GetImageById(Guid Id);
        public Task<IEnumerable<Image>> GetAddressShipByProductItemId(Guid ProductItemId);
        public Task<IEnumerable<Image>> GetAllImage();
        public Task<List<Image_Join_ProductItem>> GetAllImage_PrductItem();
        public Task<List<Image_Join_ProductItem>> GetAllImage_PrductItem_ByProductId(Guid ProductId);
        public Task<int> GetImage_STT_Max();
    }
}
