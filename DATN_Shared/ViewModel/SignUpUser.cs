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
        [Required(ErrorMessage ="Username not null")]
        [MaxLength(30)]     
                
        public string? UserName { get; set; } 
        [MaxLength(30)]

		[Required(ErrorMessage = "Password not null")]
		public string? Password { get; set; }
        [EmailAddress(ErrorMessage ="khong dung din dang email")]
		[Required(ErrorMessage = "Email not null")]
		public string? Email { get; set; }
        [MaxLength(20)]
		[Required(ErrorMessage = "PhoneNumber khong duoc de trong")]
		[Range(0, double.MaxValue,ErrorMessage = "PhoneNumber khong hop le")]
		public string? PhoneNumber { get; set; }
		[MaxLength(20)]
		[Required(ErrorMessage = "Name khong duoc de trong")]
		public string? Name { get; set; }
		[Required(ErrorMessage = "ConfirmPassword khong duoc de trong")]
		public string? ConfirmPassword { get; set; }
		[Required(ErrorMessage = "sex  not null")]
		
		public bool Sex { get; set; } 
    }
}
