using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class CustomerPointService : ICustomerPointService
	{
		private readonly ApplicationDbContext _context;
		public CustomerPointService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<List<CustomerPoint_VM>> GetAllCustomerPoint()
		{
			try
			{
				return await _context.ConsumerPoints.Select(x => new CustomerPoint_VM
				{
					UserID = x.UserID,
					Point = x.Point,
					Status = x.Status,
				}).ToListAsync();

			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<CustomerPoint_VM> GetCustomerPoint_byUserID(Guid UserID)
		{
			try
			{
				return await _context.ConsumerPoints.Select(x => new CustomerPoint_VM
				{
					UserID = x.UserID,
					Point = x.Point,
					Status = x.Status,
				}).FirstOrDefaultAsync(c => c.UserID == UserID) ?? new();
			}
			catch (Exception)
			{
				return new CustomerPoint_VM();
			}
		}

		public async Task<string> PutCustomerPoint(CustomerPoint_VM customerPoint_VM)
		{
			try
			{
				var a = await _context.ConsumerPoints.FindAsync(customerPoint_VM.UserID);
				if (a == null)
				{
					return "Ví điểm chưa tồn tại";
				}
				a.Point = customerPoint_VM.Point;
				a.Status = customerPoint_VM.Status;
				_context.ConsumerPoints.Update(a);
				await _context.SaveChangesAsync();
				return "Success";
			}
			catch (Exception)
			{

				return "Đã có lỗi xảy ra";
			}
		}
	}
}
