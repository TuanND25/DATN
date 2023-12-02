using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class ForgetPasswordRequest
    {
		[EmailAddress(ErrorMessage = "Không đúng định dạng email")]
		[Required(ErrorMessage = "Email không được để trống")]
		public string Email { get; set; } = string.Empty;

		[MaxLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
		[Required(ErrorMessage = "Số điện thoại không được để trống")]
		[RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required(ErrorMessage = "Tên đăng nhập không được để trống ")]
		[MaxLength(30, ErrorMessage = "Tên đăng nhập không được vượt quá 30 ký tự")]
		public string Username { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "OTP không đúng định dạng ")]
        public string OTP { get; set; } = string.Empty;

    }
    
}
