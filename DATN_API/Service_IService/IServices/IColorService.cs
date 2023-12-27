

using DATN_API.Models;

namespace DATN_API.Service_IService.IServices
{
	public interface IColorService
    {
        public Task<Color> PostColor(Color color);
        public Task<Color> PutColor(Color color);
        public Task<Color> DeleteColor(Guid Id);
        public Task<Color> GetColorById(Guid Id);
        public Task<IEnumerable<Color>> GetColorByName(string name);
        public Task<IEnumerable<Color>> GetAllColor();
    }
}
