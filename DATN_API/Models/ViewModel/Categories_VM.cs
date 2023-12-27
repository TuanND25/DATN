using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class Categories_VM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        public string Name { get; set; }
        public string TenKhongDau { get; set; }
        public int Status { get; set; }
    }
}
