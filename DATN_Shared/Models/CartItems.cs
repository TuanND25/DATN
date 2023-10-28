using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class CartItems
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductItemId { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }

        public ProductItems ProductItems { get; set; }
        public Cart Cart { get; set; }
     
    }
}
