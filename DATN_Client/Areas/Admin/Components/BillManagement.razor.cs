using DATN_Shared.ViewModel;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static DATN_Client.Areas.Admin.Components.BillManagement;

namespace DATN_Client.Areas.Admin.Components
{
    public partial class BillManagement
    {
        HttpClient _client = new HttpClient();
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        List<Bill_ShowModel> _lstbill = new List<Bill_ShowModel>();
        List<BillShowOnMain> _lstBillShowOnMain = new List<BillShowOnMain>();
       
        List<TabType> tabTypes = new List<TabType>();
        public int activeTabType { get; set; } = 1;
        public string tabName { get; set; } = "";

        public int SearchType { get; set; } = 1;

        protected override async Task OnInitializedAsync()
        {
            tabTypes.Add(new TabType { Id = 1, Name = "Tất cả hóa đơn" });
            tabTypes.Add(new TabType { Id = 2, Name = "Chờ thanh toán" });
            tabTypes.Add(new TabType { Id = 3, Name = "Chờ xác nhận" });
            tabTypes.Add(new TabType { Id = 4, Name = "Chờ giao hàng" });
            tabTypes.Add(new TabType { Id = 5, Name = "Đang giao hàng" });
            tabTypes.Add(new TabType { Id = 6, Name = "Đã hoàn thành" });
            await HandleActiveTabType(1,"a");
        }
        public class TabType
        {
            public string Name { get; set; }

            public int Id { get; set; }
        }
        public async Task HandleActiveTabType(int id,string a)
        {
           
            var _GetclstBill = await _client.GetFromJsonAsync<List<Bill_ShowModel>>("https://localhost:7141/api/Bill/get_alll_bill");
            activeTabType = id;
            tabName = a;
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
            else if(activeTabType == 2)
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
            else if (activeTabType ==4)
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
                var list = _GetclstBill.Where(x => (x.Type == 1 && x.Status == 5) ||
                (x.Type == 2 && x.Status == 3)
                ).ToList();
                _lstBillShowOnMain = ConvertbillShowOnMain(list);
            }
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
                    Cash =  item.Cash,
                    CustomerPayment = item.CustomerPayment,
                    FinalAmount = item.FinalAmount,
                    CreateDate = item.CreateDate,
                    CompletionDate = item.CompletionDate,
                    ConfirmationDate =  item.ConfirmationDate,
                    DateTimeShow = item.CompletionDate != null ? item.CompletionDate : (item.ConfirmationDate !=null ? item.ConfirmationDate : (item.CreateDate != null ? item.CreateDate :  default )),
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
            _navigationManager.NavigateTo($"/bill-management/bill-detail?billid={BillId}",true);
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
    }
}
