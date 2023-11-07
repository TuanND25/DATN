using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class AddressShip_VM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required(ErrorMessage ="Tên người nhận không được để trống")]
        public string Recipient { get; set; }
        [Required(ErrorMessage = "Quận/Huyện không được để trống")]
        public string District { get; set; }
        [Required(ErrorMessage = "Tỉnh/TP không được để trống")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Xã/Phường không được để trống")]
        public string WardName { get; set; }
        [Required(ErrorMessage = "Địa chỉ cụ thể không được để trống")]
        public string ToAddress { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(\+84|0)\d{9,10}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string NumberPhone { get; set; }
        [Required(ErrorMessage = "Tình trạng không được để trống")]
        public int Status { get; set; }
    }
}
