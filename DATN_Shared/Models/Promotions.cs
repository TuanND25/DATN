using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Promotions
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public ICollection<PromotionsItem>  PromotionsProducts { get; set; }
        
    }
}
