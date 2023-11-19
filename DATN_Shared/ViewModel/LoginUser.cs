using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN_Shared.ViewModel
{
    public class LoginUser
    {
        [Required(ErrorMessage =" Username không được để trống")]
        public string UserName { get; set;}
        [Required(ErrorMessage = " Password không được để trống")]
        public string Password { get; set;}
    }
}
