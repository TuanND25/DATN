
using DATN_API.Data;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Text;


namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PDFController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePDF(Guid Id)
        {
            var detailbill =  from user in _context.Users
                             join billuser in _context.Bills on user.Id equals billuser.UserId
                             join billitem in _context.BillItems on billuser.Id equals billitem.BillId
                             join productitem in _context.ProductItems on billitem.ProductItemsId equals productitem.Id
                             join color in _context.Colors on productitem.ColorId equals color.Id
                             join size in _context.Sizes on productitem.SizeId equals size.Id
                             join product in _context.Products on productitem.ProductId equals product.Id
                             where billuser.Id == Id
                             select new PDFBill
                             {
                                 NameCustomer = user.Name,
                                 BillCode = billuser.BillCode,
                                 ProductCode = product.ProductCode,
                                 Description = product.Description,
                                 Size = size.Name,
                                 Color = color.Name,
                                 Quantity = billitem.Quantity,
                                 Price = billitem.Price,
                                 Total = billitem.Quantity * billitem.Price,
                                 Address = billuser.ToAddress,
                                 PhoneNumber = billuser.NumberPhone,

                             };
           var Bill = _context.Bills.FirstOrDefault(b => b.Id == Id);

            var document = new PdfDocument();
     

            string[] copies = { "Customer copy", "Comapny Copy" };
            for (int i = 0; i < copies.Length; i++)
            {
               
                string htmlcontent = "<div style='width:100%; text-align:center'>";
      
                htmlcontent += "<h2>" + copies[i] + "</h2>";
                htmlcontent += "<h2>Welcome to BHunisex</h2>";



               
                    htmlcontent += "<h2> Invoice No:" +Bill.BillCode + " & Invoice Date:" + DateTime.Now+ "</h2>";
                    htmlcontent += "<h3> Customer : " + detailbill.First().NameCustomer + "</h3>";
                    htmlcontent += "<p>" + Bill.ToAddress + "</p>";
                    htmlcontent += "<h3> Contact : 9898989898 & Email :bhunisex@gmail.com </h3>";
                    htmlcontent += "<div>";
                



                htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
                htmlcontent += "<thead style='font-weight:bold'>";
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000'> Product Code </td>";
                htmlcontent += "<td style='border:1px solid #000'> Màu sắc</td>";
                htmlcontent += "<td style='border:1px solid #000'> Kích thước </td>";
                htmlcontent += "<td style='border:1px solid #000'>Số lượng</td>";
                htmlcontent += "<td style='border:1px solid #000'>Giá bán</td >";
                htmlcontent += "<td style='border:1px solid #000'>Total</td>";
                htmlcontent += "</tr>";
                htmlcontent += "</thead >";

                htmlcontent += "<tbody>";
                double TotalBill = 0;
                if (detailbill != null)
                {
                    foreach (var item in detailbill)
                    {
                        htmlcontent += "<tr>";
                        htmlcontent += "<td>" + item.ProductCode + "</td>";
                        htmlcontent += "<td>" + item.Color+ "</td>";
                        htmlcontent += "<td>" + item.Size + "</td >";
                        htmlcontent += "<td>" + item.Quantity + "</td>";
                        htmlcontent += "<td>" + item.Price + "</td>";
                        htmlcontent += "<td> " + item.Total + "</td >";
                        htmlcontent += "</tr>";
                    }
                       
                    
                }
                htmlcontent += "</tbody>";

                htmlcontent += "</table>";
                htmlcontent += "</div>";

                htmlcontent += "<div style='text-align:right'>";
                htmlcontent += "<h1> Summary Info </h1>";
                htmlcontent += "<table style='border:1px solid #000;float:right' >";
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000'> Summary Total </td>";
                htmlcontent += "<td style='border:1px solid #000'> Summary Tax </td>";
                htmlcontent += "<td style='border:1px solid #000'> Summary NetTotal </td>";
                htmlcontent += "</tr>";
                if (detailbill != null)
                {
                    htmlcontent += "<tr>";
                    htmlcontent += "<td style='border: 1px solid #000'> " + TotalBill + " </td>";
                  
                    htmlcontent += "</tr>";
                }
                htmlcontent += "</table>";
                htmlcontent += "</div>";

                htmlcontent += "</div>";

                PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4,10);
            }
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string Filename = "Invoice_" + detailbill.First().BillCode + ".pdf";
            return File(response, "application/pdf", Filename);
        }

    }
}
