using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class ExcelProduct
    {
        public string NameProduct { get; set; }
        public string NameCategory { get; set; }
        public string NameColor { get; set; }
        public string NameSize { get; set; }
        public int? AvaiableQuantity { get; set; }
        public int? PriceAfterReduction { get; set; }

        public int? CostPrice { get; set; }
        public int Status { get; set; }
    }
}
