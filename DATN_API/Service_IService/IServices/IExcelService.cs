using DATN_API.Models.ViewModel;

namespace DATN_API.Service_IService.IServices
{
    public interface IExcelService
	{


		byte[] ExportExcel(List<ExcelProduct> data, string sheetName);


	}
}
