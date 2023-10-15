using DATN_Shared.Models;
using DATN_Shared.ViewModel;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class DetailBill
    {
        HttpClient _client = new HttpClient();
        Bill_ShowModel _billModel = new Bill_ShowModel();
		List<BillDetailShow> _lstBillDetail = new List<BillDetailShow>();
		List<Image_VM> _lstImg = new List<Image_VM>();
        List<BillItems> _lstBillItem = new List<BillItems>();
        ProductItems _productItem = new ProductItems();
		List<ProductItems> _lstproItem = new List<ProductItems>();
		List<ProductItem_Show_VM> _lstProductItem = new List<ProductItem_Show_VM>();
        protected override async Task OnInitializedAsync()
        {
            _billModel = BillManagement._billModel;			
			_lstBillDetail = await _client.GetFromJsonAsync<List<BillDetailShow>> ("https://localhost:7141/api/BillItem/getbilldetail/" + _billModel.Id);
            
		}
    }
}
