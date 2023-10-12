﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int FinalAmount { get; set; } // tiền khách đưa
        public DateTime? CreateDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Type { get; set; }
        public string? Note { get; set; }
        public int Status { get; set; }
    }
}
