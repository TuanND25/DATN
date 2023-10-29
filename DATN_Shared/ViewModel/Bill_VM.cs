using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DATN_Shared.ViewModel
{
    public class Bill_VM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? HistoryConsumerPointID { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid? VoucherId { get; set; }
        public string BillCode { get; set; }
        public int TotalAmount { get; set; }
        public int ReducedAmount { get; set; }
        public int? Cash { get; set; }  // tiền mặt
        public int? CustomerPayment { get; set; } // tiền khách đưa
        public int? FinalAmount { get; set; } // tiền khách đưa
        public DateTime? CreateDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Type { get; set; }
        [Required(ErrorMessage = "Trường này không được để trống.")]
        public string? Note { get; set; }
        public string Recipient { get; set; } // Người nhận
        public string District { get; set; } // Quận/Huyện
        public string Province { get; set; } // Tỉnh/ TP
        public string WardName { get; set; } // Phường/ Xã
        public string ToAddress { get; set; } // Địa chỉ chi tiết
        public string NumberPhone { get; set; } // SDT
        public int Status { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số phải lớn hơn 0.")]
        public int? ShippingFee { get; set; }
    }
}
