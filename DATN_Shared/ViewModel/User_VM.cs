using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN_Shared.Models;

namespace DATN_Shared.ViewModel
{
    public class User_VM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Sex { get; set; }

        public int Status { get; set; }

  
    }
}
