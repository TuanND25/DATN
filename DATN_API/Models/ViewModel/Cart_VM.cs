using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_API.Models.ViewModel
{
    public class Cart_VM
    {
        public Guid UserId { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
