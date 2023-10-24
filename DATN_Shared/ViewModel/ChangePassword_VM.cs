using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
	public class ChangePassword_VM 
	{
		public Guid UserId { get; set; }
		public string NewPassword { get; set; }

		public string OldPassword { get; set; }
	}
}
