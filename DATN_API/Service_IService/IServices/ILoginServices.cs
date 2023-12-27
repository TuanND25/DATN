using DATN_API.Models.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface ILoginServices
    {
        public Task<ResponseMess> LoginAsync(LoginUser user);
    }
}
