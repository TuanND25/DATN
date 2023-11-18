using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class SellStalls
    {
        HttpClient _client = new HttpClient();
        [Inject] NavigationManager _navigation { get; set; }
        List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
        List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
        List<Products_VM> _lstP = new List<Products_VM>();
        List<Bill_VM> _lstBill = new List<Bill_VM>();
        Bill_VM bill = new Bill_VM();
        List<Bill_VM> _lstBill_Vm = new List<Bill_VM>();
        List<Bill_VM> _lstBill_Vm_show = new List<Bill_VM>();
        public int paymentmethodid { get; set; } = 2;
        public Guid BillId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            _lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
            _lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
        }
        public async Task  addBill()
        {
            var codeToday = "B" + DateTime.Now.ToString().Substring(0, 10).Replace("/", "") + ".";
            var id = Guid.NewGuid();
            bill = new Bill_VM();
            bill.Id = id;
            bill.Status = 5;
            bill.Type = 1;//offline
            bill.UserId = Guid.Parse("6328a6c2-1e4b-40ea-8337-3a08d362eddd");
            _lstBill = (await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
            if (_lstBill.Count == 0) bill.BillCode = codeToday + "1";
            else bill.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(10)) + 1).ToString();

           
            try
            {
                var a = await _client.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", bill);
            }
            catch (Exception)
            {

                throw;
            }
            _lstBill_Vm_show.Add(bill);
            BillId = id;
        }
        public void getBillId(Guid id )
        {
            BillId = id;
        }  
        public void getPaymetMethod(int id )
        {
            paymentmethodid = id;
        }
        public void closeBill(Guid id)
        {
            var x = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id);
            _lstBill_Vm_show.Remove(x);
            if (_lstBill_Vm_show.Count>0)
            {
                BillId = _lstBill_Vm_show[0].Id;
            }
           
        }
    }

}
