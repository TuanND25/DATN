using System;
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
        public string CreateDate { get; set; }
        public int TotalAmount { get; set; }
        public int Transport_Fee { get; set; }
        public string ShipCode { get; set; }
        public string Note { get; set; }
        public Guid HistoryConsumerPointID { get; set; }
        public int Status { get; set; }
    }
}
