
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/Size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Size>> GetAllSize()
        {
            try
            {
                var lst = await _sizeService.GetAllSize();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }
        [HttpGet("Id")]
        public async Task<ActionResult<Size>> GetByIdSize(Guid Id)
        {
            try
            {
                var lstID = await _sizeService.GetSizeById(Id);
                return Ok(lstID);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }
        [HttpPost("PostSize")]
        public async Task<ActionResult<Size>> PostSize(Size_VM sizeview)
        {
            try
            {
                Size size = new Size();
                size.Id = Guid.NewGuid();
                size.Name = sizeview.Name;
                size.Status = sizeview.Status;
                await _sizeService.PostSize(size);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }
        [HttpPut("PutSize")]
        public async Task<ActionResult<Size>> PutSize(Size_VM sizeview)
        {
            try
            {
                Size size = await _sizeService.GetSizeById(sizeview.Id);
                size.Name = sizeview.Name;
                size.Status = sizeview.Status;
                await _sizeService.PutSize(size);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult<Size>> DeleteSize(Guid Id)
        {
            try
            {
                await _sizeService.DeleteSize(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }



    }
}
