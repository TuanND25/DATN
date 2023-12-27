using DATN_API.Models.ViewModel.Momo;
using DATN_API.Models.ViewModel.Momo.Order;

namespace DATN_API.Service_IService.IServices
{
    public interface IMomoService
	{
		Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
	}
}
