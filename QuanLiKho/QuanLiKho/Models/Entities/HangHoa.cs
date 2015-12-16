namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangHoa")]
    public partial class HangHoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangHoa()
        {
            CT_HoaDon = new HashSet<CT_HoaDon>();
            HinhAnh = new HashSet<HinhAnh>();
        }

        [Key]
        [StringLength(10)]
        public string MaHH { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHH { get; set; }

        public double SoLuong { get; set; }

        [Required]
        [StringLength(50)]
        public string KichThuoc { get; set; }

        public double KhoiLuong { get; set; }

        [Required]
        [StringLength(10)]
        public string DVT { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySX { get; set; }

        [Column(TypeName = "date")]
        public DateTime HanSD { get; set; }

        [Required]
        [StringLength(50)]
        public string LoSX { get; set; }

        public double GiaBan { get; set; }

        public double GiaNhap { get; set; }

        public bool TinhTrang { get; set; }

        public double DinhMuc { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNH { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }

        public virtual LoaiHang LoaiHang { get; set; }

        public virtual NhomHang NhomHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnh> HinhAnh { get; set; }
    }
}
