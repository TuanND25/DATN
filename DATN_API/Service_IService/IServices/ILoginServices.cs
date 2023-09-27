using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface ILoginServices
    {
        public Task<Response> LoginAsync(LoginUser user);
    }
}
