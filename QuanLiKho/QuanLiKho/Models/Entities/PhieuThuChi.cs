namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuThuChi")]
    public partial class PhieuThuChi
    {
        [Key]
        [StringLength(10)]
        public string MaPH { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHD { get; set; }

        public DateTime ThoiGian { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
