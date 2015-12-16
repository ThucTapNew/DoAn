namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhomHang")]
    public partial class NhomHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhomHang()
        {
            HangHoa = new HashSet<HangHoa>();
            DoiTac = new HashSet<DoiTac>();
        }

        [Key]
        [StringLength(10)]
        public string MaNH { get; set; }

        [Required]
        public string TenNH { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HangHoa> HangHoa { get; set; }

        public virtual LoaiHang LoaiHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoiTac> DoiTac { get; set; }
    }
}
