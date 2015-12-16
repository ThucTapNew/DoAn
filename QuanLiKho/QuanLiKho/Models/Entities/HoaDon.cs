namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            CongNo = new HashSet<CongNo>();
            CT_HoaDon = new HashSet<CT_HoaDon>();
            PhieuThuChi = new HashSet<PhieuThuChi>();
        }

        [Key]
        [StringLength(10)]
        public string MaHD { get; set; }

        public DateTime NgayTao { get; set; }

        public double TongTien { get; set; }

        public bool LoaiHD { get; set; }

        [StringLength(10)]
        public string MaDT { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTK_NV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CongNo> CongNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }

        public virtual DoiTac DoiTac { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuThuChi> PhieuThuChi { get; set; }
    }
}
