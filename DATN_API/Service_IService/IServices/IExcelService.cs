using DATN_Shared.ViewModel;

namespace DATN_API.Service_IService.IServices
{
	public interface IExcelService
	{


		byte[] ExportExcel(List<ExcelProduct> data, string sheetName);


	}
}
