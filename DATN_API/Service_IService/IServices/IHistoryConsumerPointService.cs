using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IHistoryConsumerPointService
	{
		public Task<HistoryConsumerPoint> AddHistoryConsumerPoint(HistoryConsumerPoint HistoryConsumerPoint);
		public Task<HistoryConsumerPoint> UpdateHistoryConsumerPoint(HistoryConsumerPoint HistoryConsumerPoint);
		public Task<bool> DeleteHistoryConsumerPoint(Guid Id);
		public Task<List<HistoryConsumerPoint>> GetAllHistoryConsumerPoint();
		public Task<List<HistoryConsumerPoint>> GetAllHistoryConsumerPointById(Guid Id);
		public Task<HistoryConsumerPoint> GetHistoryConsumerPointById(Guid Id);
	}
}
