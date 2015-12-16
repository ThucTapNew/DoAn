using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiKho.Areas.Common
{
    [Serializable]
    public class HangHoaList
    {

        public HangHoa Product { get; set; }

        public float SoLuong { get; set; }
        public decimal GiaTri { get; set; }
    }
}