using DATN_Shared.ViewModel.Momo;
using DATN_Shared.ViewModel.Momo.Order;

namespace DATN_API.Service_IService.IServices
{
	public interface IMomoService
	{
		Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
	}
}
