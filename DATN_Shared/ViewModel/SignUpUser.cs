using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class SignUpUser
    {
     
        public string UserName { get; set; }    
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        
        public string Name { get; set; }
        public string ConfirmPassword { get; set; }
       

    }
}
