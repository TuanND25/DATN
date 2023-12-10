using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface ICustomerPointService
    {
        public Task<List<CustomerPoint_VM>> GetAllCustomerPoint();
        public Task<string> PutCustomerPoint(CustomerPoint_VM customerPoint_VM);
    }
}
