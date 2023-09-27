using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CreateDate { get; set; }
        public int TotalAmount { get; set; }
        public int Transport_Fee { get; set; }
        public string ShipCode { get; set; }
        public string Note { get; set; }
        public Guid HistoryConsumerPointID { get; set; }
        public int Status { get; set; }


        public User Users { get; set; }

        public HistoryConsumerPoint HistoryConsumerPoints { get; set; }
        public virtual ICollection<VoucherBill> Voucher_Bills { get; set; }
        public virtual ICollection<BillItems> BillItems { get; set; }
    }
}
