using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
	public class HistoryConsumerPoint_VM
	{
        public Guid Id { get; set; }
        public Guid ConsumerPointId { get; set; }
        public Guid FormulaId { get; set; }
        public int Status { get; set; }
    }
}
