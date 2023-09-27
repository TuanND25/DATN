using DATN_Shared.ViewModel;

namespace DATN.IServices
{
    public interface ISignUpServices
    {
        public Task<Response> SignUpAsync(SignUpUser user, string role);
    }
}
