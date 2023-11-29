using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class SignUpUser
    {
        [Required(ErrorMessage ="Tên đăng nhập không được để trống ")]
        [MaxLength(30, ErrorMessage = "Tên đăng nhập không được vượt quá 30 ký tự")]
                
        public string UserName { get; set; } = string.Empty;
        [MaxLength(30, ErrorMessage = "Mật khẩu không được vượt quá 30 ký tự")]

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		public string Password { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage ="Không đúng định dạng email")]
		[Required(ErrorMessage = "Email không được để trống")]
		public string Email { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự")]
		[Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]	
   
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(30, ErrorMessage = "Tên người dùng không được vượt quá 30 ký tự")]
		[Required(ErrorMessage = "Tên người dùng không được để trống")]
		public string Name { get; set; }  = string.Empty;
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
		public string ConfirmPassword { get; set; } = string.Empty;
       
		[Range(0, double.MaxValue, ErrorMessage = "OTP không đúng định dạng ")]
		public string OTP { get; set; } = string.Empty;
		public bool Sex { get; set; } 
    }
}
