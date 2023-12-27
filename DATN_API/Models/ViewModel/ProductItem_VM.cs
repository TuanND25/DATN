using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class ProductItem_VM
    {
        public Guid Id { get; set; }
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Vui lòng chọn sản phẩm!")]
        public Guid ProductId { get; set; }
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Vui lòng chọn màu sắc!")]
        public Guid? ColorId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Vui lòng chọn size!")]
        public Guid? SizeId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");
        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Vui lòng chọn thể loại!")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Số lượng tồn không được để trống")]
        public int? AvaiableQuantity { get; set; }
        public int? PriceAfterReduction { get; set; }
        [Required(ErrorMessage = "Giá bán không được để trống")]
        public int? CostPrice { get; set; }
        public int Status { get; set; }
    }
}
