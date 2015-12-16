namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoiTac")]
    public partial class DoiTac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoiTac()
        {
            HoaDon = new HashSet<HoaDon>();
            NganQuy = new HashSet<NganQuy>();
            NhomHang = new HashSet<NhomHang>();
        }

        [Key]
        [StringLength(10)]
        public string MaDT { get; set; }

        [StringLength(50)]
        public string MaSoThue { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string SDT { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string KieuDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NganQuy> NganQuy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhomHang> NhomHang { get; set; }
    }
}
