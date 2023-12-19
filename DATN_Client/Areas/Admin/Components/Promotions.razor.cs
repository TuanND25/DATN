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

        //private async Task ExportExcel2()
        //{
        //    //Bill_ShowModel _bill

        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var package = new ExcelPackage())
        //    {
        //        var worksheet = package.Workbook.Worksheets.Add("Sheet1");
        //        // Merge hai dòng đầu
        //        worksheet.Cells["A1:B1"].Merge = true;
        //        worksheet.Cells["A2:B2"].Merge = true;
        //        worksheet.Cells["A3:B3"].Merge = true;
        //        worksheet.Cells["A4:C4"].Merge = true;
        //        worksheet.Cells["A5:C5"].Merge = true;
        //        worksheet.Cells["A6:C6"].Merge = true;
        //        worksheet.Cells["D4:E4"].Merge = true;
        //        worksheet.Cells["D5:E5"].Merge = true;
        //        worksheet.Cells["C1:E3"].Merge = true;



        //        worksheet.Cells["A1:B1"].Value = "BH UNISEX";
        //        worksheet.Cells["A1:B1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        worksheet.Cells["A1:B1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //        worksheet.Cells["A1:B1"].Style.Font.Size = 18;

        //        worksheet.Cells["A2:B2"].Value = "Dia chi: 22 ngo 132 Cau Dien, Bac Tu Liem, Ha Noi";
        //        worksheet.Cells["A2:B2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        worksheet.Cells["A2:B2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //        worksheet.Cells["A2:B2"].Style.Font.Size = 9;
        //        worksheet.Cells["A2:B2"].EntireColumn.AutoFit();

        //        worksheet.Cells["A3:B3"].Value = "SDT: 0367180646";
        //        worksheet.Cells["A3:B3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        worksheet.Cells["A3:B3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //        worksheet.Cells["A3:B3"].Style.Font.Size = 9;

        //        worksheet.Cells["C1:E3"].Value = "HOA DON BAN HANG";
        //        worksheet.Cells["C1:E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        worksheet.Cells["C1:E3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        //        worksheet.Cells["C1:E3"].Style.Font.Bold = true;
        //        worksheet.Cells["C1:E3"].Style.Font.Size = 20;


        //        // bắt đầu điền thuộc tính vào trong này nha 
        //        worksheet.Cells["A4:C4"].Value = "Ten khach hang: Nguyen Van Thang";  /// "ten khách hang" + _bill.rédf
        //        worksheet.Cells["A5:C5"].Value = "Dia chi: ";
        //        worksheet.Cells["A6:C6"].Value = "SDT: 0367180646";
        //        worksheet.Cells["D4:E4"].Value = "Ma hoa don: ";
        //        worksheet.Cells["D5:E5"].Value = "Ngay: 18/12/2023";

        //        worksheet.Cells["A4:C4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        worksheet.Cells["A5:C5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        worksheet.Cells["A6:C6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        worksheet.Cells["D4:E5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        worksheet.Cells["D5:E5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

        //        worksheet.Cells[8, 1].Value = "STT";
        //        worksheet.Cells[8, 1].Style.Font.Bold = true;
        //        worksheet.Cells[8, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //        worksheet.Cells[8, 2].Value = "TEN SAN PHAM";
        //        worksheet.Cells[8, 2].Style.Font.Bold = true;
        //        worksheet.Cells[8, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //        worksheet.Cells[8, 3].Value = "DON GIA";
        //        worksheet.Cells[8, 3].Style.Font.Bold = true;
        //        worksheet.Cells[8, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //        worksheet.Cells[8, 4].Value = "SO LUONG";
        //        worksheet.Cells[8, 4].Style.Font.Bold = true;
        //        worksheet.Cells[8, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //        worksheet.Cells[8, 5].Value = "THANH TIEN";
        //        worksheet.Cells[8, 5].Style.Font.Bold = true;
        //        worksheet.Cells[8, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //        worksheet.Column(1).Width = 5;
        //        worksheet.Column(2).Width = 35;
        //        worksheet.Column(3).Width = 14;
        //        worksheet.Column(4).Width = 15;
        //        worksheet.Column(5).Width = 15;


        //        var headerRange = worksheet.Cells[8, 1, 8, 5];  // cái này để tạo viền
        //        headerRange.Style.Font.Bold = true;  // in đậm 
        //        headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;  // căn giữa
        //        headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //        headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //        headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //        headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

        //        for (int i = 0; i < fakeData.Count; i++)
        //        {
        //            BillTest billTest = fakeData[i];
        //            worksheet.Cells[i + 9, 1].Value = i;
        //            worksheet.Cells[i + 9, 2].Value = billTest.Name;
        //            worksheet.Cells[i + 9, 3].Value = billTest.DonGia;
        //            worksheet.Cells[i + 9, 4].Value = billTest.SoLuong;
        //            worksheet.Cells[i + 9, 5].Value = billTest.ThanhTien;


        //            var dataRange = worksheet.Cells[i + 9, 1, i + 9, 5];
        //            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa 
        //            dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //            dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //        }
        //        int a = fakeData.Count;

        //        worksheet.Cells[a + 9, 4].Value = "Tong tien: ";
        //        worksheet.Cells[a + 9, 5].Value = "100.000.000đ";
        //        worksheet.Cells[a + 10, 4].Value = "Voucher tu shop: ";
        //        worksheet.Cells[a + 10, 5].Value = "0";
        //        worksheet.Cells[a + 11, 4].Value = "Su dung diem: ";
        //        worksheet.Cells[a + 11, 5].Value = "0 ";
        //        worksheet.Cells[a + 12, 4].Value = "Tong thanh toan: ";
        //        worksheet.Cells[a + 12, 5].Value = "100.000.000đ ";


        //        int startRow = a + 14; // Dòng bắt đầu của phạm vi merge
        //        int endRow = a + 14;   // Dòng kết thúc của phạm vi merge
        //        int startColumn = 1;   // Cột bắt đầu của phạm vi merge
        //        int endColumn = 5;     // Cột kết thúc của phạm vi merge

        //        worksheet.Cells[startRow, startColumn, endRow, endColumn].Merge = true;
        //        worksheet.Cells[a + 14, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        worksheet.Cells[a + 14, 1].Value = "Tien thanh chu: mot tram trieu dong";

        //        var stream = new MemoryStream(package.GetAsByteArray());
        //        var fileName = "myobjects.xlsx"; // Tên file mặc định
        //        await JSRuntime.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(stream.ToArray()));
        //    }

        //}

        //private async Task ExportPDFHoaDon(Bill_ShowModel _bill,List<BillDetailShow> _lstBillItem)
        //{
        //    // Tạo tệp PDF
        //    var document = new Document();
        //    var stream = new MemoryStream();
        //    var writer = PdfWriter.GetInstance(document, stream);
        //    document.Open();

        //    // Thêm tiêu đề
        //    var titleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD);
        //    var title = new Paragraph("BH UNISEX", titleFont);
        //    title.Alignment = Element.ALIGN_CENTER;
        //    document.Add(title);

        //    // Thêm địa chỉ
        //    var addressFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9);
        //    var address = new Paragraph("Dia chi: 22 ngo 132 Cau Dien, Bac Tu Liem, Ha Noi", addressFont);
        //    address.Alignment = Element.ALIGN_CENTER;
        //    document.Add(address);

        //    // Thêm số điện thoại
        //    var phoneFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9);
        //    var phone = new Paragraph("SDT: 0367180646", phoneFont);
        //    phone.Alignment = Element.ALIGN_CENTER;
        //    document.Add(phone);

        //    // Thêm tiêu đề hóa đơn
        //    var billTitleFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD);
        //    var billTitle = new Paragraph("HOA DON BAN HANG", billTitleFont);
        //    billTitle.Alignment = Element.ALIGN_CENTER;
        //    document.Add(billTitle);

        //    // Thêm thông tin khách hàng
        //    var customerInfo = new Chunk("Ten khach hang: "+ _bill.UserName);
        //    document.Add(new Paragraph(customerInfo));
        //    document.Add(new Paragraph("Dia chi: " +_bill.ToAddress +","+_bill.WardName + "," +_bill.District + "," + _bill.Province));
        //    document.Add(new Paragraph("SDT: " + _bill.PhoneNumber));
        //    document.Add(new Paragraph("Ma hoa don: " +_bill.BillCode));
        //    document.Add(new Paragraph("Ngay: " +_bill.CreateDate?.ToString("HH:mm dd/MM/yyyy")));
        //    Paragraph paragraph = new Paragraph();
        //    paragraph.SpacingAfter = 10; // Thêm khoảng cách 10 điểm ảnh
        //    document.Add(paragraph);


        //    // Thêm bảng sản phẩm
        //    var table = new PdfPTable(5);
        //    table.WidthPercentage = 100;

        //    float[] columnWidths = { 0.5f, 3f, 1.5f, 1.5f, 1.5f };
        //    table.SetWidths(columnWidths);

        //    table.AddCell("STT");
        //    table.AddCell("TEN SAN PHAM");
        //    table.AddCell("DON GIA");
        //    table.AddCell("SO LUONG");
        //    table.AddCell("THANH TIEN");


        //    for (int i = 0; i < _lstBillItem.Count; i++)
        //    {
        //        BillDetailShow billTest = _lstBillItem[i];
        //        table.AddCell((i + 1).ToString());
        //        table.AddCell(billTest.Name);
        //        table.AddCell(billTest.PriceAfter.ToString());
        //        table.AddCell(billTest.Quantity.ToString());
        //        table.AddCell((billTest.PriceAfter*billTest.Quantity)?.ToString("#,##0")+"đ");
        //    }

        //    table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;



        //    table.AddCell("");
        //    table.AddCell("");
        //    table.AddCell("");
        //    table.AddCell("Phi ship: ");
        //    table.AddCell(_bill.ShippingFee?.ToString("#,##0") + "đ");


        //    table.AddCell("");
        //    table.AddCell("");
        //    table.AddCell("");
        //    table.AddCell("Tong tien: ");
        //    table.AddCell(_bill.FinalAmount?.ToString("#,##0") + "đ");





        //    // Thêm ô tiền thành chữ
        //    PdfPCell mergedCell = new PdfPCell(new Phrase("Tien thanh chu: Mot tram trieu dong"));
        //    mergedCell.Colspan = 5; // Hợp nhất qua 5 cột
        //    mergedCell.Rowspan = 1; // Hợp nhất qua 1 dòng
        //    mergedCell.Border =iTextSharp.text.Rectangle.NO_BORDER; // Loại bỏ đường viền
        //    table.AddCell(mergedCell);
        //    table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    //table.DefaultCell.Colspan = 5;
        //    table.DefaultCell.Border =iTextSharp.text.Rectangle.NO_BORDER;
        //    table.AddCell("");


        //    document.Add(table);

        //    document.Close();

        //    // Lưu tệp PDF
        //    var fileName = $"HoaDonBanHang{_bill.BillCode}.pdf"; // Tên file mặc định
        //    await JSRuntime.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(stream.ToArray()));


        //}

    }

}
