namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BC_CuoiNgay
    {
        [Key]
        [StringLength(10)]
        public string MaBC { get; set; }

        public DateTime NgayTao { get; set; }

        public decimal? TongThu { get; set; }

        public decimal? TongChi { get; set; }
    }
}
