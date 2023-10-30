using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class BillItems
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public Guid ProductItemsId { get; set; }
        public int Quantity { get; set; }
        public int? Price { get; set; }
        public int Status { get; set; }


        public Bill Bills { get; set; }
        public ProductItems ProductItems { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
