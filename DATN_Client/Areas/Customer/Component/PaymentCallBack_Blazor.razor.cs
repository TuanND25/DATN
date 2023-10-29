using DATN_Client.Areas.Customer.Controllers;
using DATN_Shared.ViewModel.Momo;
using System.Net.Http;

namespace DATN_Client.Areas.Customer.Component
{
	public partial class PaymentCallBack_Blazor
	{
		HttpClient _client = new HttpClient();
		MomoExecuteResponseModel _responseModel = new MomoExecuteResponseModel();

		protected override async Task OnInitializedAsync()
		{
			_responseModel = BanOnlineController._momoExecuteResponseModel;
		}
	}
}
