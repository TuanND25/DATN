using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class Bill_DataAnotation_VM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? HistoryConsumerPointID { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? VoucherId { get; set; }
        public string BillCode { get; set; }
        public int? TotalAmount { get; set; }
        public int? ReducedAmount { get; set; }
        public int? Cash { get; set; }  // tiền mặt
        public int? CustomerPayment { get; set; } // tiền khách đưa
        public int? FinalAmount { get; set; } // tiền khách đưa
        public DateTime? CreateDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int Type { get; set; }
        public string? CanelBy { get; set; }  // người huỷ 
        public Guid? CreateBy { get; set; }  // người huỷ 
        public string? Note { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string? Recipient { get; set; } // Người nhận
        [Required(ErrorMessage = "Vui lòng chọn Quận/Huyện/Thị xã!")]
        public string? District { get; set; } // Quận/Huyện
        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành phố!")]
        public string? Province { get; set; } // Tỉnh/ TP
        [Required(ErrorMessage = "Vui lòng chọn Xã/Phường/Thị trấn!")]
        public string? WardName { get; set; } // Phường/ Xã
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string? ToAddress { get; set; } // Địa chỉ chi tiết
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Thông tin không hợp lệ.")]
        public string? NumberPhone { get; set; } // SDT
        public int Status { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số phải lớn hơn hoặc bằng 0.")]
        public int? ShippingFee { get; set; }
    }
}
