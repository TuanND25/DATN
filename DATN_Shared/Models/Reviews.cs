using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.Models
{
    public class Reviews
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }      
        public int Status { get; set; }


        public User Users { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
