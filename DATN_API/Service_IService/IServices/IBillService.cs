using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface IBillService
    {
        public Task<Bill> PostBill(Bill bill);
        public Task<Bill> PutBill(Bill bill);
        public Task<Bill> DeleteBill(Guid Id);
        public Task<Bill> GetBillById(Guid Id);
        public Task<IEnumerable<Bill>> GetBillByUserId(Guid UsedId);
        public Task<List<Bill_ShowModel>> GetAllBill();
    }
}
