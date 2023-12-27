﻿using DATN_API.Data;
using DATN_API.Models;
using DATN_API.Models.ViewModel;
using DATN_API.Service_IService.IServices;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class PromotionServices : IPromotionServices
    {
        private readonly ApplicationDbContext _context;
        public PromotionServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Promotions> AddPromotions(Promotions item)
        {
            try
            {
                var a = await _context.Promotions.AddAsync(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeletePromotions(Guid Id)
        {
            try
            {
                var a = await _context.Promotions.FindAsync(Id);
                a.Status = 0;
                _context.Promotions.Update(a);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Promotions>> GetAllPromotions()
        {
            var a = await _context.Promotions.ToListAsync();
            return a;
        }

        public async Task<Promotions> GetAllPromotionsById(Guid Id)
        {
            var a = await _context.Promotions.FirstOrDefaultAsync(x => x.Id == Id);
            return a;
        }

        public async Task<Promotions> UpdatePromotions(Promotions item)
        {
            try
            {
                var a = await _context.Promotions.FindAsync(item.Id);
                a.Status=item.Status;
                a.StartDate=item.StartDate;
                a.EndDate=item.EndDate;
                a.Description=item.Description;
                a.Name=item.Name;
                a.Percent=item.Percent;               
                _context.Promotions.Update(a);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Promotions> UpdateQuantityPromotion(Guid producitemId)
        {
            try
            {
                var a = (from b in _context.Promotions
                         join c in _context.PromotionsItem on b.Id equals c.PromotionsId
                         where c.ProductItemsId == producitemId
                         select new PromotionItem_VM
                         {
                             Id = b.Id,
                             PromotionsId = b.Id,
                             ProductItemsId = c.ProductItemsId,
                             Quantity = b.Quantity,
                         }).FirstOrDefault();
                if (a != null)
                {
                    var s = await _context.Promotions.FindAsync(a.Id);
                    s.Quantity = a.Quantity - 1;
                    _context.Promotions.Update(s);
                    _context.SaveChanges();
                    return s;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
