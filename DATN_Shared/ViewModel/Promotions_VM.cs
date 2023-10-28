using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class Promotions_VM
    {
		public Guid Id { get; set; }
		[Required(ErrorMessage = "Tên khuyến mã không được để trống")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Phần trăm giảm không được để trống")]
		[Range(0, 100, ErrorMessage = "Phần trăm giảm nằm trong khoảng từ 0-100%")]
		public int Percent { get; set; }
		[Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
		public DateTime StartDate { get; set; }
		[Required(ErrorMessage = "Ngày kết thúc không được để trống")]
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		[Required(ErrorMessage = "Tình trạng không được để trống")]
		public int Status { get; set; }
	}
}
