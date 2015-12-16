using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLiKho.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLiKho.Areas.Admin.Models
{
    public class TongNo
    {
        public DoiTac DoiTac { get; set; }
        public double GiaTri { get; set; }
    }
}