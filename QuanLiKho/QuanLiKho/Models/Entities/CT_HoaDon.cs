namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_HoaDon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaHH { get; set; }

        public double SoLuong { get; set; }

        public double DonGia { get; set; }

        public decimal? LoiNhuan { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
