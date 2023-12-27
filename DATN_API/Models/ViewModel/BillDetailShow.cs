using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class BillDetailShow
    {
        public Guid Id { get; set; } // billitem
        public Guid BillID { get; set; }
        public Guid ProductItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public Guid? ColorId { get; set; }
        public string ColorName { get; set; }
        public Guid? SizeId { get; set; }
        public string SizeName { get; set; }
        public Guid CategoryID { get; set; }
        public int? PriceAfter { get; set; }
        public string CategoryName { get; set; }
        public string PaymentMethod { get; set; }
        public int Quantity { get; set; }
        public int? CostPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }

    }
}
