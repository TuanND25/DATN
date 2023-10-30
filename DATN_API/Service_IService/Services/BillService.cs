using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class BillService : IBillService
	{
		private readonly ApplicationDbContext _context;
		public BillService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Bill> DeleteBill(Guid Id)
		{
			try
			{
				var a = await _context.Bills.FindAsync(Id);
				if (a == null) return a;
				a.Status = 0;

				_context.Bills.Update(a);
				await _context.SaveChangesAsync();
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<List<Bill_ShowModel>> GetAllBill()
		{
			var lst = (from a in _context.Bills
					   join b in _context.Users on a.UserId equals b.Id
					   join c in _context.HistoryConsumerPoints on a.HistoryConsumerPointID equals c.Id
					   join d in _context.PaymentMethods on a.PaymentMethodId equals d.Id
					   //join e in _context.Vouchers on a.VoucherId equals e.Id
					   select new Bill_ShowModel
					   {
						   Id = a.Id,
						   UserId = a.UserId,
						   UserName = b.Name,
						   HistoryConsumerPointID = a.HistoryConsumerPointID,
						   PaymentMethodId = a.PaymentMethodId,
						   PaymentMethodName = d.Name,
						   //VoucherId = a.VoucherId,
						   //Reduced_Value = e.Reduced_Value,
						   BillCode = a.BillCode,
						   TotalAmount = a.TotalAmount,
						   ReducedAmount = a.ReducedAmount,
						   Cash = a.Cash,
						   CustomerPayment = a.CustomerPayment,
						   FinalAmount = a.FinalAmount,
						   CreateDate = a.CreateDate,
						   ConfirmationDate = a.ConfirmationDate,
						   CompletionDate = a.CompletionDate,
						   Type = a.Type,
						   Note = a.Note,
						   Status = a.Status,
						   PhoneNumber = b.PhoneNumber,
						   Recipient = a.Recipient,
						   District = a.District,
						   Province= a.Province,
						   WardName = a.WardName,
						   ToAddress= a.ToAddress,
						   NumberPhone = a.NumberPhone,
					   }).ToList();
			return lst;
		}

        public async Task<Bill> GetBillById(Guid Id)
		{
			return await _context.Bills.FindAsync(Id);
		}

		public async Task<IEnumerable<Bill>> GetBillByUserId(Guid UsedId)
		{
			var a = await _context.Bills.Where(x => x.UserId == UsedId).ToListAsync();
			return a;
		}

		public async Task<Bill> PostBill(Bill bill)
		{
			try
			{
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();
                return bill;
            }
			catch (Exception e)
			{
				throw;
			}
		}

		public async Task<Bill> PutBill(Bill bill)
		{
			try
			{
				var a = await _context.Bills.FindAsync(bill.Id);
				if (a == null) return a;
				//a = bill;
				//addressShip.UserId = a.UserId;
				//addressShip.Recipient = a.Recipient;
				//addressShip.DistrictID = a.DistrictID;
				//addressShip.ProvinceID = a.ProvinceID;
				//addressShip.WardCode = a.WardCode;
				//addressShip.ToAddress = a.ToAddress;
				//addressShip.NumberPhone = a.NumberPhone;
				//addressShip.Status = a.Status;
				a.Note = bill.Note;
				a.Status = bill.Status;
				a.ShippingFee = bill.ShippingFee;
				_context.Bills.Update(a);
				await _context.SaveChangesAsync();
				return a;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
    }
}
