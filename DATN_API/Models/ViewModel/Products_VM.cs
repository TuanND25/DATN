using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class Products_VM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
