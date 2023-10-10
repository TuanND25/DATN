using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _iImageService;
        public ImageController(IImageService iImageService)
        {
            _iImageService = iImageService;
        }


        [HttpGet]
        public async Task<ActionResult<Image>> GetAllImage()
        {
            try
            {
                var lst = await _iImageService.GetAllImage();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }

        }

        // GET api/<AddressShipController>/5
        [HttpGet("pro")]
        public async Task<ActionResult<Image>> GetImageByProductItemId(Guid pro)
        {
            try
            {
                var lstUsId = await _iImageService.GetAddressShipByProductItemId(pro);
                return Ok(lstUsId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }
        [HttpGet("Id")]
        public async Task<ActionResult<Image>> GetImageById(Guid Id)
        {
            try
            {
                var lstId = await _iImageService.GetImageById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }

        }
        // POST api/<AddressShipController>
        [HttpPost("Post-Image")]
        public async Task<ActionResult<Image>> PostImage(Image_VM img_vm)
        {
            try
            {
                Image img = new Image();
                img.Id = Guid.NewGuid();
                img.ReviewId = img_vm.ReviewId;
                img.Name = img_vm.Name;
                img.PathImage = img_vm.PathImage;
                img.ProductItemId = img_vm.ProductItemId;
                img.Status = img_vm.Status;

                await _iImageService.PostImage(img);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }

        [HttpPut("Put-Image")]
        public async Task<ActionResult<Image>> PutAddressShip(Image_VM img_vm)
        {
            try
            {
                Image img = await _iImageService.GetImageById(img_vm.Id);

                img.ReviewId = img_vm.ReviewId;
                img.Name = img_vm.Name;
                img.PathImage = img_vm.PathImage;
                img.ProductItemId = img_vm.ProductItemId;
                img.Status = img_vm.Status;
                await _iImageService.PutImage(img);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }

        [HttpDelete("Delete-Address")]
        public async Task<ActionResult<AddressShip>> DeleteAddressShip(Guid Id)
        {
            try
            {
                await _iImageService.DeleteImage(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }
    }
}
