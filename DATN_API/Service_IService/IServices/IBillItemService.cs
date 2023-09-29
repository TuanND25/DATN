using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IBillItemService
    {
        public Task<BillItems> PostBillItems(BillItems billItems);
        public Task<BillItems> PutBillItems(BillItems billItems);
        public Task<BillItems> DeleteBillItems(Guid Id);
        public Task<BillItems> GetBillItemsById(Guid Id);
        public Task<IEnumerable<BillItems>> GetBillItemsByBillId(Guid BillId);
        public Task<IEnumerable<BillItems>> GetAllBillItems();
    }
}
