using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class User_VM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tên người dùng được để trống")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(\+84|0)\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng đinh dạng")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Giới tính không được để trống")]
        public bool Sex { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public int Status { get; set; }
        public string? Role { get; set; }


    }
}
