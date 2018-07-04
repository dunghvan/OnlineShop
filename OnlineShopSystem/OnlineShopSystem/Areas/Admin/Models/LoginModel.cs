using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopSystem.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Ban chua nhap Ten dang nhap")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ban chua nhap Mat khau")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}