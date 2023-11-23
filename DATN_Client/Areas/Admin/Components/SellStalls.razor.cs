using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using DATN_Shared.ViewModel.DiaChi;
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
        List<Products_VM> _lstP_1 = new List<Products_VM>();
        List<Bill_VM> _lstBill = new List<Bill_VM>();
        Bill_VM bill = new Bill_VM();
        List<Bill_VM> _lstBill_Vm = new List<Bill_VM>();
        List<Bill_VM> _lstBill_Vm_show = new List<Bill_VM>();
        List<User_VM> _lstUser = new List<User_VM>();
        List<User_VM> _lstUser_1 = new List<User_VM>();
        User_VM getuser = new User_VM();
        public int ActiveTabSearchUser { get; set; } = 1;
        public int paymentmethodid { get; set; } = 2;
        public Guid BillId { get; set; }
        private List<Province_VM> _lstTinhTp = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong = new List<Ward_VM>();
        private List<Province_VM> _lstTinhTp_Data = new List<Province_VM>();
        private List<District_VM> _lstQuanHuyen_Data = new List<District_VM>();
        private List<Ward_VM> _lstXaPhuong_Data = new List<Ward_VM>();

        public string _TinhTp { get; set; }
        public string _QuanHuyen { get; set; }
        public string _PhuongXa { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
            _lstImg_PI = await _client.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem");
            _lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
            _lstP_1 = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");



            //try
            //{
            //    _lstUser = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");
            //}
            //catch (Exception e)
            //{

            //    throw;
            //}

            //_lstUser_1 = await _client.GetFromJsonAsync<List<User_VM>>("https://localhost:7141/api/user/get-user");




            _lstTinhTp_Data = await _client.GetFromJsonAsync<List<Province_VM>>("https://api.npoint.io/ac646cb54b295b9555be"); // api tỉnh tp
            _lstTinhTp = _lstTinhTp_Data;
            _lstQuanHuyen_Data = await _client.GetFromJsonAsync<List<District_VM>>("https://api.npoint.io/34608ea16bebc5cffd42");
            _lstXaPhuong_Data = await _client.GetFromJsonAsync<List<Ward_VM>>("https://api.npoint.io/dd278dc276e65c68cdf5");





        }
        public async Task addBill()
        {
            var codeToday = "B" + DateTime.Now.ToString().Substring(0, 10).Replace("/", "") + ".";
            var id = Guid.NewGuid();
            bill = new Bill_VM();
            bill.Id = id;
            bill.Status = 5;
            bill.Type = 1;//offline
            if (getuser.Id == default)
            {
                bill.UserId = Guid.Parse("cb01c19a-5a10-4e8a-aea5-34221bd6ea20");
            }
            else
            {
                bill.UserId = getuser.Id;
            }

            _lstBill = (await _client.GetFromJsonAsync<List<Bill_VM>>("https://localhost:7141/api/Bill/get_alll_bill")).Where(c => c.BillCode.StartsWith(codeToday)).ToList();
            if (_lstBill.Count == 0) bill.BillCode = codeToday + "1";
            else bill.BillCode = codeToday + _lstBill.Max(c => int.Parse(c.BillCode.Substring(10)) + 1).ToString();

            var a = await _client.PostAsJsonAsync("https://localhost:7141/api/Bill/Post-Bill", bill);
            if (a.StatusCode.ToString() == "OK")
            {
                _lstBill_Vm_show.Add(bill);
                BillId = id;
            }
        }
        private void getBillId(Guid id123)
        {
            if (_lstBill_Vm_show.Count > 0)
            {
                var x = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id123);
                BillId = x.Id;
            }
        }
        public void getPaymetMethod(int id)
        {
            paymentmethodid = id;
        }
        public void closeBill(Guid id)
        {
            var z = _lstBill_Vm_show.FirstOrDefault(x => x.Id == id);
            _lstBill_Vm_show.Remove(z);
            if (_lstBill_Vm_show.Count > 0)
            {
                BillId = _lstBill_Vm_show[0].Id;
            }
        }
        public void SearchUser(ChangeEventArgs e)
        {
            //string a = e.Value.ToString().ToLower();
            //if (e.Value == null || e.Value.ToString() == "" || e.Value.ToString() == string.Empty)
            //{
            //    _lstUser = _lstUser_1;
            //    return;
            //}
            //if (_lstUser.Count == 0)
            //{
            //    _lstUser = _lstUser_1;
            //}
            //_lstUser = _lstUser.Where(c => c.Name.Contains(e.Value.ToString()) || c.PhoneNumber.Contains(e.Value.ToString())).ToList();
            //ActiveTabSearchUser = 2;
        }

        public void SearchProduct(ChangeEventArgs e)
        {
            if (e.Value == null || e.Value.ToString() == "" || e.Value.ToString() == string.Empty)
            {
                _lstP = _lstP_1;
                return;
            }
            if (_lstUser.Count == 0)
            {
                _lstP = _lstP_1;

            }
            _lstP = _lstP.Where(c => c.Name.Contains(e.Value.ToString())).ToList();
            //Chưa xong còn search theo mã nữa
        }

        public void offTabSearchUser()
        {
            ActiveTabSearchUser = 1;
        }
        public void GetUserFormSearchUser(Guid id)
        {
            getuser = _lstUser_1.FirstOrDefault(x => x.Id == id);
            ActiveTabSearchUser = 1;
        }
        public async Task ChonTinhTP(ChangeEventArgs e)
        {
            _TinhTp = e.Value.ToString();
            _lstQuanHuyen.Clear();
            _lstXaPhuong.Clear();            
            var chon = _lstTinhTp_Data.FirstOrDefault(x => x.Name == _TinhTp);
            _lstQuanHuyen = _lstQuanHuyen_Data.Where(x => x.ProvinceId == chon.Id).ToList();
        }
        public async Task ChonQuanHuyen(ChangeEventArgs e)
        {
            _PhuongXa = e.Value.ToString();   
            _lstXaPhuong.Clear();   
         
            var chon = _lstQuanHuyen.FirstOrDefault(x => x.Name == _PhuongXa);
            _lstXaPhuong = _lstXaPhuong_Data.Where(x => x.DistrictId == chon.Id).ToList();

        }

    }

}
