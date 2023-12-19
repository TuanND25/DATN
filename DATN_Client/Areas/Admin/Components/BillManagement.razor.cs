using DATN_Client.Areas.Customer.Component;
using DATN_Shared.ViewModel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static DATN_Client.Areas.Admin.Components.BillManagement;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class BillManagement
    {
        HttpClient _client = new HttpClient();
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        List<Bill_ShowModel> _lstbill = new List<Bill_ShowModel>();
        List<BillShowOnMain> _lstBillShowOnMain = new List<BillShowOnMain>();
        List<PaymentMethod_VM> _lstPaymentMethod = new List<PaymentMethod_VM>();

        List<TabType> tabTypes = new List<TabType>();
        public int activeTabType { get; set; } = 1;
        public string tabName { get; set; } = "";
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now).AddDays(-1);
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        public string SearchType { get; set; } = string.Empty;
        public string SearchPaymentMethod { get; set; } = string.Empty;
        public string SearchBillCode { get; set; } = string.Empty;
        public string SearchPhoneNumber { get; set; }
        public string SearchNameUser { get; set; } = string.Empty;
        public bool activeTabDateSearch { get; set; } = false;



        protected override async Task OnInitializedAsync()
        {
            tabTypes.Add(new TabType { Id = 1, Name = "Tất cả hóa đơn" });
            tabTypes.Add(new TabType { Id = 2, Name = "Chờ thanh toán" });
            tabTypes.Add(new TabType { Id = 3, Name = "Chờ xác nhận" });
            tabTypes.Add(new TabType { Id = 4, Name = "Chờ giao hàng" });
            tabTypes.Add(new TabType { Id = 5, Name = "Đang giao hàng" });
            tabTypes.Add(new TabType { Id = 6, Name = "Giao hàng thành công" });
            tabTypes.Add(new TabType { Id = 7, Name = "Hoàn thành" });
            tabTypes.Add(new TabType { Id = 8, Name = "Đã hủy" });



            _lstPaymentMethod = await _client.GetFromJsonAsync<List<PaymentMethod_VM>>(" https://localhost:7141/api/paymentMethod/get_all_paymentMethod");




            var uri = new Uri(_navigationManager.Uri);
            var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);

            SearchType = queryParams["type"];
            SearchBillCode = queryParams["billcode"];
            SearchPhoneNumber = queryParams["phone"];
            SearchNameUser = queryParams["nameuser"];
            SearchPaymentMethod = queryParams["pm"];
            int x = 1;
            int.TryParse(queryParams["TabType"], out x);
            if (x==0)
            {
                x = 1;
            }
            await HandleActiveTabType(x);


            if (DateOnly.TryParse(queryParams["startdate"], out var parsedDate))
            {
                StartDate = parsedDate;
                if (StartDate != null)
                {
                    activeTabDateSearch = true;
                }
            }
            if (DateOnly.TryParse(queryParams["enddate"], out var parsedDate1))
            {
                EndDate = parsedDate1;
            }
            await LocDuLieu();
        }
        public async Task handleSearch()
        {
            var currentUrl = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

            // Thêm thông tin vào URL parameters
            var uriBuilder = new UriBuilder(currentUrl);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            if (!string.IsNullOrEmpty(SearchType))
            {
                query["type"] = SearchType.ToString();
            }
            else
            {
                query["type"] = null;
            }

            if (!string.IsNullOrEmpty(SearchBillCode))
            {
                query["billcode"] = SearchBillCode.ToString();
            }
            else
            {
                query["billcode"] = null;
            }

            if (!string.IsNullOrEmpty(SearchPhoneNumber))
            {
                query["phone"] = SearchPhoneNumber.ToString();
            }
            else
            {
                query["phone"] = null;
            }

            if (!string.IsNullOrEmpty(SearchNameUser))
            {
                query["nameuser"] = SearchNameUser;
            }
            else
            {
                query["nameuser"] = null;
            }

            if (!string.IsNullOrEmpty(SearchPaymentMethod))
            {
                query["pm"] = SearchPaymentMethod;
            }
            else
            {
                query["pm"] = null;
            }

            if (activeTabDateSearch == true)
            {
                query["startdate"] = StartDate.ToString();
                query["enddate"] = EndDate.ToString();
            }
            else
            {
                query["startdate"] = null;
                query["enddate"] = null;
            }

            uriBuilder.Query = query.ToString();


            // Chuyển đến URL mới
            _navigationManager.NavigateTo(uriBuilder.ToString());

            await LocDuLieu();

        }

        public async Task LocDuLieu()
        {
            var _GetclstBill = await _client.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");

            if (activeTabType == 1)
            {

                var listRemove = _GetclstBill.Where(x => x.Type == 2 && x.Status == 5).ToList();
                foreach (var item in listRemove)
                {
                    _GetclstBill.Remove(item);
                }
                //tất cả hóa đơn
                _lstBillShowOnMain = ConvertbillShowOnMain(_GetclstBill);
            }
            else if (activeTabType == 2)
            {
                //Chờ thanh toán
                var list = _GetclstBill.Where(x => x.Type == 1 && x.Status == 1).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 3)
            {
                //Chờ thanh toán
                var list = _GetclstBill.Where(x => x.Type == 1 && x.Status == 2).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 4)
            {
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 3) || (x.Type == 2 && x.Status == 1)).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 5)
            {
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 4) || (x.Type == 2 && x.Status == 2)).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 6)
            {
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 5)
                ).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if(activeTabType == 7)
            {
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 6) ||
               (x.Type == 2 && x.Status == 3)
               ).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (true)
            {
                var list = _GetclstBill.Where(x =>x.Status == 0).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }



            if (activeTabDateSearch == false)
            {

                if (!string.IsNullOrEmpty(SearchNameUser))
                {
                    SearchNameUser = RemoveUnicode(SearchNameUser);
                }

                _lstBillShowOnMain = _lstBillShowOnMain.Where(c =>
                (SearchType == null || SearchType == string.Empty || c.Type == Convert.ToInt32(SearchType)) &&
                (SearchBillCode == null || SearchBillCode == string.Empty || c.BillCode.Contains(SearchBillCode)) &&
                (SearchPhoneNumber == null || SearchPhoneNumber == string.Empty || c.PhoneNumber.Contains(SearchPhoneNumber)) &&
                (SearchNameUser == null || SearchNameUser == string.Empty || RemoveUnicode(c.UserName).ToLower().Contains(SearchNameUser.ToLower())) &&
                (SearchPaymentMethod == null || SearchPaymentMethod == string.Empty || c.PaymentMethodName == SearchPaymentMethod)
                ).ToList();
            }
            else
            {
                if (!string.IsNullOrEmpty(SearchNameUser))
                {
                    SearchNameUser = RemoveUnicode(SearchNameUser);
                }
                _lstBillShowOnMain = _lstBillShowOnMain.Where(c =>
                (SearchType == null || SearchType == string.Empty || c.Type == Convert.ToInt32(SearchType)) &&
                (SearchBillCode == null || SearchBillCode == string.Empty || c.BillCode.Contains(SearchBillCode)) &&
                (SearchPhoneNumber == null || SearchPhoneNumber.ToString() == string.Empty || c.PhoneNumber.Contains(SearchPhoneNumber.ToString())) &&
                (SearchNameUser == null || SearchNameUser == string.Empty || RemoveUnicode(c.UserName).ToLower().Contains(SearchNameUser.ToLower())) &&
                (SearchPaymentMethod == null || SearchPaymentMethod == string.Empty || c.PaymentMethodName == SearchPaymentMethod) &&
                (DateOnly.FromDateTime(c.DateTimeShow.Value) >= StartDate && DateOnly.FromDateTime(c.DateTimeShow.Value) <= EndDate)
                ).ToList();
            }
        }





        public class TabType
        {
            public string Name { get; set; }

            public int Id { get; set; }
        }
        public async Task HandleActiveTabType(int id)
        {
            //tabTypes.Add(new TabType { Id = 1, Name = "Tất cả hóa đơn" });
            //tabTypes.Add(new TabType { Id = 2, Name = "Chờ thanh toán" });
            //tabTypes.Add(new TabType { Id = 3, Name = "Chờ xác nhận" });
            //tabTypes.Add(new TabType { Id = 4, Name = "Chờ giao hàng" });
            //tabTypes.Add(new TabType { Id = 5, Name = "Đang giao hàng" });
            //tabTypes.Add(new TabType { Id = 6, Name = "Giao hàng thành công" });
            //tabTypes.Add(new TabType { Id = 6, Name = "Hoàn thành" });
            //tabTypes.Add(new TabType { Id = 7, Name = "Đã hủy" });

            var currentUrl = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

            // Thêm thông tin vào URL parameters
            var uriBuilder = new UriBuilder(currentUrl);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["TabType"] = id.ToString();
            uriBuilder.Query = query.ToString();

            // Chuyển đến URL mới
            _navigationManager.NavigateTo(uriBuilder.ToString());
            activeTabType = id;

            var _GetclstBill = await _client.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            if (activeTabType == 1)
            {

                var listRemove = _GetclstBill.Where(x => x.Type == 2 && x.Status == 5).ToList();
                foreach (var item in listRemove)
                {
                    _GetclstBill.Remove(item);
                }
                //tất cả hóa đơn
                _lstBillShowOnMain = ConvertbillShowOnMain(_GetclstBill);
            }
            else if (activeTabType == 2)
            {
                //Chờ thanh toán
                var list = _GetclstBill.Where(x => x.Type == 1 && x.Status == 1).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 3)
            {
                //Chờ xác nhận
                var list = _GetclstBill.Where(x => x.Type == 1 && x.Status == 2).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 4)
            {
                //Chờ giao hàng
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 3) || (x.Type == 2 && x.Status == 1)).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 5)
            {
                //Đang giao hàng
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 4) || (x.Type == 2 && x.Status == 2)).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if (activeTabType == 6)
            {
                // Giao hàng thành công
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 5)
                ).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            else if(activeTabType == 7)
            {
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 6) ||
               (x.Type == 2 && x.Status == 3)
               ).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
            await LocDuLieu();

        }



        public List<BillShowOnMain> ConvertbillShowOnMain(List<Bill_ShowModel> _lstBillGet)
        {
            List<BillShowOnMain> billshow = new List<BillShowOnMain>();
            foreach (var item in _lstBillGet)
            {
                var a = new BillShowOnMain
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    UserName = item.UserName,
                    HistoryConsumerPointID = item.HistoryConsumerPointID,
                    ConsumerPoint = item.ConsumerPoint,
                    PaymentMethodId = item.PaymentMethodId,
                    PaymentMethodName = item.PaymentMethodName,
                    Recipient = item.Recipient,
                    District = item.District,
                    Province = item.Province,
                    WardName = item.WardName,
                    ToAddress = item.ToAddress,
                    NumberPhone = item.NumberPhone,
                    Reduced_Value = item.Reduced_Value,
                    BillCode = item.BillCode,
                    TotalAmount = item.TotalAmount,
                    ReducedAmount = item.ReducedAmount,
                    Cash = item.Cash,
                    CustomerPayment = item.CustomerPayment,
                    FinalAmount = item.FinalAmount,
                    CreateDate = item.CreateDate,
                    CompletionDate = item.CompletionDate,
                    ConfirmationDate = item.ConfirmationDate,
                    DateTimeShow = item.CompletionDate != null ? item.CompletionDate : (item.ConfirmationDate != null ? item.ConfirmationDate : (item.CreateDate != null ? item.CreateDate : default(DateTime))),
                    Type = item.Type,
                    Note = item.Note,
                    Status = item.Status,
                    PhoneNumber = item.PhoneNumber,
                };
                billshow.Add(a);
            }
            return billshow.OrderByDescending(x => x.DateTimeShow).ThenBy(x => x.DateTimeShow).ToList();
            ;
        }
        public void redirectToDetails(Guid BillId)
        {
            _navigationManager.NavigateTo($"/bill-management/bill-detail?billid={BillId}", true);

        }

        public class BillShowOnMain
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public Guid? HistoryConsumerPointID { get; set; }
            public int ConsumerPoint { get; set; }
            public Guid? PaymentMethodId { get; set; }
            public string PaymentMethodName { get; set; }
            public string? Recipient { get; set; }
            public string? District { get; set; }
            public string? Province { get; set; }
            public string? WardName { get; set; }
            public string? ToAddress { get; set; }
            public string? NumberPhone { get; set; }
            public int? Reduced_Value { get; set; }
            public string BillCode { get; set; }
            public int? TotalAmount { get; set; }
            public int? ReducedAmount { get; set; }
            public int? Cash { get; set; }  // tiền mặt
            public int? CustomerPayment { get; set; } // tiền khách đưa
            public int? FinalAmount { get; set; } // tiền khách đưa
            public DateTime? CreateDate { get; set; }
            public DateTime? ConfirmationDate { get; set; }
            public DateTime? CompletionDate { get; set; }
            public DateTime? DateTimeShow { get; set; }
            public int? Type { get; set; }
            public string? Note { get; set; }
            public int Status { get; set; }
            public string PhoneNumber { get; set; }
        }

        public static string RemoveUnicode(string text)
        {

            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;


        }
    }
}
