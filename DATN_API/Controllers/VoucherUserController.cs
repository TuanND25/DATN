using DATN_API.Service_IService.IServices;
using DATN_API.Service_IService.Services;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/voucher-user")]
	[ApiController]
	public class VoucherUserController : ControllerBase
	{
		private readonly IVoucherUserService _voucherUserService;
		public VoucherUserController(IVoucherUserService voucherUserService)
		{
			_voucherUserService = voucherUserService;
		}

		[HttpGet("get-all")]
		public async Task<ActionResult<VoucherUser>> GetAllVoucherUser()
		{
			try
			{
				var lst = await _voucherUserService.GetAllVoucherUser();
				return Ok(lst);
			}
			catch (Exception ex)
			{
				return BadRequest(null);

			}

		}

		[HttpGet("get-voucherUser-by-id/{Id}")]
		public async Task<ActionResult<VoucherUser>> GetByIdVoucherUser(Guid Id)
		{
			try
			{
				var lstID = await _voucherUserService.GetVoucherUserById(Id);
				return Ok(lstID);
			}
			catch (Exception)
			{
				return new VoucherUser();

			}

		}

		[HttpGet("get-voucherUser-by-userid/{UserId}")]
		public async Task<ActionResult<List<VoucherUser>>> GetVoucherUserByCode(Guid UserId)
		{
			try
			{
				var lstID = await _voucherUserService.GetVoucherUserByUserId(UserId);
				if (lstID == null)
				{
					return Ok(new VoucherUser());
				}
				return Ok(lstID);
			}
			catch (Exception)
			{
				return BadRequest(null);
			}

		}

		// POST api/<VoucherController>
		[HttpPost("post-voucherUser")]
		public async Task<ActionResult<VoucherUser>> PostVoucherUser(VoucherUser_VM a)
		{
			try
			{
				VoucherUser voucher = new VoucherUser();
				voucher.Id = a.Id;
				voucher.UserId = a.UserId;
				voucher.VoucherId = a.VoucherId;
				voucher.Status = a.Status;
				await _voucherUserService.PostVoucherUser(voucher);
				return Ok(a);
			}
			catch (Exception ex)
			{
				return null;

			}

		}

		[HttpPut("put-voucherUser")]
		public async Task<ActionResult<VoucherUser>> PutVoucherUser(VoucherUser_VM a)
		{
			try
			{
				VoucherUser voucherUser = await _voucherUserService.GetVoucherUserById(a.Id);
				voucherUser.UserId = a.UserId;
				voucherUser.VoucherId = a.VoucherId;
				voucherUser.Status = a.Status;
				await _voucherUserService.PutVoucherUser(voucherUser);
				return Ok("Success");
			}
			catch (Exception ex)
			{
				return null;

			}

		}

		// DELETE api/<VoucherController>/5
		[HttpDelete("delete-voucherUser")]
		public async Task<ActionResult<VoucherUser>> DeleteVoucherUser(Guid Id)
		{
			try
			{
				await _voucherUserService.DeleteVoucherUser(Id);
				return Ok("Success");
			}
			catch (Exception ex)
			{
				return BadRequest(null);

			}

		}
	}
}
