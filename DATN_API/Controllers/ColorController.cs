
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATN_API.Controllers
{
    [Route("api/Color")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorS;
        public ColorController(IColorService colorS)
        {
            _colorS = colorS;
        }
        [HttpGet]
        public async Task<ActionResult<Color>> GetAllColor()
        {
            try
            {
                var lst = await _colorS.GetAllColor();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }

        [HttpGet("ById")]
        public async Task<ActionResult<Color>> GetByIdColor(Guid Id)
        {
            try
            {
                var lstId = await _colorS.GetColorById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
          
        }

        [HttpPost("PostColor")]
        public async Task<ActionResult<Color>> PostColor(Color_VM colorView)
        {
            try
            {
                Color color = new Color();
                color.Id = Guid.NewGuid();
                color.Name = colorView.Name;
                color.Status = colorView.Status;
                await _colorS.PostColor(color);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
          
        }

        // PUT api/<ColorController>/5
        [HttpPut("PutColoe")]
        public async Task<ActionResult<Color>> PutColor(Color_VM colorView)
        {
            try
            {
                Color color = await _colorS.GetColorById(colorView.Id);
                color.Name = colorView.Name;
                color.Status = colorView.Status;
                await _colorS.PutColor(color);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
          
        }

        // DELETE api/<ColorController>/5
        [HttpDelete("DeleteColor")]
        public async Task<ActionResult<Color>> DeleteColor(Guid Id)
        {
            try
            {
                await _colorS.DeleteColor(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return Ok("Đã Xóa");
            }
           
        }
    }
}
