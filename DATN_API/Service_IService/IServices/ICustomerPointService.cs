﻿using DATN_API.Models.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface ICustomerPointService
    {
        public Task<List<CustomerPoint_VM>> GetAllCustomerPoint();
        public Task<CustomerPoint_VM> GetCustomerPoint_byUserID(Guid UserID);
        public Task<string> PutCustomerPoint(CustomerPoint_VM customerPoint_VM);
    }
}
