using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPointController : ControllerBase
    {
        private readonly ICustomerPointService _customerPointService;
        public CustomerPointController(ICustomerPointService customerPointService)
        {
            _customerPointService = customerPointService;
        }
		[HttpGet("getAllCustomerPoint")]
        public async Task<List<CustomerPoint_VM>> GetAllCustomerPoint()
        {
            var lst = await _customerPointService.GetAllCustomerPoint();
            return lst;
        }
        [HttpPut("putCustomerPoint")]
        public async Task<string> PutCustomerPoint(CustomerPoint_VM c)
        {
            var lst = await _customerPointService.PutCustomerPoint(c);
            return lst;
        }


    }
}
