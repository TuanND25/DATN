using DATN_API.Service_IService.IServices;
using DATN_API.Service_IService.Services;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/paymentMethod")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodServices _PaymentMethodService;
        public PaymentMethodController(IPaymentMethodServices PaymentMethodService)
        {
            _PaymentMethodService = PaymentMethodService;
        }
        [HttpGet("get_all_paymentMethod")]
        public async Task<ActionResult<PaymentMethod>> GetAllPaymentMethod()
        {
            try
            {
                var lst = await _PaymentMethodService.GetAllPaymentMethod();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest();

            }

        }

        [HttpGet("get_all_paymentMethod_ById/{Id}")]
        public async Task<ActionResult<PaymentMethod>> GetByIdPaymentMethod(Guid Id)
        {
            try
            {
                var lstId = await _PaymentMethodService.GetPaymentMethodById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest();

            }

        }

        [HttpPost("add_paymentMethod")]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod_VM c)
        {
            try
            {
                PaymentMethod PaymentMethod = new PaymentMethod();
                PaymentMethod.Id = c.Id;
                PaymentMethod.Name = c.Name;
                PaymentMethod.Status = c.Status;
                await _PaymentMethodService.PostPaymentMethod(PaymentMethod);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest();

            }

        }

        // PUT api/<ColorController>/5
        [HttpPut("update_paymentMethod")]
        public async Task<ActionResult<PaymentMethod>> PutPaymentMethod(Categories_VM c)
        {
            try
            {
                PaymentMethod PaymentMethod = await _PaymentMethodService.GetPaymentMethodById(c.Id);
                PaymentMethod.Name = c.Name;
                PaymentMethod.Status = c.Status;
                await _PaymentMethodService.PutPaymentMethod(PaymentMethod);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest();

            }

        }

        // DELETE api/<ColorController>/5
        [HttpDelete("delete_paymentMethod")]
        public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod(Guid Id)
        {
            try
            {
                await _PaymentMethodService.DeletePaymentMethod(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
