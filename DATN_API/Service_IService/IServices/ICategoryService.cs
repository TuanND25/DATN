using DATN_Shared.Models;

namespace DATN_API.Service_IService.IServices
{
    public interface ICategoryService
    {
        public Task<Category> PostCategory (Category category);
        public Task<Category> PutCategory (Category category);
        public Task<Category> DeleteCategory (Guid Id);
        public Task<Category> GetCategoryById(Guid Id);
        public Task<IEnumerable<Category>> GetCategoryByName(string name);
        public Task<IEnumerable<Category>> GetAllCategory();
    }
}
