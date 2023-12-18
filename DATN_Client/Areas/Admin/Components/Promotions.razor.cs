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
        List<BillTest> fakeData = new List<BillTest>();

        private string fileName;
        private string filePath;

        protected override async Task OnInitializedAsync()
        {
            _lstPromotion = await _httpClient.GetFromJsonAsync<List<Promotions_VM>>("https://localhost:7141/api/promotion");

            fakeData = new List<BillTest>
                {
                    new BillTest { Name = "Product 1", SoLuong = 5, DonGia = 1000, ThanhTien = 5000 },
                    new BillTest { Name = "Product 2", SoLuong = 3, DonGia = 2000, ThanhTien = 6000 },
                    new BillTest { Name = "Product 3", SoLuong = 2, DonGia = 1500, ThanhTien = 3000 },
                    new BillTest { Name = "Product 4", SoLuong = 6, DonGia = 800, ThanhTien = 4800 },
                    new BillTest { Name = "Product 5", SoLuong = 4, DonGia = 1200, ThanhTien = 4800 },
                    new BillTest { Name = "Product 6", SoLuong = 7, DonGia = 900, ThanhTien = 6300 },
                    new BillTest { Name = "Product 7", SoLuong = 1, DonGia = 2500, ThanhTien = 2500 },
                    new BillTest { Name = "Product 8", SoLuong = 9, DonGia = 700, ThanhTien = 6300 },
                    new BillTest { Name = "Product 9", SoLuong = 2, DonGia = 1800, ThanhTien = 3600 },
                    new BillTest { Name = "Product 10", SoLuong = 5, DonGia = 1100, ThanhTien = 5500 }
                };


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
                await JSRuntime.InvokeVoidAsync("saveAsFile", fileName, Convert.ToBase64String(stream.ToArray()));

            }
        }

        //private async Task ExportExcel1()
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
        //        int a =fakeData.Count;

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

        //private async Task ExportExcel()
        //{
        //    // Tạo tài liệu PDF
        //    var document = new Document();
        //    var stream = new MemoryStream();

        //    PdfWriter writer = PdfWriter.GetInstance(document, stream);
        //    document.Open();

        //    // Tạo nội dung PDF
        //    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
        //    var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        //    var dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

        //    var title = new Paragraph("HOA DON BAN HANG", titleFont);
        //    title.Alignment = Element.ALIGN_CENTER;
        //    document.Add(title);

        //    document.Add(new Paragraph("Ten khach hang: Nguyen Van Thang", dataFont));
        //    document.Add(new Paragraph("Dia chi: ", dataFont));
        //    document.Add(new Paragraph("SDT: 0367180646", dataFont));
        //    document.Add(new Paragraph("Ma hoa don: ", dataFont));
        //    document.Add(new Paragraph("Ngay: 18/12/2023", dataFont));

        //    var table = new PdfPTable(5);

        //    var sttHeader = new PdfPCell(new Phrase("STT", headerFont));
        //    sttHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //    table.AddCell(sttHeader);

        //    var tenSanPhamHeader = new PdfPCell(new Phrase("TEN SAN PHAM", headerFont));
        //    tenSanPhamHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //    table.AddCell(tenSanPhamHeader);

        //    var donGiaHeader = new PdfPCell(new Phrase("DON GIA", headerFont));
        //    donGiaHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //    table.AddCell(donGiaHeader);

        //    var soLuongHeader = new PdfPCell(new Phrase("SO LUONG", headerFont));
        //    soLuongHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //    table.AddCell(soLuongHeader);

        //    var thanhTienHeader = new PdfPCell(new Phrase("THANH TIEN", headerFont));
        //    thanhTienHeader.HorizontalAlignment = Element.ALIGN_CENTER;
        //    table.AddCell(thanhTienHeader);

        //    for (int i = 0; i < fakeData.Count; i++)
        //    {
        //        BillTest billTest = fakeData[i];
        //        table.AddCell(new Phrase(i.ToString(), dataFont));
        //        table.AddCell(new Phrase(billTest.Name, dataFont));
        //        table.AddCell(new Phrase(billTest.DonGia.ToString(), dataFont));
        //        table.AddCell(new Phrase(billTest.SoLuong.ToString(), dataFont));
        //        table.AddCell(new Phrase(billTest.ThanhTien.ToString(), dataFont));
        //    }

        //    document.Add(table);

        //    document.Add(new Paragraph("Tong tien: 100.000.000đ", dataFont));
        //    document.Add(new Paragraph("Voucher tu shop: 0", dataFont));
        //    document.Add(new Paragraph("Su dung diem: 0", dataFont));
        //    document.Add(new Paragraph("Tong thanh toan: 100.000.000đ", dataFont));
        //    document.Add(new Paragraph("Tien thanh chu: mot tram trieu dong", dataFont));

        //    document.Close();

        //    // Hiển thị tài liệu PDF trên trình duyệt
        //    var pdfBytes = stream.ToArray();
        //    var pdfBase64 = Convert.ToBase64String(pdfBytes);

        //    _navigationManager.NavigateTo($"/pdfviewer?PDFBase64={Uri.EscapeDataString(pdfBase64)}", forceLoad: true);


        //}
        //public async Task ExportExcel3(string idiframe)
        //{
        //    var pdf=new GennerPDF();
        //    pdf.ViewPdf(JSRuntime, idiframe);
        //}
        private byte[] PDFReport()
        {
            var document = new Document();
            var stream = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            // Tạo nội dung PDF
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            var title = new Paragraph("HOA DON BAN HANG", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            document.Add(new Paragraph("Ten khach hang: Nguyen Van Thang", dataFont));
            document.Add(new Paragraph("Dia chi: ", dataFont));
            document.Add(new Paragraph("SDT: 0367180646", dataFont));
            document.Add(new Paragraph("Ma hoa don: ", dataFont));
            document.Add(new Paragraph("Ngay: 18/12/2023", dataFont));

            var table = new PdfPTable(5);

            var sttHeader = new PdfPCell(new Phrase("STT", headerFont));
            sttHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(sttHeader);

            var tenSanPhamHeader = new PdfPCell(new Phrase("TEN SAN PHAM", headerFont));
            tenSanPhamHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(tenSanPhamHeader);

            var donGiaHeader = new PdfPCell(new Phrase("DON GIA", headerFont));
            donGiaHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(donGiaHeader);

            var soLuongHeader = new PdfPCell(new Phrase("SO LUONG", headerFont));
            soLuongHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(soLuongHeader);

            var thanhTienHeader = new PdfPCell(new Phrase("THANH TIEN", headerFont));
            thanhTienHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(thanhTienHeader);

            for (int i = 0; i < fakeData.Count; i++)
            {
                BillTest billTest = fakeData[i];
                table.AddCell(new Phrase(i.ToString(), dataFont));
                table.AddCell(new Phrase(billTest.Name, dataFont));
                table.AddCell(new Phrase(billTest.DonGia.ToString(), dataFont));
                table.AddCell(new Phrase(billTest.SoLuong.ToString(), dataFont));
                table.AddCell(new Phrase(billTest.ThanhTien.ToString(), dataFont));
            }

            document.Add(table);

            document.Add(new Paragraph("Tong tien: 100.000.000đ", dataFont));
            document.Add(new Paragraph("Voucher tu shop: 0", dataFont));
            document.Add(new Paragraph("Su dung diem: 0", dataFont));
            document.Add(new Paragraph("Tong thanh toan: 100.000.000đ", dataFont));
            document.Add(new Paragraph("Tien thanh chu: mot tram trieu dong", dataFont));

            document.Close();

            // Hiển thị tài liệu PDF trên trình duyệt
            var pdfBytes = stream.ToArray();
            var pdfBase64 = Convert.ToBase64String(pdfBytes);
            return pdfBytes;
        }

        public class BillTest
        {
            public string Name { get; set; }
            public int SoLuong { get; set; }
            public int DonGia { get; set; }
            public int ThanhTien { get; set; }
        }
    }

}
