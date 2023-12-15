using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class HistoryConsumerPoint
    {
        public Guid Id { get; set; }
        public Guid ConsumerPointId { get; set; }
        public Guid? FormulaId { get; set; }
        public Guid BillId { get; set; }
        public int Point { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public ConsumerPoint ConsumerPoints { get; set; }
        public Formula Formulas { get; set; }
        public Bill Bill { get; set; }

    }
}
