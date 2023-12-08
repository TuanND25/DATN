using ClosedXML.Excel;
using DATN_API.Data;
using DATN_API.Service_IService.IServices;
using DATN_Shared.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Service_IService.Services
{
	public class ExcelService : IExcelService
	{
		private readonly ApplicationDbContext _context;

		public ExcelService(ApplicationDbContext context)
		{
			_context = context;
		}

		public byte[] ExportExcel(List<ExcelProduct> data, string sheetName)
		{
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add(sheetName);

				var properties = typeof(ExcelProduct).GetProperties();
				for (int i = 0; i < properties.Length; i++)
				{
					worksheet.Cell(1, i + 1).Value = properties[i].Name;
				}

				for (int i = 0; i < data.Count; i++)
				{
					for (int j = 0; j < properties.Length; j++)
					{
						var value = properties[j].GetValue(data[i]);
						worksheet.Cell(i + 2, j + 1).Value = data[i].NameProduct;
					}
				}

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					return stream.ToArray();
				}
			}
		}



		
	}
}
