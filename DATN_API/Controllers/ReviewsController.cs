using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace DATN_API.Controllers
{
	[Route("api/Reviews")]
	[ApiController]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewsService _Reviews;
		public ReviewsController(IReviewsService Reviews)
		{
			_Reviews = Reviews;
		}

		[HttpGet("get-Reviews")]
		public async Task<List<Reviews>> GetAllReviews()
		{
			var Reviews = await _Reviews.GetAllReviews();
			return Reviews;
		}
		[HttpGet("{ID}")]
		public async Task<Reviews> GetReviewsById(Guid ID)
		{
			var x = await _Reviews.GetReviewsById(ID);
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
		[HttpPost("add-Reviews")]
		public async Task<ActionResult<Reviews>> PostReviews(Reviews_VM rvm)
		{
			Reviews Reviews = new Reviews();
			Reviews.Id = Guid.NewGuid();
			Reviews.UserId = rvm.UserId;
			Reviews.ProductId = rvm.ProductId;
			Reviews.Rating = rvm.Rating;
			Reviews.Comment = rvm.Comment;
			Reviews.CreateAt = rvm.CreateAt;
			Reviews.UpdateAt = rvm.UpdateAt;
			Reviews.Status = rvm.Status;
			await _Reviews.AddReviews(Reviews);
			return Ok();
		}
		[HttpPut("update-Reviews")]
		public async Task<ActionResult<Reviews>> PutReviews(Reviews_VM rvm)
		{
			Reviews Reviews = await _Reviews.GetReviewsById(rvm.Id);
			Reviews.UserId = rvm.UserId;
			Reviews.ProductId = rvm.ProductId;
			Reviews.Rating = rvm.Rating;
			Reviews.Comment = rvm.Comment;
			Reviews.CreateAt = rvm.CreateAt;
			Reviews.UpdateAt = rvm.UpdateAt;
			Reviews.Status = rvm.Status;
			await _Reviews.UpdateReviews(Reviews);
			return Ok();
		}
		[HttpDelete("delete-Reviews")]
		public async Task<ActionResult<Reviews>> Delete(Guid id)
		{
			Reviews Reviews = await _Reviews.GetReviewsById(id);
			Reviews.Status = 0;
			await _Reviews.UpdateReviews(Reviews);
			return Ok();
		}
	}
}
