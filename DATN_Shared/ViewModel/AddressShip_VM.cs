using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class AddressShip_VM
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Recipient { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string WardName { get; set; }
        public string ToAddress { get; set; }
        public string NumberPhone { get; set; }
        public int Status { get; set; }
    }
}
