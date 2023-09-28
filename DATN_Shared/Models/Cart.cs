using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Cart
    {
        [Key]
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        public User User { get; set; }
        public virtual ICollection<CartItems> CartItems { get; set; }
      

    }
}
