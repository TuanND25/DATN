using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATN_API.Controllers
{
    [Route("api/AddressShip")]
    [ApiController]
    public class AddressShipController : ControllerBase
    {
        private readonly IAddressShipService _addressShipService;
        public AddressShipController(IAddressShipService addressShipService)
        {
            _addressShipService = addressShipService;
        }
        [HttpGet]
        public async Task<ActionResult<AddressShip>> GetAllAddressShip()
        {
            try
            {
                var lst = await _addressShipService.GetAllAddressShip();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        // GET api/<AddressShipController>/5
        [HttpGet("UserId")]
        public async Task<ActionResult<AddressShip>> GetAddressShipByUserId(Guid UserId)
        {
            try
            {
                var lstUsId = await _addressShipService.GetAddressShipByUserId(UserId);
                return Ok(lstUsId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
        }
        [HttpGet("Id")]
        public async Task<ActionResult<AddressShip>> GetAddressShipById(Guid Id)
        {
            try
            {
                var lstId = await _addressShipService.GetAddressShipById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
          
        }
        // POST api/<AddressShipController>
        [HttpPost("Post-Address")]
        public async Task<ActionResult<AddressShip>> PostAddressShip(AddressShip_VM avm)
        {
            try
            {
                AddressShip addressShip = new AddressShip();
                addressShip.Id = avm.Id;
                addressShip.UserId = avm.UserId;
                addressShip.Recipient = avm.Recipient;
                addressShip.DistrictID = avm.DistrictID;
                addressShip.ProvinceID = avm.ProvinceID;
                addressShip.WardCode = avm.WardCode;
                addressShip.ToAddress = avm.ToAddress;
                addressShip.NumberPhone = avm.NumberPhone;
                addressShip.Status = avm.Status;
                await _addressShipService.PostAddressShip(addressShip);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }   
        }

        [HttpPut("Put-Address")]
        public async Task<ActionResult<AddressShip>> PutAddressShip(AddressShip_VM avm)
        {
            try
            {
                AddressShip addressShip = await _addressShipService.GetAddressShipById(avm.Id);
                addressShip.UserId = avm.UserId;
                addressShip.Recipient = avm.Recipient;
                addressShip.DistrictID = avm.DistrictID;
                addressShip.ProvinceID = avm.ProvinceID;
                addressShip.WardCode = avm.WardCode;
                addressShip.ToAddress = avm.ToAddress;
                addressShip.NumberPhone = avm.NumberPhone;
                addressShip.Status = avm.Status;
                await _addressShipService.PutAddressShip(addressShip);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
        }

        [HttpDelete("Delete-Address")]
        public async Task<ActionResult<AddressShip>> DeleteAddressShip(Guid Id)
        {
            try
            {
                await _addressShipService.DeleteAddressShip(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
        }
    }
}
