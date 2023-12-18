using BlazorBootstrap;
using DATN_Shared.Models;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class ThongKe
    {
        List<Bill_ShowModel> _lstBill = new List<Bill_ShowModel>();
        List<Bill_ShowModel> _lstBillSale = new List<Bill_ShowModel>();
        List<Bill_ShowModel> _lstBillRevenue = new List<Bill_ShowModel>();
        List<Bill_ShowModel> _lstBillProducts = new List<Bill_ShowModel>();
        List<Bill_ShowModel> _lstBillTopSale = new List<Bill_ShowModel>();
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
        Count count4 = new Count(); // biểu đồ line
        Count count5 = new Count();
        List<Count> productCounts = new List<Count>();
        List<Count> _billCounts = new List<Count>();
        List<Count> _RevenueCounts = new List<Count>();
        List<BillDetailShow> _lstBillDeails = new List<BillDetailShow>();
        List<BillDetailShow> _lstThongKeProductItem = new List<BillDetailShow>();
        List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
        [Inject] NavigationManager _navigationManager { get; set; }
        //var _lstThongKeProductItem;
        [Inject] Blazored.Toast.Services.IToastService _toastService { get; set; } // Khai báo khi cần gọi ở code-behind

        [Inject] public IHttpContextAccessor _ihttpcontextaccessor { get; set; }
        User_VM _user_VM = new User_VM();
        bool isLoader = false;


        private BarChart barChart = new BarChart();
        private LineChart lineChart = new LineChart();
        private DateTime dateTime = DateTime.Today;
        private string _time { get; set; } = String.Empty;
        protected override async Task OnInitializedAsync()
        {
            //isLoader = true;
            _lstBill = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            _lstBill = _lstBill.Where(x => !(x.Status==5 && x.Type==2) && x.Status!=0).ToList();
            _lstProductItem = await _httpClient.GetFromJsonAsync<List<ProductItems>>("https://localhost:7141/api/productitem/get_all_productitem");
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");
            _lstBillDetai = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/get_alll_bill_item_show");
			_lstImg_PI = (await _httpClient.GetFromJsonAsync<List<Image_Join_ProductItem>>("https://localhost:7141/api/Image/GetAllImage_PrductItem")).OrderBy(c => c.STT).ToList();
			await Sale(0);
            await Revenue(1);
            await Products(1);
            await TopSale(1);
            await Task(0);
            //isLoader = false;
            //StateHasChanged();
            var uri = new Uri(_navigationManager.Uri);
            var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
            _time = queryParams["time"];
            if (string.IsNullOrEmpty(_time))
            {
                _time = "month";
            }
            await RenderWormAsync(_time);
            await RenderManhattanAsync();
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        //await ChonTimeThang();
        //        //await RenderManhattanAsync();
                
        //    }
        //    //await RenderWormAsync(currentChartId);
        //    await base.OnAfterRenderAsync(firstRender);

        //}

        private async Task RenderWormAsync(string time)
        {
            if (time == "month")
            {
                await RenderWormAsyncThisMonth();
                count4.Tittle = "Tháng này";
                StateHasChanged();
                return;
            }
            if (time == "3-month")
            {
                await RenderWormAsyncThis3Month();
                count4.Tittle = "3 tháng gần đây";
                StateHasChanged();
                return;
            }
            if (time == "year")
            {
                await RenderWormAsyncThisYeah();
                count4.Tittle = "Năm nay";
                StateHasChanged();
            }
        }
        public async Task Sale(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            a = a.Where(x => !(x.Status == 5 && x.Type == 2) && x.Status != 0).ToList();
            if (option == 0)
            {
                _lstBillSale = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count.Dem = _lstBillSale.Count();
                count.Tittle = "Hôm nay";
            }
            else if (option == 1)
            {
                _lstBillSale = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count.Dem = _lstBillSale.Count();
                count.Tittle = "Tháng này";
            }
            else
            {
                _lstBillSale = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count.Dem = _lstBillSale.Count();
                count.Tittle = "Năm nay";
            }
        }
        public async Task Revenue(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            a = a.Where(x => !(x.Status == 5 && x.Type == 2) && x.Status != 0).ToList();
            if (option == 0)
            {
                _lstBillRevenue = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBillRevenue)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "Hôm nay";
            }
            else if (option == 1)
            {
                _lstBillRevenue = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBillRevenue)
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
                _lstBillRevenue = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count1.Dem = 0;
                foreach (var b in _lstBillRevenue)
                {
                    count1.Dem += b.TotalAmount;
                }
                count1.Tittle = "Năm nay";
            }
        }
        public async Task Products(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            a = a.Where(x => !(x.Status == 5 && x.Type == 2) && x.Status != 0).ToList();
            if (option == 0)
            {
                _lstBillDeails.Clear();
                _lstBillProducts = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count2.Dem = 0;
                foreach (var b in _lstBillProducts)
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
                _lstBillProducts = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count2.Dem = 0;
                foreach (var b in _lstBillProducts)
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
                _lstBillProducts = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count2.Dem = 0;
                foreach (var b in _lstBillProducts)
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
            a = a.Where(x => !(x.Status == 5 && x.Type == 2) && x.Status != 0).ToList();
            if (option == 0)
            {
                count3.Tittle = "Hôm nay";
                _lstBillDeails.Clear();
                _lstBillTopSale = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                foreach (var b in _lstBillTopSale)
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
            else if (option == 1)
            {
                count3.Tittle = "Tháng này";
                _lstBillDeails.Clear();
                _lstBillTopSale = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                foreach (var b in _lstBillTopSale)
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
                                            CostPrice = group.FirstOrDefault()?.CostPrice,
                                            ProductItemId = group.FirstOrDefault()?.ProductItemId ?? default(Guid)
                                        })
                                        .OrderByDescending(group => group.Quantity).Take(5)
                                        .ToList();
            }
            else
            {
                count3.Tittle = "Năm nay";
                _lstBillDeails.Clear();
                _lstBillTopSale = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                foreach (var b in _lstBillTopSale)
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
        public async Task Task(int option)
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            a = a.Where(x => !(x.Status == 5 && x.Type == 2) && x.Status != 0).ToList();
            if (option == 0)
            {
                var d = a.Where(x => x.CreateDate?.Date == _optioSale.Date).ToList();
                count5.Dem2 = d.Where(x => x.Status == 1 || x.Status==2).Count();
                count5.Dem3 = d.Where(x => x.Status == 3).Count();
                count5.Dem4 = d.Where(x => x.Status == 4).Count();
                count5.Dem5 = d.Where(x => x.Status == 0).Count();
                count5.Dem5 = d.Where(x => x.Status == 6).Count();
                count5.Tittle = "Hôm nay";
            }
            else if (option == 1)
            {
                var d = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                count5.Dem2 = d.Where(x => x.Status == 1 || x.Status == 2).Count();
                count5.Dem3 = d.Where(x => x.Status == 3).Count();
                count5.Dem4 = d.Where(x => x.Status == 4).Count();
                count5.Dem5 = d.Where(x => x.Status == 0).Count();
                count5.Dem5 = d.Where(x => x.Status == 6).Count();
                count.Tittle = "Tháng này";
            }
            else
            {
                var d = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year).ToList();
                count5.Dem2 = d.Where(x => x.Status == 1 || x.Status == 2).Count();
                count5.Dem3 = d.Where(x => x.Status == 3).Count();
                count5.Dem4 = d.Where(x => x.Status == 4).Count();
                count5.Dem5 = d.Where(x => x.Status == 0).Count();
                count5.Dem5 = d.Where(x => x.Status == 6).Count();
                count.Tittle = "Năm nay";
            }
        }


        private async Task RenderManhattanAsync()
        {
            var b = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>("https://localhost:7141/api/BillItem/get_alll_bill_item_show");


            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

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


            //List<Count> doanhSoHangThang = new List<Count>();

            //// Lặp qua từng tháng trong năm
            //for (int thang = 1; thang <= 12; thang++)
            //{
            //    // Lấy ngày bắt đầu và ngày kết thúc cho tháng hiện tại
            //    DateTime ngayBatDau = new DateTime(DateTime.Now.Year, thang, 1);
            //    DateTime ngayKetThuc = ngayBatDau.AddMonths(1).AddDays(-1);

            //    // Thống kê số lượng hóa đơn trong tháng
            //    int soLuongHoaDon = b
            //        .Count(item => item.CreateDate?.Date >= ngayBatDau.Date && item.CreateDate?.Date <= ngayKetThuc.Date);

            //    // Tạo đối tượng Count và thêm vào danh sách doanhSoHangThang
            //    Count count = new Count { Dem1 = soLuongHoaDon, Date = ngayBatDau.ToString("MM/yyyy") };
            //    doanhSoHangThang.Add(count);
            //}



            var data = new ChartData
            {
                Labels = productCounts.Select(x => x.Date).ToList(),
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



        public async Task RenderWormAsyncThisMonth()
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
                        Label ="Tháng" + Convert.ToString(startDate.Month),
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

            options.Plugins.Title.Text = "Doanh thu tháng này";
            options.Plugins.Title.Display = true;
            options.Plugins.Title.Font.Size = 20;

            options.Responsive = true;

            options.Scales.X.Title.Text = "Ngày";
            options.Scales.X.Title.Display = true;

            options.Scales.Y.Title.Text = "Doanh thu";
            options.Scales.Y.Title.Display = true;
            await lineChart.InitializeAsync(data, options);
        }
        public async Task RenderWormAsyncThis3Month()
        {
            DateTime startDate = DateTime.Now.AddMonths(-2).AddDays(1 - DateTime.Now.Day);
            DateTime endDate = DateTime.Now;

            var data = new ChartData
            {
                Labels = new List<string>(),
                Datasets = new List<IChartDataset>()
            };

            // Mảng các màu cố định cho các line
            string[] colors = { "rgb(88, 80, 141)", "rgb(18, 110, 130)", "rgb(135, 58, 84)" };

            // Duyệt qua từng tháng trong 3 tháng gần đây
            for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                var monthlyData = new LineChartDataset()
                {
                    Label = date.ToString("MM/yyyy"),
                    Data = new List<double>(),
                    BackgroundColor = new List<string> { colors[data.Datasets.Count % colors.Length] },
                    BorderColor = new List<string> { colors[data.Datasets.Count % colors.Length] },
                    BorderWidth = new List<double> { 2 },
                    HoverBorderWidth = new List<double> { 4 },
                    PointBackgroundColor = new List<string> { colors[data.Datasets.Count % colors.Length] },
                    PointBorderColor = new List<string> { colors[data.Datasets.Count % colors.Length] },
                    PointRadius = new List<int> { 0 },
                    PointHoverRadius = new List<int> { 4 },
                };

                int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

                // Duyệt qua từng ngày trong tháng
                for (int day = 1; day <= daysInMonth; day++)
                {
                    // Thống kê doanh thu trong ngày
                    DateTime monthDate = new DateTime(date.Year, date.Month, day);
                    int? a = _lstBill
                        .Where(bill => bill.CreateDate?.Date == monthDate.Date)
                        .Sum(bill => bill.TotalAmount);
                    double totalRevenue = Convert.ToDouble(a);
                    monthlyData.Data.Add(totalRevenue);
                }

                data.Datasets.Add(monthlyData);
            }

            // Thêm danh sách labels từ 1 đến 31
            data.Labels.AddRange(Enumerable.Range(1, 31).Select(day => day.ToString()));

            var options = new LineChartOptions();

            options.Interaction.Mode = InteractionMode.Index;

            options.Plugins.Title.Text = "Doanh thu 3 tháng gần đây";
            options.Plugins.Title.Display = true;
            options.Plugins.Title.Font.Size = 20;

            options.Responsive = true;

            options.Scales.X.Title.Text = "Ngày";
            options.Scales.X.Title.Display = true;

            options.Scales.Y.Title.Text = "Doanh thu";
            options.Scales.Y.Title.Display = true;

            await lineChart.InitializeAsync(data, options);
        }
        public async Task RenderWormAsyncThisYeah()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);

            List<Count> countList = new List<Count>();

            // Duyệt qua từng tháng trong năm
            for (DateTime date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                // Thống kê doanh thu trong tháng
                int? totalRevenue = _lstBill
                    .Where(bill => bill.CreateDate?.Year == date.Year && bill.CreateDate?.Month == date.Month)
                    .Sum(bill => bill.TotalAmount);

                // Tạo đối tượng Count và thêm vào danh sách countList
                Count count = new Count { Dem1 = (int)totalRevenue, Date = date.ToString("MM/yyyy") };
                countList.Add(count);
            }

            var data = new ChartData
            {
                Labels = countList.Select(x => x.Date).ToList(),
                Datasets = new List<IChartDataset>()
        {
            new LineChartDataset()
            {
                Label = DateTime.Now.Year.ToString(),
                Data = countList.Select(x => x.Dem1).ToList(),
                BackgroundColor = new List<string> { "rgb(88, 80, 141)" },
                BorderColor = new List<string> { "rgb(88, 80, 141)" },
                BorderWidth = new List<double> { 2 },
                HoverBorderWidth= new List<double> { 4 },
                PointBackgroundColor = new List<string> { "rgb(88, 80, 141)" },
                PointBorderColor = new List<string> { "rgb(88, 80, 141)" },
                PointRadius = new List<int> { 0 }, // hide points
                PointHoverRadius = new List<int> { 4 },
            }
        }
            };

            var options = new LineChartOptions();

            options.Interaction.Mode = InteractionMode.Index;

            options.Plugins.Title.Text = "Doanh thu năm " + DateTime.Now.Year.ToString();
            options.Plugins.Title.Display = true;
            options.Plugins.Title.Font.Size = 20;

            options.Responsive = true;

            options.Scales.X.Title.Text = "Tháng";
            options.Scales.X.Title.Display = true;

            options.Scales.Y.Title.Text = "Doanh thu";
            options.Scales.Y.Title.Display = true;

            await lineChart.InitializeAsync(data, options);
        }
        public async Task ExportExcel()
        {
            var a = await _httpClient.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Hôm nay");
                //var worksheet = package.Workbook.Worksheets.Add("Tháng này");
                //var worksheet = package.Workbook.Worksheets.Add("Hôm nay");

                _lstBillDeails.Clear();
                _lstBill = a.Where(x => x.CreateDate?.Year == DateTime.Now.Year && x.CreateDate?.Month == DateTime.Now.Month).ToList();
                foreach (var b in _lstBill)
                {
                    var _lstBillitem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstBillitem);
                }
                var d = _lstBillDeails
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
                var bill = a.Where(x => x.CreateDate?.Date == _optioSale.Date).Count();
                var billcancel = a.Where(x => x.CreateDate?.Date == _optioSale.Date && x.Status == 0).Count();
                int productItem = 0;
                int? doanhthu = 0;

                foreach (var b in _lstBill)
                {
                    var _lstBillitem = await _httpClient.GetFromJsonAsync<List<BillDetailShow>>($"https://localhost:7141/api/BillItem/getbilldetail/{b.Id}");
                    _lstBillDeails.AddRange(_lstBillitem);
                }
                foreach (var c in _lstBillDeails)
                {
                    var _lstBi = await _httpClient.GetFromJsonAsync<List<BillItems>>("https://localhost:7141/api/BillItem/get_alll_bill_item");
                    var pro = _lstBi.FirstOrDefault(x => x.Id == c.Id);
                    productItem += pro.Quantity;
                }
                foreach (var b in _lstBill)
                {
                    doanhthu += b.TotalAmount;
                }
                // Merge hai dòng đầu
                worksheet.Cells["A1:H2"].Merge = true;

                // Ghi dòng chữ "Danh sách khuyến mại của BH Unisex" và căn giữa
                worksheet.Cells["A1"].Value = "Báo cáo doanh số và doanh thu BH Unisex ";
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 20;

                worksheet.Cells[3, 2].Value = "Hoá đơn";
                worksheet.Cells[3, 6].Value = "Ngày";
                worksheet.Cells[4, 2].Value = "Đơn bị huỷ";
                worksheet.Cells[5, 2].Value = "Sản phẩm";
                worksheet.Cells[6, 2].Value = "Doanh thu";

                worksheet.Cells[3, 3].Value = bill;
                worksheet.Cells[3, 7].Value = dateTime;
                worksheet.Cells[4, 3].Value = billcancel;
                worksheet.Cells[5, 3].Value = productItem;
                worksheet.Cells[6, 3].Value = doanhthu;
                worksheet.Cells[6, 3].Style.Numberformat.Format = "₫#,##0";
                worksheet.Cells[3, 7].Style.Numberformat.Format = "dd/MM/yyyy";

                worksheet.Cells[8, 1].Value = "STT";
                worksheet.Cells[8, 2].Value = "Mã sản phẩm";
                worksheet.Cells[8, 3].Value = "Tên sản phẩm";
                worksheet.Cells[8, 4].Value = "Size";
                worksheet.Cells[8, 5].Value = "Màu";
                worksheet.Cells[8, 6].Value = "Số lượng bán";
                worksheet.Cells[8, 7].Value = "Danh thu";

                var headerRange = worksheet.Cells[8, 1, 8, 7];  // cái này để tạo viền
                headerRange.Style.Font.Bold = true;  // in đậm 
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;  // căn giữa
                headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                for (int i = 1; i <= 7; i++)
                {
                    worksheet.Column(i).Width = worksheet.Cells[8, i].Value.ToString().Length + 5;
                }
                for (int i = 0; i < d.Count; i++)
                {
                    BillDetailShow promotion = d[i];
                    worksheet.Cells[i + 9, 1].Value = i;
                    worksheet.Cells[i + 9, 2].Value = promotion.ProductCode;
                    worksheet.Cells[i + 9, 3].Value = promotion.Name;
                    worksheet.Cells[i + 9, 4].Value = promotion.SizeName;
                    worksheet.Cells[i + 9, 5].Value = promotion.ColorName;
                    worksheet.Cells[i + 9, 6].Value = promotion.Quantity;
                    worksheet.Cells[i + 9, 7].Value = promotion.CostPrice * promotion.Quantity;


                    worksheet.Cells[i + 9, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;  // căn giữa 
                    worksheet.Cells[i + 9, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 9, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 9, 7].Style.Numberformat.Format = "₫#,##0";

                    var dataRange = worksheet.Cells[i + 9, 1, i + 9, 7];
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa 
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    // ...
                }




                var stream = new MemoryStream(package.GetAsByteArray());
                var fileName = "myobjects.xlsx"; // Tên file mặc định
                await JSRuntime.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(stream.ToArray()));

            }
        }



        private async Task ChonTimeThang(string time)
        {
            var currentUrl = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

            // Thêm thông tin vào URL parameters
            var uriBuilder = new UriBuilder(currentUrl);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            if (!string.IsNullOrEmpty(time))
            {
                query["time"] = time.ToString();
            }
            uriBuilder.Query = query.ToString();
            // Chuyển đến URL mới
            _navigationManager.NavigateTo(uriBuilder.ToString(),true);
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
        public int Dem2 { get; set; }
        public int Dem3 { get; set; }
        public int Dem4 { get; set; }
        public int Dem5 { get; set; }
        public int Dem6 { get; set; }
        public Count()
        {

        }
    }


}
