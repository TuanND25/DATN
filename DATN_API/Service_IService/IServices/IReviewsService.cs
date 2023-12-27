using DATN_API.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IReviewsService
	{
		public Task<Reviews> AddReviews(Reviews Reviews);
		public Task<Reviews> UpdateReviews(Reviews Reviews);
		public Task<bool> DeleteReviews(Guid Id);
		public Task<List<Reviews>> GetAllReviews();
		public Task<List<Reviews>> GetAllReviewsById(Guid Id);
		public Task<Reviews> GetReviewsById(Guid Id);
	}
}
