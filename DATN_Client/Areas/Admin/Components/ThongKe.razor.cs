using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class ThongKe
    {
        List<Bill_ShowModel> _lstBill = new List<Bill_ShowModel>();
        Bill_ShowModel _model = new Bill_ShowModel();
        HttpClient _httpClient = new HttpClient();
        public DateTime _optioSale = DateTime.Now;
        Count count = new Count();
        Count count1 = new Count();
        Count count2 = new Count();
        Count count3 = new Count();
        List<BillDetailShow> _lstBillDeails = new List<BillDetailShow>();
        List<BillDetailShow> _lstThongKeProductItem = new List<BillDetailShow>();
        [Inject] NavigationManager _navigationManager { get; set; }
        //var _lstThongKeProductItem;
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind

        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        User_VM _user_VM = new User_VM();
        protected override async Task OnInitializedAsync()
        {
            _lstBill = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            await Sale(0);
            await Revenue(1);
            await Products(1);
            await TopSale(1);
        }
        public async Task Sale(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");

            if (option == 0)
            {
                _lstBill = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count.Dem = _lstBill.Count();
                count.Tittle = "Today";
            }
            else if (option == 1)
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count.Dem = _lstBill.Count();
                count.Tittle = "This Month";
            }
            else
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count.Dem = _lstBill.Count();
                count.Tittle = "This Year";
            }
        }

        public async Task Revenue(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");

            if (option == 0)
            {
                _lstBill = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBill)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "Today";
            }
            else if (option == 1)
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBill)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "This Month";
            }
            else
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBill)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "This Year";
            }
        }
        public async Task Products(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            if (option == 0)
            {
                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count2.Dem = 0;
                foreach (var b in _lstBill)
                {
                    var _lstBillitem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstBillitem);
                }
                foreach (var c in _lstBillDeails)
                {
                    var _lstBi = await _httpClient.GetFromJsonAsync<List<BillItems>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
                    var pro = _lstBi.FirstOrDefault(x => x.Id == c.Id);
                    count2.Dem += pro.Quantity;
                }
                count2.Tittle = "Today";
            }
            else if (option == 1)
            {
                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count2.Dem = 0;
                foreach (var b in _lstBill)
                {
                    var _lstPro = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstPro);
                }
                foreach (var c in _lstBillDeails)
                {
                    var _lstBi = await _httpClient.GetFromJsonAsync<List<BillItems>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
                    var pro = _lstBi.FirstOrDefault(x => x.Id == c.Id);
                    count2.Dem += pro.Quantity;
                }
                count2.Tittle = "This Month";
            }
            else
            {
                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count2.Dem = 0;
                foreach (var b in _lstBill)
                {
                    var _lstPro = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstPro);
                }
                foreach (var c in _lstBillDeails)
                {
                    var _lstBi = await _httpClient.GetFromJsonAsync<List<BillItems>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
                    var pro = _lstBi.FirstOrDefault(x => x.Id == c.Id);
                    count2.Dem += pro.Quantity;
                }
                count2.Tittle = "This Year";
            }
        }
        public async Task TopSale(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            if (option == 0)
            {
                count3.Tittle = "Today";
                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                foreach (var b in _lstBill)
                {
                    var _lstBillitem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstBillitem);
                }
                _lstThongKeProductItem = _lstBillDeails
                                        .GroupBy(x => x.ProductItemId)
                                        .Select(group => new BillDetailShow
                                        {
                                            Quantity = group.Sum(item => item.Quantity),
                                            Name = group.FirstOrDefault()?.Name,
                                            ColorName = group.FirstOrDefault()?.ColorName,
                                            SizeName = group.FirstOrDefault()?.SizeName,
                                            CostPrice = group.FirstOrDefault()?.CostPrice
                                        })
                                        .OrderByDescending(group => group.Quantity)
                                        .ToList();

            }
            else if (option == 1)
            {
                count3.Tittle = "This Month";
                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                foreach (var b in _lstBill)
                {
                    var _lstBillitem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstBillitem);
                }
                _lstThongKeProductItem = _lstBillDeails
                                        .GroupBy(x => x.ProductItemId)
                                        .Select(group => new BillDetailShow
                                        {
                                            Quantity = group.Sum(item => item.Quantity),
                                            Name = group.FirstOrDefault()?.Name,
                                            ColorName = group.FirstOrDefault()?.ColorName,
                                            SizeName = group.FirstOrDefault()?.SizeName,
                                            CostPrice = group.FirstOrDefault()?.CostPrice
                                        })
                                        .OrderByDescending(group => group.Quantity).Take(5)
                                        .ToList();
            }
            else
            {
                count3.Tittle = "This Year";
                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                foreach (var b in _lstBill)
                {
                    var _lstBillitem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstBillitem);
                }
                _lstThongKeProductItem = _lstBillDeails
                                        .GroupBy(x => x.ProductItemId)
                                        .Select(group => new BillDetailShow
                                        {
                                            Quantity = group.Sum(item => item.Quantity),
                                            Name = group.FirstOrDefault()?.Name,
                                            ColorName = group.FirstOrDefault()?.ColorName,
                                            SizeName = group.FirstOrDefault()?.SizeName,
                                            CostPrice = group.FirstOrDefault()?.CostPrice
                                        })
                                        .OrderByDescending(group => group.Quantity)
                                        .ToList();
            }
        }

    }
    public class Count
    {
        public int? Dem { get; set; }
        public string Tittle { get; set; }
        public int Persen { get; set; }
        public bool Status { get; set; }
        public Count()
        {

        }
    }
}
