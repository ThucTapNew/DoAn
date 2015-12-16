using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLiKho.Models.Entities;

namespace QuanLiKho.Areas.Admin.Models
{
    public class CartItem
    {
        public HangHoa Product { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
    }
}