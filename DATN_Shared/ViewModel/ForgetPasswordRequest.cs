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
        [Required(ErrorMessage ="Email không được để trống")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "phonenumber không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Username không được để trống")]
        public string Username { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "OTP không đúng định dạng ")]
        public string OTP { get; set; } = string.Empty;

    }
    
}
