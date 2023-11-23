using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
	public class VoucherUser_VM
	{
		public Guid Id { get; set; }
		public Guid? VoucherId { get; set; }
		public Guid UserId { get; set; }
		public int Status { get; set; }
	}
}
