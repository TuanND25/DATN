using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Api.V2010.Account;

namespace DATN_API.Service_IService.Services
{
	public class VoucherUserService : IVoucherUserService
	{
		private readonly ApplicationDbContext _context;
		public VoucherUserService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<VoucherUser> DeleteVoucherUser(Guid Id)
		{
			try
			{
				var vu = await _context.VoucherUsers.FindAsync(Id);
				_context.VoucherUsers.Remove(vu);
				_context.SaveChanges();
				return vu;
			}
			catch (Exception)
			{

				return new VoucherUser();
			}
		}

		public async Task<IEnumerable<VoucherUser>> GetAllVoucherUser()
		{
			try
			{
				var lst = await _context.VoucherUsers.ToListAsync();
				return lst;
			}
			catch (Exception)
			{
				return new List<VoucherUser>();
			}
		}

		public async Task<VoucherUser> GetVoucherUserById(Guid Id)
		{
			try
			{
				var vu = await _context.VoucherUsers.FindAsync(Id);
				return vu;
			}
			catch (Exception)
			{
				return new VoucherUser();
			}
		}

		public async Task<IEnumerable<VoucherUser>> GetVoucherUserByUserId(Guid UserId)
		{
			try
			{
				var vu = await _context.VoucherUsers.Where(c => c.UserId == UserId).ToListAsync();
				return vu;
			}
			catch (Exception)
			{
				return new List<VoucherUser>();
			}
		}

		public async Task<VoucherUser> PostVoucherUser(VoucherUser voucher)
		{
			try
			{
				_context.VoucherUsers.Add(voucher);
				_context.SaveChanges();
				return voucher;
			}
			catch (Exception)
			{

				throw;
			}
		}

		public async Task<VoucherUser> PutVoucherUser(VoucherUser voucher)
		{
			try
			{
				var vu = await _context.VoucherUsers.FindAsync(voucher.Id);
				vu.Status = voucher.Status;
				_context.VoucherUsers.Update(voucher);
				_context.SaveChanges();
				return voucher;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
