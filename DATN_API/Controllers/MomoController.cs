using DATN_API.Models.ViewModel.Momo;
using DATN_API.Models.ViewModel.Momo.Order;
using DATN_API.Service_IService.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/Momo")]
	[ApiController]
	public class MomoController : ControllerBase
	{
		private IMomoService _momoService;

		public MomoController(IMomoService momoService)
		{
			_momoService = momoService;
		}
		[HttpPost("CreatePaymentAsync")]
		public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model)
		{
			try
			{
				var x = await _momoService.CreatePaymentAsync(model);
				return x;
			}
			catch (Exception e)
			{

				return null;
			}			
		}
	}
}
