using DATN_API.Service_IService.IServices;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    public class BillItemController : Controller
    {
        private readonly IBillItemService _iBillItemService;
        public BillItemController(IBillItemService iBillItemService)
        {
            _iBillItemService = iBillItemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("get_alll_bill_item")]
        public async Task<ActionResult<BillItems>> GetAllBillItems()
        {
            try
            {
                var lst = await _iBillItemService.GetAllBillItems();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }

        }

        // GET api/<AddressShipController>/5
        [HttpGet("get_alll_bill_byId/{BillId}")]
        public async Task<ActionResult<BillItems>> GetBillItemsByBillId(Guid BillId)
        {
            try
            {
                var lstUsId = await _iBillItemService.GetBillItemsByBillId(BillId);
                return Ok(lstUsId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }
        [HttpGet("get_alll_billItem_byId")]
        public async Task<ActionResult<BillItems>> GetBillItemById(Guid Id)
        {
            try
            {
                var lstId = await _iBillItemService.GetBillItemsById(Id);
                return Ok(lstId);
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }

        }
        // POST api/<AddressShipController>
        [HttpPost("Post-BillItem")]
        public async Task<ActionResult<BillItems>> PostBillItem(BillItem_VM billitem_vm)
        {
            try
            {
                BillItems billitem = new BillItems();
                billitem.Id = Guid.NewGuid();
                billitem.BillId = billitem_vm.BillId;
                billitem.ProductItemsId = billitem_vm.ProductItemsId;
                billitem.Quantity = billitem_vm.Quantity;
                billitem.Price = billitem_vm.Price;
                billitem.Status = billitem_vm.Status;
                await _iBillItemService.PostBillItems(billitem);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }

        [HttpPut("Put-BillItems")]
        public async Task<ActionResult<BillItems>> PutBillItems(BillItem_VM billitem_vm)
        {
            try
            {
                BillItems billitem = await _iBillItemService.GetBillItemsById(billitem_vm.Id);
                billitem.Id = Guid.NewGuid();
                billitem.BillId = billitem_vm.BillId;
                billitem.ProductItemsId = billitem_vm.ProductItemsId;
                billitem.Quantity = billitem_vm.Quantity;
                billitem.Price = billitem_vm.Price;
                billitem.Status = billitem_vm.Status;
                await _iBillItemService.PutBillItems(billitem);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }

        [HttpDelete("Delete-BillItem")]
        public async Task<ActionResult<BillItems>> DeleteBillItem(Guid Id)
        {
            try
            {
                await _iBillItemService.DeleteBillItems(Id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Liên hệ Thai để sửa 0349198240");

            }
        }
       
    }
}
