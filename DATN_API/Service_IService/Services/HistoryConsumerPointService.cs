using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class HistoryConsumerPointService : IHistoryConsumerPointService
	{
		public ApplicationDbContext context;
		public HistoryConsumerPointService(ApplicationDbContext _context)
		{
			context = _context;
		}
		public async Task<HistoryConsumerPoint> AddHistoryConsumerPoint(HistoryConsumerPoint HistoryConsumerPoint)
		{
			try
			{
				var a = await context.HistoryConsumerPoints.AddAsync(HistoryConsumerPoint);
				context.SaveChanges();
				return HistoryConsumerPoint;
			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<bool> DeleteHistoryConsumerPoint(Guid Id)
		{
			try
			{
				var a = await context.HistoryConsumerPoints.FindAsync(Id);
				context.HistoryConsumerPoints.Remove(a);
				context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public async Task<List<HistoryConsumerPoint>> GetAllHistoryConsumerPoint()
		{
			var a = await context.HistoryConsumerPoints.ToListAsync();
			return a;
		}
		public async Task<HistoryConsumerPoint> GetHistoryConsumerPointById(Guid Id)
		{
			var x = await context.HistoryConsumerPoints.FirstOrDefaultAsync(c => c.Id == Id);
			return x;
		}
		public async Task<List<HistoryConsumerPoint>> GetAllHistoryConsumerPointById(Guid Id)
		{
			var x = await context.HistoryConsumerPoints.Where(c => c.Id == Id).ToListAsync();
			return x;
		}
		//public Guid Id { get; set; }
		//public Guid ConsumerPointId { get; set; }
		//public Guid FormulaId { get; set; }
		//public int Status { get; set; }
		public async Task<HistoryConsumerPoint> UpdateHistoryConsumerPoint(HistoryConsumerPoint HistoryConsumerPoint)
		{
			try
			{
				var a = await context.HistoryConsumerPoints.FindAsync(HistoryConsumerPoint.Id);
				a.ConsumerPointId = HistoryConsumerPoint.ConsumerPointId;
				a.FormulaId = HistoryConsumerPoint.FormulaId;
				a.Status = HistoryConsumerPoint.Status;
				context.HistoryConsumerPoints.Update(a);
				context.SaveChanges();
				return a;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
