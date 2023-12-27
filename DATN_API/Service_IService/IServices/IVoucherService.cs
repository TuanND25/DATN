using DATN_API.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IVoucherService
    {
        public Task<Voucher> PostVoucher(Voucher voucher);
        public Task<Voucher> PutVoucher(Voucher voucher);
        public Task<Voucher> DeleteVoucher(Guid Id);
        public Task<Voucher> GetVoucherById(Guid Id);
        public Task<Voucher> GetVoucherByCode(string code);
        public Task<IEnumerable<Voucher>> GetVoucherByName(string name);
        public Task<IEnumerable<Voucher>> GetAllVoucher();
    }
}
