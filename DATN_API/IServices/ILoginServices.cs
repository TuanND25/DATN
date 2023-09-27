
using DATN_Shared.ViewModel;

namespace DATN.IServices
{
    public interface ILoginServices
    {
        public Task<Response> LoginAsync(LoginUser user); 
    }
}
