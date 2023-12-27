using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models
{
	public class ConsumerPoint
	{
		[Key]
		public Guid UserID { get; set; }
		public string Point { get; set; }
		public int Status { get; set; }

		public User Users { get; set; }
		public virtual ICollection<HistoryConsumerPoint> HistoryConsumerPoints { get; set; }
	}
}
