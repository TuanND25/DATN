using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class PromotionItem_VM
    {
        public Guid Id { get; set; }
        public Guid PromotionsId { get; set; }
        public Guid ProductItemsId { get; set; }
        public int Status { get; set; }
        public Guid ProductId { get; set; }
        public int Percent { get; set; }
        public int Quantity { get; set; }
    }
}
