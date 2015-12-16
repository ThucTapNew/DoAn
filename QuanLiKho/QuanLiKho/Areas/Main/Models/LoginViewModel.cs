using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLiKho.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Chưa nhập UserName")]
        [Display(Name ="UserName")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Chưa nhập Password")]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu")]
        public string Pwd { get; set; }

        [Required]
        public string RememberMe { get; set; }
    }
}