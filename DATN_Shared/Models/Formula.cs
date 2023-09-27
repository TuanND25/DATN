using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Formula
    {
        public Guid Id { get; set; }
        public int Coefficient { get; set; }
        public int Status { get; set; }
        public virtual ICollection<HistoryConsumerPoint> HistoryConsumerPoints { get; set; }
    }
}
