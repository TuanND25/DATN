using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class Voucher_VM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên voucher không được để trống ")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mã voucher không được để trống ")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phần trăm giảm giá không được để trống ")]
        [Range(1, 100, ErrorMessage = "Phần giảm phải từ 1 đến 100%")]
        public int Percent { get; set; }  // giá trị giảm ( giảm 10% )
        [Required(ErrorMessage = "Số lượng  không được để trống ")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng lớn hơn 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống ")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống ")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Điều kiện giảm đơn tối thiểu không được để trống ")]
        [Range(1, int.MaxValue, ErrorMessage = " Điều kiến tối thiểu phải lớn hơn 0")]
        public int? Discount_Conditions { get; set; }  // điều kiện giảm  đơn tối thiểu 100k
        [Required(ErrorMessage = "Giá trị giảm tối đa không được để trống ")]
        [Range(1, int.MaxValue, ErrorMessage = " Giá trị giảm tối đa phải lớn hơn 0")]
        public int Maximum_Reduction { get; set; } // giảm tối đa  30k
        public int Status { get; set; }
    }
}
