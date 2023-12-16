using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/HistoryConsumerPoint")]
	[ApiController]
	public class HistoryConsumerPointController : ControllerBase
	{
		private readonly IHistoryConsumerPointService _HistoryConsumerPoint;
		public HistoryConsumerPointController(IHistoryConsumerPointService HistoryConsumerPoint)
		{
			_HistoryConsumerPoint = HistoryConsumerPoint;
		}

		[HttpGet("get-HistoryConsumerPoint")]
		public async Task<List<HistoryConsumerPoint>> GetAllHistoryConsumerPoint()
		{
			var HistoryConsumerPoint = await _HistoryConsumerPoint.GetAllHistoryConsumerPoint();
			return HistoryConsumerPoint;
		}
		[HttpGet("{ID}")]
		public async Task<HistoryConsumerPoint> GetHistoryConsumerPointById(Guid ID)
		{
			var x = await _HistoryConsumerPoint.GetHistoryConsumerPointById(ID);
			return x;
		}
        [HttpGet("Get-HistoryConsumerPointBy-BillId/{BillID}")]
        public async Task<HistoryConsumerPoint> GetHistoryConsumerPointByBillId(Guid BillID)
        {
			try
			{
				var x = await _HistoryConsumerPoint.GetHistoryConsumerPointByBillId(BillID);
				if (x == null) return new();
				return x;
			}
			catch (Exception)
			{
				return new();
			}
        }
        //public Guid Id { get; set; }
        //public Guid ConsumerPointId { get; set; }
        //public Guid FormulaId { get; set; }
        //public int Status { get; set; }
        [HttpPost("add-HistoryConsumerPoint")]
		public async Task<ActionResult<HistoryConsumerPoint>> PostHistoryConsumerPoint(HistoryConsumerPoint_VM rvm)
		{
			HistoryConsumerPoint HistoryConsumerPoint = new HistoryConsumerPoint();
			HistoryConsumerPoint.Id = rvm.Id;
			HistoryConsumerPoint.ConsumerPointId = rvm.ConsumerPointId;
			HistoryConsumerPoint.FormulaId = rvm.FormulaId;
			HistoryConsumerPoint.BillId = rvm.BillId;
			HistoryConsumerPoint.Point = rvm.Point;
			HistoryConsumerPoint.Status = rvm.Status;
			await _HistoryConsumerPoint.AddHistoryConsumerPoint(HistoryConsumerPoint);
			return Ok();
		}
		[HttpPut("update-HistoryConsumerPoint")]
		public async Task<ActionResult<HistoryConsumerPoint>> PutHistoryConsumerPoint(HistoryConsumerPoint_VM rvm)
		{
			HistoryConsumerPoint HistoryConsumerPoint = await _HistoryConsumerPoint.GetHistoryConsumerPointById(rvm.Id);
			HistoryConsumerPoint.ConsumerPointId = rvm.ConsumerPointId;
			HistoryConsumerPoint.FormulaId = rvm.FormulaId;
            HistoryConsumerPoint.BillId = rvm.BillId;
            HistoryConsumerPoint.Point = rvm.Point;
            HistoryConsumerPoint.Status = rvm.Status;
			await _HistoryConsumerPoint.UpdateHistoryConsumerPoint(HistoryConsumerPoint);
			return Ok();
		}
		[HttpDelete("delete-HistoryConsumerPoint")]
		public async Task<ActionResult<HistoryConsumerPoint>> Delete(Guid id)
		{
			HistoryConsumerPoint HistoryConsumerPoint = await _HistoryConsumerPoint.GetHistoryConsumerPointById(id);
			HistoryConsumerPoint.Status = 0;
			await _HistoryConsumerPoint.UpdateHistoryConsumerPoint(HistoryConsumerPoint);
			return Ok();
		}
	}
}
