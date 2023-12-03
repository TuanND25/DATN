using BlazorBootstrap;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class ThongKe
    {
        List<Bill_ShowModel> _lstBill = new List<Bill_ShowModel>();
        List<BillDetailShow> _lstBillDetai = new List<BillDetailShow>();
        List<ProductItems> _lstProductItem = new List<ProductItems>();
        List<Promotions_VM> _lstPromotion = new List<Promotions_VM>();
        Bill_ShowModel _model = new Bill_ShowModel();
        HttpClient _httpClient = new HttpClient();
        public DateTime _optioSale = DateTime.Now;
        Count count = new Count();
        Count count1 = new Count();
        Count count2 = new Count();
        Count count3 = new Count();
        List<Count> productCounts = new List<Count>();
        List<Count> _billCounts = new List<Count>();
        List<Count> _RevenueCounts = new List<Count>();
        List<Count> _RevenueCounts1 = new List<Count>();
        List<Count> _RevenueCounts2 = new List<Count>();
        List<BillDetailShow> _lstBillDeails = new List<BillDetailShow>();
        List<BillDetailShow> _lstThongKeProductItem = new List<BillDetailShow>();
        [Inject] NavigationManager _navigationManager { get; set; }
        //var _lstThongKeProductItem;
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind

        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        User_VM _user_VM = new User_VM();
        bool isLoader = false;


        private BarChart barChart=new BarChart();
        private LineChart lineChart =new LineChart();
        protected override async Task OnInitializedAsync()
        {
            //isLoader = true;
            _lstBill = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItems>>("https://localhost:7141/api/productitem/get_all_productitem");
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");
            _lstBillDetai = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/get_alll_bill_item_show");
            await Sale(0);
            await Revenue(1);
            await Products(1);
            await TopSale(1);
            //isLoader = false;
            //StateHasChanged();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RenderManhattanAsync();
                await RenderWormAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        public async Task Sale(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");

            if (option == 0)
            {
                _lstBill = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count.Dem = _lstBill.Count();
                count.Tittle = "Hôm nay";
            }
            else if (option == 1)
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count.Dem = _lstBill.Count();
                count.Tittle = "Tháng này";
            }
            else
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count.Dem = _lstBill.Count();
                count.Tittle = "Năm nay";
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
                count1.Tittle = "Hôm nay";
            }
            else if (option == 1)
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBill)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "Tháng này";
                int? countTemp = 0;
                var _lstBillBefore = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month - 1).ToList();
                foreach (var b in _lstBillBefore)
                {
                    countTemp += b.TotalAmount;
                }
                if (_lstBillBefore.Count > 0)
                {
                    count1.Persen = (count1.Dem - countTemp) / countTemp * 100;
                }
                else
                {
                    count1.Persen = 0;
                }

            }
            else
            {
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBill)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "Năm nay";
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
                count2.Tittle = "Hôm nay";
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
                count2.Tittle = "Tháng này";
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
                count2.Tittle = "Năm nay";
            }
        }
        public async Task TopSale(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            if (option == 0)
            {
                count3.Tittle = "Hôm nay";
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
                                            ProductCode=group.FirstOrDefault()?.ProductCode,
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
                count3.Tittle = "Tháng này";
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
                                            ProductCode = group.FirstOrDefault()?.ProductCode,
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
                count3.Tittle = "Năm nay";
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
                                            ProductCode = group.FirstOrDefault()?.ProductCode,
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


        private async Task RenderManhattanAsync()
        {
            var b = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/get_alll_bill_item_show");
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            // Duyệt qua từng ngày trong tháng
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Thống kê số đơn và số sản phẩm bán ra trong ngày
                int numberOfBills = b.Count(item => item.CreateDate?.Date == date.Date);
                int numberOfProducts = b.Where(item => item.CreateDate?.Date == date.Date).Sum(item => item.Quantity);

                // Tạo đối tượng Count và thêm vào danh sách countList
                Count count = new Count { Dem1 = numberOfProducts, Date = date.ToString("dd") };
                productCounts.Add(count);
            }
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Thống kê số hóa đơn trong ngày
                int numberOfBills = b
                    .Where(item => item.CreateDate?.Date == date.Date)
                    .GroupBy(item => item.BillID)
                    .Count();

                // Tạo đối tượng Count và thêm vào danh sách countList
                Count count = new Count { Dem1 = numberOfBills, Date = date.ToString("dd") };
                _billCounts.Add(count);
            }
            var d = _billCounts.ToList();
            var a = productCounts.ToList();
            var data = new ChartData
            {
                Labels = productCounts.Select(x=>x.Date).ToList(),
                Datasets = new List<IChartDataset>()
                {
                    new BarChartDataset()
                    {
                        Label = "Sản phẩm",
                        Data=productCounts.Select(x=>x.Dem1).ToList(),
                        BackgroundColor = new List<string>{ "rgb(88, 80, 141)" },
                        CategoryPercentage = 0.8,
                        BarPercentage = 1,
                    },
                    new BarChartDataset()
                    {
                        Label = "Hoá đơn",
                        Data = _billCounts.Select(x=>x.Dem1).ToList(),
                        BackgroundColor = new List<string> { "rgb(255, 166, 0)" },
                        CategoryPercentage = 0.8,
                        BarPercentage = 1,
                    }
                }
            };

            var options = new BarChartOptions();

            options.Interaction.Mode = InteractionMode.Index;

            options.Plugins.Title.Text = "Doanh số tháng này";
            options.Plugins.Title.Display = true;
            options.Plugins.Title.Font.Size = 20;

            options.Responsive = true;

            options.Scales.X.Title.Text = "Ngày";
            options.Scales.X.Title.Display = true;

            options.Scales.Y.Title.Text = "Số lượng";
            options.Scales.Y.Title.Display = true;
            await barChart.InitializeAsync(data, options);
        }

        private async Task RenderWormAsync()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            // Duyệt qua từng ngày trong tháng
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Thống kê doanh thu trong ngày
                int? totalRevenue = _lstBill
                    .Where(bill => bill.CreateDate?.Date == date.Date)
                    .Sum(bill => bill.TotalAmount);

                // Tạo đối tượng Count và thêm vào danh sách countList
                Count count = new Count { Dem1 = (int)totalRevenue, Date = date.ToString("dd") };
                _RevenueCounts.Add(count);
            }
        
            var data = new ChartData
            {
                Labels = _RevenueCounts.Select(x => x.Date).ToList(),
                Datasets = new List<IChartDataset>()
                {
                    new LineChartDataset()
                    {
                        Label = "Tháng 10",
                        Data = new List<double>{ 1, 1, 8, 19, 24, 266, 39, 47, 56, 66, 75, 88, 95, 1000000, 109, 114, 124, 129, 140, 142 },
                        BackgroundColor = new List<string>{ "rgb(0, 176, 168)" },
                        BorderColor = new List<string>{ "rgb(0, 176, 168)" },
                        BorderWidth = new List<double>{2},
                        HoverBorderWidth = new List<double>{4},
                        PointBackgroundColor = new List<string>{ "rgb(0, 176, 168)" },
                        PointBorderColor = new List<string>{ "rgb(0, 176, 168)" },
                        PointRadius = new List<int>{0}, // hide points
                        PointHoverRadius = new List<int>{4},
                    },                   
                    new LineChartDataset()
                    {
                        Label = "Tháng 11",
                        Data = new List<double>{ 1, 1, 8, 19, 24, 266, 39, 47, 56, 66, 75, 88, 95, 100, 109, 114, 124, 129, 140, 142 },
                        BackgroundColor = new List<string>{ "rgb(255, 166, 0)" },
                        BorderColor = new List<string>{ "rgb(255, 166, 0)" },
                        BorderWidth = new List<double>{2},
                        HoverBorderWidth = new List<double>{4},
                        PointBackgroundColor = new List<string>{ "rgb(255, 166, 0)" },
                        PointBorderColor = new List<string>{ "rgb(255, 166, 0)" },
                        PointRadius = new List<int>{0}, // hide points
                        PointHoverRadius = new List<int>{4},
                    },
                    new LineChartDataset()
                    {
                        Label = "Tháng 12",
                        Data =_RevenueCounts.Select(x=>x.Dem1).ToList(),
                        BackgroundColor = new List<string>{ "rgb(88, 80, 141)" },
                        BorderColor = new List<string>{ "rgb(88, 80, 141)" },
                        BorderWidth = new List<double>{2},
                        HoverBorderWidth = new List<double>{4},
                        PointBackgroundColor = new List<string>{ "rgb(88, 80, 141)" },
                        PointBorderColor = new List<string>{ "rgb(88, 80, 141)" },
                        PointRadius = new List<int>{0}, // hide points
                        PointHoverRadius = new List<int>{4},
                    }
                }
            };

            var options = new LineChartOptions();

            options.Interaction.Mode = InteractionMode.Index;

            options.Plugins.Title.Text = "Doanh thu 3 tháng gần nhất";
            options.Plugins.Title.Display = true;
            options.Plugins.Title.Font.Size = 20;

            options.Responsive = true;

            options.Scales.X.Title.Text = "Overs";
            options.Scales.X.Title.Display = true;

            options.Scales.Y.Title.Text = "Runs";
            options.Scales.Y.Title.Display = true;
            await lineChart.InitializeAsync(data, options);
        }



    }
    public class Count
    {
        public int? Dem { get; set; }
        public string Tittle { get; set; }
        public int? Persen { get; set; }
        public double Dem1 { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
        public Count()
        {

        }
    }


}
