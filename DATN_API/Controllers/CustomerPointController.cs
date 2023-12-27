﻿using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
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
		[HttpGet("GetCustomerPoint_byUserID/{UserID}")]
		public async Task<CustomerPoint_VM> GetCustomerPoint_byUserID(Guid UserID)
		{
			var lst = await _customerPointService.GetCustomerPoint_byUserID(UserID);
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
