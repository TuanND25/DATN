﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class Bill_VM
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
        public DateTime? CancelDate { get; set; }  // ngày huỷ  
        public int Type { get; set; }

        public string? Note { get; set; }
        public string? Recipient { get; set; } // Người nhận
        public string? District { get; set; } // Quận/Huyện
        public string? Province { get; set; } // Tỉnh/ TP
        public string? WardName { get; set; } // Phường/ Xã
        public string? ToAddress { get; set; } // Địa chỉ chi tiết
        public string? NumberPhone { get; set; } // SDT
        public int Status { get; set; }

        public int? ShippingFee { get; set; }

        public string? CanelBy { get; set; }
        public Guid? CreateBy { get; set; }
        public string? NameCreatBy { get; set; }
        public string? NameCancelBy { get; set; }
    }
}
