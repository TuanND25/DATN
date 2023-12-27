using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel.DiaChi
{
    public class Ward_VM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
    }
}
