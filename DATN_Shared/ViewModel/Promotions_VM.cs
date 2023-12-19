using DATN_Shared.ViewModel.DiaChi;
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
		[Range(1, 100, ErrorMessage = "Phần trăm giảm nằm trong khoảng từ 1-100%")]
		public int Percent { get; set; }
		[Required(ErrorMessage = "Số lượng không được để trống")]
		[Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
		public int Quantity { get; set; }

		//[CheckStartDate(ErrorMessage = "Ngày bắt đầu phải lớn hơn ngày hiện tại")]
		public DateTime StartDate { get; set; }

		//[CheckEndDate(ErrorMessage = "Ngày kết thúc phải lớn hơn ngày hiện tại")]
		public DateTime EndDate { get; set; }

		[Required(ErrorMessage = "Mô tả không được để trống")]
		public string Description { get; set; }
		[Required(ErrorMessage = "Tình trạng không được để trống")]
		public int Status { get; set; }



		public DateTime CurrentDate => DateTime.Now.Date;
	}
}
