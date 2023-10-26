using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface ISignUpServices
    {
        public Task<Response> SignUpAsync(SignUpUser user);
    }
}
