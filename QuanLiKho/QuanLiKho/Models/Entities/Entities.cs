namespace QuanLiKho.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<BC_CuoiNgay> BC_CuoiNgay { get; set; }
        public virtual DbSet<CongNo> CongNo { get; set; }
        public virtual DbSet<Credential> Credential { get; set; }
        public virtual DbSet<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual DbSet<CuaHang> CuaHang { get; set; }
        public virtual DbSet<ChuCuaHang> ChuCuaHang { get; set; }
        public virtual DbSet<DoiTac> DoiTac { get; set; }
        public virtual DbSet<HangHoa> HangHoa { get; set; }
        public virtual DbSet<HinhAnh> HinhAnh { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhoHang> KhoHang { get; set; }
        public virtual DbSet<LoaiHang> LoaiHang { get; set; }
        public virtual DbSet<MOD> MOD { get; set; }
        public virtual DbSet<NganQuy> NganQuy { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<NhomHang> NhomHang { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyen { get; set; }
        public virtual DbSet<PhieuThuChi> PhieuThuChi { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BC_CuoiNgay>()
                .Property(e => e.TongThu)
                .HasPrecision(15, 3);

            modelBuilder.Entity<BC_CuoiNgay>()
                .Property(e => e.TongChi)
                .HasPrecision(15, 3);

            modelBuilder.Entity<CT_HoaDon>()
                .Property(e => e.LoiNhuan)
                .HasPrecision(15, 3);

            modelBuilder.Entity<CuaHang>()
                .HasMany(e => e.ChuCuaHang)
                .WithRequired(e => e.CuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChuCuaHang>()
                .HasMany(e => e.NhanVien)
                .WithRequired(e => e.ChuCuaHang)
                .HasForeignKey(e => e.TenTK_Chu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DoiTac>()
                .HasMany(e => e.NganQuy)
                .WithRequired(e => e.DoiTac)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DoiTac>()
                .HasMany(e => e.NhomHang)
                .WithMany(e => e.DoiTac)
                .Map(m => m.ToTable("CT_NhomHang").MapLeftKey("MaDT").MapRightKey("MaNH"));

            modelBuilder.Entity<HangHoa>()
                .HasMany(e => e.CT_HoaDon)
                .WithRequired(e => e.HangHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangHoa>()
                .HasMany(e => e.HinhAnh)
                .WithRequired(e => e.HangHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CongNo)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CT_HoaDon)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.PhieuThuChi)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhoHang>()
                .HasMany(e => e.CuaHang)
                .WithRequired(e => e.KhoHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiHang>()
                .HasMany(e => e.HangHoa)
                .WithRequired(e => e.LoaiHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiHang>()
                .HasMany(e => e.KhoHang)
                .WithRequired(e => e.LoaiHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiHang>()
                .HasMany(e => e.NhomHang)
                .WithRequired(e => e.LoaiHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NganQuy>()
                .Property(e => e.SoTien)
                .HasPrecision(15, 3);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDon)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.TenTK_NV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhomHang>()
                .HasMany(e => e.HangHoa)
                .WithRequired(e => e.NhomHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.ChuCuaHang)
                .WithRequired(e => e.PhanQuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.MOD)
                .WithRequired(e => e.PhanQuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.NhanVien)
                .WithRequired(e => e.PhanQuyen)
                .WillCascadeOnDelete(false);
        }
    }
}
