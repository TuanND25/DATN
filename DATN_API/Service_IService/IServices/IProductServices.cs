﻿using DATN_API.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IProductsServices
    {
        public Task<Products> AddProducts(Products item);
        public Task<Products> UpdateProducts(Products item);
        public Task<bool> DeleteProducts(Guid Id);
        public Task<List<Products>> GetAllProducts();
        public Task<Products> GetAllProductsById(Guid Id);
        public Task<bool> CheckProductCode_ByCode(string productCode);
    }
}
