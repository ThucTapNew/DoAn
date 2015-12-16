namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NganQuy")]
    public partial class NganQuy
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaNQ { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaDT { get; set; }

        public DateTime ThoiGian { get; set; }

        public decimal SoTien { get; set; }

        public virtual DoiTac DoiTac { get; set; }
    }
}
