namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnh")]
    public partial class HinhAnh
    {
        [Key]
        [StringLength(10)]
        public string MaIMG { get; set; }

        public string TenIMG { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHH { get; set; }

        public string PathFile { get; set; }

        public bool TinhChat { get; set; }

        public virtual HangHoa HangHoa { get; set; }
    }
}
