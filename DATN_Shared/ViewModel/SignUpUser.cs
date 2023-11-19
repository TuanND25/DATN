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
        [Required(ErrorMessage ="Username không được để trống ")]
        [MaxLength(30)]     
                
        public string? UserName { get; set; }=string.Empty;
        [MaxLength(30)]

		[Required(ErrorMessage = "Password không được để trống")]
		public string? Password { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage ="Không đúng định dạng email")]
		[Required(ErrorMessage = "Email không được để trống")]
		public string? Email { get; set; }
        [MaxLength(20)]
		[Required(ErrorMessage = "PhoneNumber không được để trống")]
		[Range(0, double.MaxValue,ErrorMessage = "PhoneNumber không hợp lệ")]
		public string? PhoneNumber { get; set; } = string.Empty;
        [MaxLength(20)]
		[Required(ErrorMessage = "Name không được để trống")]
		public string? Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "ConfirmPassword không được để trống")]
		public string? ConfirmPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "giới thích không được để trống")]
		
		public bool Sex { get; set; } 
    }
}
