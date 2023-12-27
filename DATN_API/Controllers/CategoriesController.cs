using Microsoft.AspNetCore.Mvc;
using DATN_API.Service_IService.IServices;
using DATN_API.Models.ViewModel;
using DATN_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATN_API.Controllers
{
	[Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategory()
        {
            try
            {
                var lst = await _categoryService.GetAllCategory();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        [HttpGet("ById")]
        public async Task<ActionResult<Category>> GetByIdCategory(Guid Id)
        {
            try
            {
                var lstId = await _categoryService.GetCategoryById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }

        [HttpPost("PostCategory")]
        public async Task<ActionResult<Category>> PostCategory(Categories_VM c)
        {
            try
            {
                Category category = new Category();
                category.Id = c.Id;
                category.Name = c.Name;
                category.Status = c.Status;
                await _categoryService.PostCategory(category);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }

        // PUT api/<ColorController>/5
        [HttpPut("PutCategory")]
        public async Task<ActionResult<Category>> PutCategory(Categories_VM c)
        {
            try
            {
                Category category = await _categoryService.GetCategoryById(c.Id);
                category.Name = c.Name;
                category.Status = c.Status;
                await _categoryService.PutCategory(category);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("DeleteCategory")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid Id)
        {
            try
            {
                await _categoryService.DeleteCategory(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");
            }

        }
    }
}
