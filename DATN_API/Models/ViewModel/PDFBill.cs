using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class PDFBill
    {
        public Guid Id { get; set; }
        public string NameCustomer { get; set; }
        public string BillCode { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public double? Total { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
