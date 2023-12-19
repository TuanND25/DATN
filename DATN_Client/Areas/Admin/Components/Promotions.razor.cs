using ClosedXML.Excel;
using DATN_Client.Areas.Admin.Controllers;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.JSInterop;
using System.Drawing;
using OfficeOpenXml.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class Promotions
    {
        HttpClient _httpClient = new HttpClient();
        [Inject] NavigationManager _navigationManager { get; set; }

        //[Inject] PromotionController _navPromotion { get; set; }

        List<Promotions_VM> _lstPromotion = new List<Promotions_VM>();
        private Promotions_VM _promotion_VM = new Promotions_VM();
        private int selectedValue = 0;
        private int selectedSort = 0;
        private int statusValue;
        private DateTime StartDateValue = new DateTime(2000, 1, 1);
        private DateTime EndDateValue = new DateTime(2000, 1, 1);
        private string? _promotionName = null;
        //private JSRuntime _runtime;

        private string fileName;
        private string filePath;

        protected override async Task OnInitializedAsync()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");
        }

        public async Task NavigationAddPromotion()
        {
            _navigationManager.NavigateTo("/promotion-management/add", true);
        }


        public async Task NavigationUpdatePromotion(Promotions_VM promotionVM)
        {
            _promotion_VM = promotionVM;
            _navigationManager.NavigateTo($"/promotion-management/update?id={promotionVM.Id}", true);
        }
        public async Task DeletePromotion(Promotions_VM promotionVM)
        {
            //_promotion_VM = promotionVM;
            promotionVM.Status = 0;

            try
            {
                var a = await _httpClient.PutAsJsonAsync<Promotions_VM>("https://localhost:7141/api/promotion/update", promotionVM);
            }
            catch (Exception)
            {

                throw;
            }

            var d = await _httpClient.GetFromJsonAsync<List<ProductItem_VM>>($"https://localhost:7141/api/productitem/ProductItem_By_PromotionId/{promotionVM.Id}");
            foreach (var item in d)
            {
                var productItem = await _httpClient.GetFromJsonAsync<ProductItem_VM>($"https://localhost:7141/api/productitem/get_all_productitem_byID/{item.Id}");
                productItem.PriceAfterReduction = productItem.CostPrice;
                var t = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/productitem/update_productitem", productItem);
            }
            var e = await _httpClient.GetFromJsonAsync<List<PromotionItem_VM>>($"https://localhost:7141/api/PromotionItem/PromotionItem_By_Promotion/{_promotion_VM.Id}");
            foreach (var item in e)
            {
                item.Status = 0;
                var f = await _httpClient.PutAsJsonAsync("https://localhost:7141/api/PromotionItem/update", item);
            }
            _navigationManager.NavigateTo("/promotion-management", true);
        }
        public async Task Search()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");
            _lstPromotion = _lstPromotion.Where(x => x.Name == null || x.Name == string.Empty || x.Name.ToLower().Contains(_promotionName.ToLower())).ToList();
        }
        // status 0 1 
        public async Task Loc()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");

            if (selectedValue == 1)
            {
                statusValue = 1;
            }
            else if (selectedValue == 2)
            {
                statusValue = 0;
            }
            else
            {
                statusValue = 3; // selectValue = 0;
            }

            if (selectedSort == 0)
            {
                _lstPromotion = _lstPromotion.Where(x => (statusValue == 3 || x.Status == statusValue) && (StartDateValue == new DateTime(2000, 1, 1) || x.StartDate >= StartDateValue) && (EndDateValue == new DateTime(2000, 1, 1) || x.EndDate <= EndDateValue) && (selectedSort == 0 || selectedSort == 1 || selectedSort == 2)).ToList();
            }
            else if (selectedSort == 1)
            {
                _lstPromotion = _lstPromotion.Where(x => (statusValue == 3 || x.Status == statusValue) && (StartDateValue == new DateTime(2000, 1, 1) || x.StartDate >= StartDateValue) && (EndDateValue == new DateTime(2000, 1, 1) || x.EndDate <= EndDateValue)).OrderByDescending(c => c.Percent).ToList();
            }
            else if (selectedSort == 2)
            {
                _lstPromotion = _lstPromotion.Where(x => (statusValue == 3 || x.Status == statusValue) && (StartDateValue == new DateTime(2000, 1, 1) || x.StartDate >= StartDateValue) && (EndDateValue == new DateTime(2000, 1, 1) || x.EndDate <= EndDateValue)).OrderBy(c => c.Percent).ToList();
            }
        }


        public async Task ExportExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                // Merge hai dòng đầu
                worksheet.Cells["A1:H2"].Merge = true;

                // Ghi dòng chữ "Danh sách khuyến mại của BH Unisex" và căn giữa
                worksheet.Cells["A1"].Value = "Danh sách khuyến mại của BH Unisex";
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 20;

                // Ghi các tiêu đề cột
                worksheet.Cells[3, 1].Value = "STT";
                worksheet.Cells[3, 2].Value = "Tên khuyến mại";
                worksheet.Cells[3, 3].Value = "Phần trăm giảm";
                worksheet.Cells[3, 4].Value = "Số lượng";
                worksheet.Cells[3, 5].Value = "Ngày bắt đầu";
                worksheet.Cells[3, 6].Value = "Ngày kết thúc";
                worksheet.Cells[3, 7].Value = "Mô tả";
                worksheet.Cells[3, 8].Value = "Tình trạng";

                var headerRange = worksheet.Cells[3, 1, 3, 8];  // cái này để tạo viền
                headerRange.Style.Font.Bold = true;  // in đậm 
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;  // căn giữa
                headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                // ...

                // Thiết lập độ rộng của các cột
                for (int i = 1; i <= 8; i++)
                {
                    worksheet.Column(i).Width = worksheet.Cells[3, i].Value.ToString().Length + 5;
                }
                worksheet.Column(5).Width = 20; // Đặt độ rộng cột "Ngày bắt đầu" là 20
                worksheet.Column(6).Width = 20; // Đặt độ rộng cột "Ngày bắt đầu" là 20
                worksheet.Column(6).Width = 25; // Đặt độ rộng cột "Ngày bắt đầu" là 20
                // Ghi dữ liệu từ danh sách đối tượng vào các ô tương ứng
                for (int i = 0; i < _lstPromotion.Count; i++)
                {
                    Promotions_VM promotion = _lstPromotion[i];
                    worksheet.Cells[i + 4, 1].Value = i;
                    worksheet.Cells[i + 4, 2].Value = promotion.Name;
                    worksheet.Cells[i + 4, 3].Value = promotion.Percent;
                    worksheet.Cells[i + 4, 4].Value = promotion.Quantity;
                    worksheet.Cells[i + 4, 5].Value = promotion.StartDate;
                    worksheet.Cells[i + 4, 6].Value = promotion.EndDate;
                    worksheet.Cells[i + 4, 7].Value = promotion.Description;
                    worksheet.Cells[i + 4, 8].Value = promotion.Status;


                    worksheet.Cells[i + 4, 5].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss"; // định dạng ngày và giờ 
                    worksheet.Cells[i + 4, 6].Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
                    worksheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;  // căn giữa 
                    worksheet.Cells[i + 4, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 4, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    var dataRange = worksheet.Cells[i + 4, 1, i + 4, 8];
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa 
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    // ...
                }


                var stream = new MemoryStream(package.GetAsByteArray());
                var fileName = "myobjects.xlsx"; // Tên file mặc định
                await JSRuntime1.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(stream.ToArray()));

            }
        }

    }

}
