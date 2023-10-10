using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DATN_Shared.Models 
{ 

    public class User :IdentityUser<Guid>
    {
   
        public string Name { get; set; } = string.Empty;
        public bool Sex { get; set; } 
        public string RefreshToken { get; set; } =string.Empty;
        public DateTime TokenCreated { get; set; } 
        public DateTime TokenExpires { get; set; }
        public int Status { get; set; } 
        public ConsumerPoint ConsumerPoint { get; set; }
        public Cart Cart { get; set; }  
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }    
        public virtual ICollection<AddressShip> AddressShips { get; set; }
        public virtual ICollection<VoucherUser> VoucherUsers { get; set; }
      

    }
}
