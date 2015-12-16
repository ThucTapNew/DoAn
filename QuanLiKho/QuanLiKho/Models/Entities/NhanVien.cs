namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        [Key]
        [StringLength(50)]
        public string TenTK { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string ChucVu { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string MaPQ { get; set; }

        [Required]
        [StringLength(50)]
        public string Pwd { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTK_Chu { get; set; }

        public virtual ChuCuaHang ChuCuaHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDon { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
