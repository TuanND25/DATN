using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class Image_VM
    {
        public Guid Id { get; set; }
        public Guid ReviewId { get; set; }


        public string Name { get; set; }

        public string PathImage { get; set; }
        public Guid ProductItemId { get; set; }
        public int Status { get; set; }

    }
}
