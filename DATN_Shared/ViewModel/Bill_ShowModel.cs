using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Bill_ShowModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid? HistoryConsumerPointID { get; set; }
        public int ConsumerPoint { get; set; }
        public Guid PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public Guid? VoucherId { get; set; }
        public Guid? AddressId { get; set; }
        public string? Recipient { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? WardName { get; set; }
        public string? ToAddress { get; set; }
        public string? NumberPhone { get; set; }
        public int? Reduced_Value { get; set; }
        public string BillCode { get; set; }
        public int TotalAmount { get; set; }
        public int ReducedAmount { get; set; }
        public int? Cash { get; set; }  // tiền mặt
        public int? CustomerPayment { get; set; } // tiền khách đưa
        public int FinalAmount { get; set; } // tiền khách đưa
        public DateTime? CreateDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? Type { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }
        public string PhoneNumber { get; set; }


    }
}
