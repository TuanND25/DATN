using DATN_API.Models;
using DATN_API.Models.ViewModel;

namespace DATN_API.Service_IService.IServices
{
	public interface IBillItemService
    {
        public Task<BillItems> PostBillItems(BillItems billItems);
        public Task<BillItems> PutBillItems(BillItems billItems);
        public Task<BillItems> DeleteBillItems(Guid Id);
        public Task<BillItems> GetBillItemsById(Guid Id);
        public Task<List<BillDetailShow>> GetBillItemsByBillId(Guid BillId);
        public Task<List<BillItems>> GetBillItemsByBillId_billitemdb(Guid BillId);
        public Task<List<BillDetailShow>> GetBillItemsShow();
        public Task<List<BillDetailShow>> GetAllBillItemsByUserId(Guid UserId);
        public Task<IEnumerable<BillItems>> GetAllBillItems();
    }
}
