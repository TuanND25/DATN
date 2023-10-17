﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class Voucher_VM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Percent { get; set; }  // giá trị giảm ( giảm 10% )
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Discount_Conditions { get; set; }  // điều kiện giảm  đơn tối thiểu 100k
        public int Maximum_Reduction { get; set; } // giảm tối đa  30k
        public int Status { get; set; }
    }
}
