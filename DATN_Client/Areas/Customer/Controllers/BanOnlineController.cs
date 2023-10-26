using DATN_Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace DATN_Client.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class BanOnlineController : Controller
	{
		HttpClient _client = new HttpClient();
		List<ProductItem_Show_VM> _lstPrI_show_VM = new List<ProductItem_Show_VM>();
		List<Image_Join_ProductItem> _lstImg_PI = new List<Image_Join_ProductItem>();
		List<Products_VM> _lstP = new List<Products_VM>();
		List<Products_VM> _lstP_Tam1 = new List<Products_VM>();
		Products_VM _P_Show = new Products_VM();
		public static Guid _idP;
		public static IPagedList<Products_VM> _pageList;

		public async Task<IActionResult> ShowProduct(int? page)
		{
			_lstPrI_show_VM = await _client.GetFromJsonAsync<List<ProductItem_Show_VM>>("https://localhost:7141/api/productitem/get_all_productitem_show");
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			// Lấy list sp ko có spct
			foreach (var a in _lstP)
			{
				if (_lstPrI_show_VM.Any(c => c.ProductId == a.Id))
				{
					_lstP_Tam1.Add(a);
				}
			}
			// 1. Tham số int? dùng để thể hiện null và kiểu int
			// page có thể có giá trị là null và kiểu int.

			// 2. Nếu page = null thì đặt lại là 1.
			if (page == null) page = 1;
			// 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
			// theo BookID mới có thể phân trang.
			var pr = _lstP_Tam1.OrderBy(c => c.Name);
			// 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
			int pageSize = 4;

			// 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
			// nếu page = null thì lấy giá trị 1 cho biến pageNumber.
			int pageNumber = (page ?? 1);
			_pageList = pr.ToPagedList(pageNumber, pageSize);
			// 5. Trả về các Link được phân trang theo kích thước và số trang.
			return View(_pageList);
		}
		public async Task<IActionResult> ProductDetail(Guid id)
		{
			_lstP = await _client.GetFromJsonAsync<List<Products_VM>>("https://localhost:7141/api/product/get_allProduct");
			_P_Show = _lstP.Where(c => c.Id == id).FirstOrDefault();
			_idP = _P_Show.Id;
			return View(_P_Show);
		}
	}
}
