using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public virtual ICollection<ProductItems> ProductItems { get; set; }

    }
}
