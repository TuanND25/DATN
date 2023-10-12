
using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATN_API.Controllers
{
    [Route("api/Voucher")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [HttpGet]
        public async Task<ActionResult<Voucher>> GetAllVoucher()
        {
            try
            {
                var lst = await _voucherService.GetAllVoucher();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        [HttpGet("ID")]
        public async Task<ActionResult<Voucher>> GetByIdVoucher(Guid Id)
        {
            try
            {
                var lstID = await _voucherService.GetVoucherById(Id);
                return Ok(lstID);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
            
        }

        // POST api/<VoucherController>
        [HttpPost("Post-Voucher")]
        public async Task<ActionResult<Voucher>> PostVoucher(Voucher_VM a)
        {
            try
            {
                Voucher voucher = new Voucher();
                voucher.Id = a.Id;
                voucher.Name = a.Name;
                voucher.Code = a.Code;
                voucher.Reduced_Value = a.Reduced_Value;
                voucher.Quantity = a.Quantity;
                voucher.StartDate = a.StartDate;
                voucher.EndDate = a.EndDate;
                voucher.Discount_Conditions
                    = a.Discount_Conditions;
                voucher.Status = a.Status;
                await _voucherService.PostVoucher(voucher);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }

        [HttpPut("Put-Voucher")]
        public async Task<ActionResult<Voucher>> PutVoucher(Voucher_VM a)
        {
            try
            {
                Voucher voucher = await _voucherService.GetVoucherById(a.Id);
                voucher.Name = a.Name;
                voucher.Code = a.Code;
                voucher.Reduced_Value = a.Reduced_Value;
                voucher.Quantity = a.Quantity;
                voucher.StartDate = a.StartDate;
                voucher.EndDate = a.EndDate;
                voucher.Discount_Conditions
                    = a.Discount_Conditions;
                voucher.Status = a.Status;
                await _voucherService.PutVoucher(voucher);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }

        // DELETE api/<VoucherController>/5
        [HttpDelete("Delete-Voucher")]
        public async Task<ActionResult<Voucher>> DeleteVoucher(Guid Id)
        {
            try
            {
                await _voucherService.DeleteVoucher(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Lợi để sửa 0828698564");

            }
           
        }
    }
}
