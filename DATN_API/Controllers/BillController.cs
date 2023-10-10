using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }
        [HttpGet("get_alll_bill")]
        public async Task<ActionResult<Bill>> GetAllBill()
        {
            try
            {
                var lst = await _billService.GetAllBill();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }

        }

        // GET api/<AddressShipController>/5
        [HttpGet("get_bill_by_user/{UserId}")]
        public async Task<ActionResult<Bill>> GetBillByUserId(Guid UserId)
        {
            try
            {
                var lstUsId = await _billService.GetBillByUserId(UserId);
                return Ok(lstUsId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }
        [HttpGet("get_bill_by_id/{Id}")]
        public async Task<ActionResult<Bill>> GetBillById(Guid Id)
        {
            try
            {
                var lstId = await _billService.GetBillById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }

        }
        // POST api/<AddressShipController>
        [HttpPost("Post-Bill")]
        public async Task<ActionResult<Bill>> PostAddressShip(Bill_VM bill_vm)
        {
            try
            {
                Bill bill = new Bill();
                bill.Id = bill_vm.Id;
                bill.UserId = bill_vm.UserId;
                bill.TotalAmount = bill_vm.TotalAmount;
                bill.Note = bill_vm.Note;
                bill.HistoryConsumerPointID = bill_vm.HistoryConsumerPointID;
                bill.Status = bill_vm.Status;

                await _billService.PostBill(bill);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }

        [HttpPut("Put-Bill")]
        public async Task<ActionResult<Bill>> PutBill(Bill_VM bill_vm)
        {
            try
            {
                Bill bill = await _billService.GetBillById(bill_vm.Id);
                bill.Id = bill_vm.Id;
                bill.UserId = bill_vm.UserId;
                bill.TotalAmount = bill_vm.TotalAmount;
                bill.Note = bill_vm.Note;
                bill.HistoryConsumerPointID = bill_vm.HistoryConsumerPointID;
                bill.Status = bill_vm.Status;
                await _billService.PutBill(bill);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }

        [HttpDelete("Delete-Bill")]
        public async Task<ActionResult<Bill>> DeleteBill(Guid Id)
        {
            try
            {
                await _billService.DeleteBill(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }
    }
}
