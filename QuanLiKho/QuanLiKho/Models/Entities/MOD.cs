namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MOD")]
    public partial class MOD
    {
        [Key]
        [StringLength(50)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(50)]
        public string Pwd { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [StringLength(10)]
        public string MaPQ { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        public virtual PhanQuyen PhanQuyen { get; set; }
    }
}
