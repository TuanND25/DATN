using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_API.Service_IService.Services;
using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
	[Route("api/excel")]
	[ApiController]
	public class ExcelController : ControllerBase
	{
		private readonly IExcelService _excelService; 
		private readonly ApplicationDbContext _context;

		
		public ExcelController(ApplicationDbContext context, ExcelService service)
		{
			_context = context;
			_excelService = service;
			
		}

		[HttpGet("export")]
		public IActionResult ExportExcel()
		{
			
				var data = GetList(); // Replace this with your actual data retrieval logic

				var sheetName = "Sheet1";
				var excelBytes = _excelService.ExportExcel(data, sheetName);
				string Path = "C:\\Users\\Code Toi Sang\\Source\\Repos\\DATN_keni\\DATN_Client\\wwwroot\\excel\\"; 
				System.IO.File.WriteAllBytes(Path, excelBytes);
				return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "exported_data.xlsx");
		
		}

		
		private List<ExcelProduct> GetList()
		{
			var listProductDetail = from pi in _context.ProductItems
									join s in _context.Sizes on pi.SizeId equals s.Id
									join c in _context.Colors on pi.ColorId equals c.Id
									join ct in _context.Categories on pi.CategoryId equals ct.Id
									join p in _context.Products on pi.ProductId equals p.Id
									select new ExcelProduct
									{
										NameProduct = p.Name,
										NameCategory = ct.Name,
										NameColor = c.Name,
										NameSize = s.Name,
										AvaiableQuantity = pi.AvaiableQuantity,
										CostPrice = pi.CostPrice,
										PriceAfterReduction = pi.PriceAfterReduction,
										Status = pi.Status
									};

			List<ExcelProduct> listExcel = listProductDetail.ToList();
			return listExcel;
		}
	}
}
