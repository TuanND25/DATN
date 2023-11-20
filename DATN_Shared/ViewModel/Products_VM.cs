using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class Products_VM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public int Status { get; set; }
    }
}
