using DATN_API.Models.ViewModel;
using Twilio.Rest.Api.V2010.Account;

namespace DATN_API.Service_IService.IServices
{
    public interface ISignUpServices
    {
        public Task<ResponseMess> SignUpAsync(SignUpUser user);
		public Task<ResponseMess> SignUpOTPsync(SignUpUser user);
        public string RandomOTP();
        public Task<ResponseMess> SendSmsOTP(string toPhoneNumber, string message);
	}
}
