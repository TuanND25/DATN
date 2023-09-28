using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class ReviewsService : IReviewsService
	{
		public ApplicationDbContext context;
		public ReviewsService(ApplicationDbContext _context)
		{
			context = _context;
		}
		public async Task<Reviews> AddReviews(Reviews Reviews)
		{
			try
			{
				var a = await context.Reviews.AddAsync(Reviews);
				context.SaveChanges();
				return Reviews;
			}
			catch (Exception e)
			{

				return null;
			}

		}

		public async Task<bool> DeleteReviews(Guid Id)
		{
			try
			{
				var a = await context.Reviews.FindAsync(Id);
				context.Reviews.Remove(a);
				context.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}

		}

		public async Task<List<Reviews>> GetAllReviews()
		{
			var a = await context.Reviews.ToListAsync();
			return a;
		}
		public async Task<Reviews> GetReviewsById(Guid Id)
		{
			var x = await context.Reviews.FirstOrDefaultAsync(c => c.Id == Id);
			return x;
		}
		public async Task<List<Reviews>> GetAllReviewsById(Guid Id)
		{
			var x = await context.Reviews.Where(c => c.Id == Id).ToListAsync();
			return x;
		}
		//public Guid Id { get; set; }
		//public Guid UserId { get; set; }
		//public Guid ProductId { get; set; }
		//public int Rating { get; set; }
		//public string Comment { get; set; }
		//public DateTime CreateAt { get; set; }
		//public DateTime UpdateAt { get; set; }
		//public int Status { get; set; }
		public async Task<Reviews> UpdateReviews(Reviews Reviews)
		{
			try
			{
				var a = await context.Reviews.FindAsync(Reviews.Id);
				a.UserId = Reviews.UserId;
				a.ProductId = Reviews.ProductId;
				a.Rating = Reviews.Rating;
				a.Comment = Reviews.Comment;
				a.CreateAt = Reviews.CreateAt;
				a.UpdateAt = Reviews.UpdateAt;
				a.Status = Reviews.Status;
				context.Reviews.Update(a);
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
