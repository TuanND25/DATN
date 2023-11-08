using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/Bill")]
	[ApiController]
	public class BillController : ControllerBase
	{
		private readonly IBillService _billService;

		public BillController(IBillService billService)
		{
			_billService = billService;
		}

		[HttpGet("get_alll_bill")]
		public async Task<ActionResult<List<Bill_ShowModel>>> GetAllBill1()
		{
			var lst = await _billService.GetAllBill();
			return lst;
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
			//public Guid Id { get; set; }
			//public Guid UserId { get; set; }
			//public Guid? HistoryConsumerPointID { get; set; }
			//public Guid PaymentMethodId { get; set; }
			//public Guid? VoucherId { get; set; }
			//public string BillCode { get; set; }
			//public int? TotalAmount { get; set; }
			//public int? ReducedAmount { get; set; }
			//public int? Cash { get; set; }  // tiền mặt
			//public int? CustomerPayment { get; set; } // tiền khách đưa
			//public int? FinalAmount { get; set; } // tiền khách đưa
			//public DateTime? CreateDate { get; set; }
			//public DateTime? ConfirmationDate { get; set; }
			//public DateTime? CompletionDate { get; set; }
			//public int Type { get; set; }
			//public string? Note { get; set; }
			//public string Recipient { get; set; } // Người nhận
			//public string District { get; set; } // Quận/Huyện
			//public string Province { get; set; } // Tỉnh/ TP
			//public string WardName { get; set; } // Phường/ Xã
			//public string ToAddress { get; set; } // Địa chỉ chi tiết
			//public string NumberPhone { get; set; } // SDT
			//public int Status { get; set; }
			//public int? ShippingFee { get; set; }
			try
			{
				Bill bill = new Bill();
				bill.Id = bill_vm.Id;
				bill.UserId = bill_vm.UserId;
				bill.HistoryConsumerPointID = bill_vm.HistoryConsumerPointID;
				bill.PaymentMethodId = bill_vm.PaymentMethodId;
				bill.VoucherId = bill_vm.VoucherId;
				bill.BillCode = bill_vm.BillCode;
				bill.TotalAmount = bill_vm.TotalAmount;
				bill.ReducedAmount = bill_vm.ReducedAmount;
				bill.Cash = bill_vm.Cash;
				bill.CustomerPayment = bill_vm.CustomerPayment;
				bill.FinalAmount = bill_vm.FinalAmount;
				bill.CreateDate = bill_vm.CreateDate;
				bill.ConfirmationDate = bill_vm.ConfirmationDate;
				bill.CompletionDate = bill_vm.CompletionDate;
				bill.Note = bill_vm.Note;
				bill.Recipient = bill_vm.Recipient;
				bill.District = bill_vm.District;
				bill.Province = bill_vm.Province;
				bill.WardName = bill_vm.WardName;
				bill.ToAddress = bill_vm.ToAddress;
				bill.NumberPhone = bill_vm.NumberPhone;
				bill.Status = bill_vm.Status;
				bill.ShippingFee = bill_vm.ShippingFee;

				var x = await _billService.PostBill(bill);
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
				bill.HistoryConsumerPointID = bill_vm.HistoryConsumerPointID;
				bill.PaymentMethodId = bill_vm.PaymentMethodId;
				bill.VoucherId = bill_vm.VoucherId;
				bill.BillCode = bill_vm.BillCode;
				bill.TotalAmount = bill_vm.TotalAmount;
				bill.ReducedAmount = bill_vm.ReducedAmount;
				bill.Cash = bill_vm.Cash;
				bill.CustomerPayment = bill_vm.CustomerPayment;
				bill.FinalAmount = bill_vm.FinalAmount;
				bill.Note = bill_vm.Note;
				bill.Type = bill_vm.Type;
				bill.Status = bill_vm.Status;
				bill.ShippingFee = bill_vm.ShippingFee;
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