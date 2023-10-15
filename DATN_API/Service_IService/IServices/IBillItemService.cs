using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface IBillItemService
    {
        public Task<BillItems> PostBillItems(BillItems billItems);
        public Task<BillItems> PutBillItems(BillItems billItems);
        public Task<BillItems> DeleteBillItems(Guid Id);
        public Task<BillItems> GetBillItemsById(Guid Id);
        public Task<List<BillDetailShow>> GetBillItemsByBillId(Guid BillId);
        public Task<IEnumerable<BillItems>> GetAllBillItems();
    }
}
