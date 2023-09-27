using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class PromotionsProduct
    {
        public Guid Id { get; set; }
        public Guid PromotionsId { get; set; }
        public Guid ProductItemsId { get; set; }
        public int Status { get; set; }

        public Promotions Promotions { get; set; }
        public ProductItems ProductItems { get; set; }
    }
}
