using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ListClass.Model
{
    public class ListDBContext : DbContext
    {
        public ListDBContext() : base("name=StrConnect") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<TapDoan> TapDoans { get; set; }
        public DbSet<KhuVuc> KhuVucs { get; set; }
        public DbSet<ListNumber> ListNumbers { get; set; } 
        public DbSet<PhanCong> PhanCongs { get; set; }
        public DbSet<GiayCNSanPham> GiayCNSanPhams { get; set; }
        public DbSet<TB_STD> TB_STDs { get; set; }
        public DbSet<STD_DUT> STD_DUTs { get; set; }
        public DbSet<BulbSpectrum> BulbSpectrums { get; set; }
        public DbSet<LampSystem> LampSystems { get; set; }
        public DbSet<QuyTrinhISO> QuyTrinhISOs { get; set; }
        public DbSet<QuyTrinhISO02> QuyTrinhISO02s { get; set; } 
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<PhanCongGhiChu> PhanCongGhiChus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CompanyRoles> CompanyRoles { get; set; }
        public DbSet<BaoCaoSuaChua> BaoCaoSuaChuas { get; set; }
        public DbSet<NgayTaoTechnicialBuldTruocHieuChinh> NgayTaoTechnicialBuldTruocHieuChinhs { get; set; }
        public DbSet<NgayTaoTechnicialBuldKetQuaHieuChinh> NgayTaoTechnicialBuldKetQuaHieuChinhs { get; set; }
        public DbSet<Ngaynghi> Ngaynghis { get; set; }
        public DbSet<ChuanSuDung> ChuanSuDungs { get; set; }
        public DbSet<GiaTriTruocHieuChinh> GiaTriTruocHieuChinhs { get; set; }
        public DbSet<KetQuaHieuChinh> KetQuaHieuChinhs { get; set; }
        public DbSet<BanTinhDKDBD> BanTinhDKDBDs { get; set; }
        public DbSet<KeHoachHieuChuan_Form> KeHoachHieuChuan_Forms { get; set; }
        public DbSet<PhieuChuyenMauHieuChuan> PhieuChuyenMauHieuChuans { get; set; }
        public DbSet<Giaychungnhan> Giaychungnhans { get; set; }
        public DbSet<Phieutrathietbi> Phieutrathietbis { get; set; }
        public DbSet<BienBanHieuChuan> BienBanHieuChuans { get; set; }
        public DbSet<PhieuTheoDoiNhietDo> PhieuTheoDoiNhietDos { get; set; }
        public DbSet<DanhSachPhieuTheoDoiNhietDo> DanhSachPhieuTheoDoiNhietDos { get; set; }
        public DbSet<PhieuNhanThietBi> PhieuNhanThietBis { get; set; } 
        public DbSet<Phieuyeucauhieuchuan> Phieuyeucauhieuchuans { get; set; }
        public DbSet<PhieuyeucauhieuchuanDoi> PhieuyeucauhieuchuanDois { get; set; }
        public DbSet<BaoCaoDanhGia> BaoCaoDanhGias { get; set; }
        public DbSet<KeHoachHieuChuan> KeHoachHieuChuans { get; set; }
        public DbSet<NhatKyBaoTriVaSuChua> NhatKyBaoTriVaSuChuas { get; set; }
        public DbSet<DanhMucThietBi> DanhMucThietBis { get; set; } 
        public DbSet<DuLieu_NhatKyBaoTriVaSuChua> DuLieu_NhatKyBaoTriVaSuChua { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GiayCNSanPham>()
            .HasRequired(b => b.ListNumber)
            .WithMany(p => p.GiayCNSanPhams)
            .HasForeignKey(b => b.Id_ListNumber);

            modelBuilder.Entity<NgayTaoTechnicialBuldTruocHieuChinh>()
           .HasOptional(b => b.LampSystem)
           .WithMany(p => p.NgayTaoTechnicialBuldTruocHieuChinhs)
           .HasForeignKey(b => b.Id_LampSystems);

            modelBuilder.Entity<NgayTaoTechnicialBuldKetQuaHieuChinh>()
            .HasOptional(b => b.LampSystem)
            .WithMany(p => p.NgayTaoTechnicialBuldKetQuaHieuChinhs)
            .HasForeignKey(b => b.Id_LampSystems);

            modelBuilder.Entity<NgayTaoTechnicialBuldTruocHieuChinh>()
           .HasOptional(b => b.BulbSpectrum)
           .WithMany(p => p.NgayTaoTechnicialBuldTruocHieuChinhs)
           .HasForeignKey(b => b.Id_BulbSpectrum);

            modelBuilder.Entity<NgayTaoTechnicialBuldKetQuaHieuChinh>()
          .HasOptional(b => b.BulbSpectrum)
          .WithMany(p => p.NgayTaoTechnicialBuldKetQuaHieuChinhs)
          .HasForeignKey(b => b.Id_BulbSpectrum);

            modelBuilder.Entity<CompanyRoles>()
           .HasOptional(b => b.Role)
           .WithMany(p => p.CompanyRoles)
           .HasForeignKey(b => b.Id_Role);

            modelBuilder.Entity<CompanyRoles>()
          .HasOptional(b => b.Company)
          .WithMany(p => p.CompanyRoles)
          .HasForeignKey(b => b.Id_Company);

            modelBuilder.Entity<Ngaynghi>()
         .HasOptional(b => b.Company)
         .WithMany(p => p.Ngaynghis)
         .HasForeignKey(b => b.Id_Company);

            modelBuilder.Entity<PhanCongGhiChu>()
            .HasRequired(b => b.PhanCong)
            .WithMany(p => p.PhanCongGhiChus)
            .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<QuyTrinhISO02>()
            .HasRequired(b => b.QuyTrinhISO)
            .WithMany(p => p.QuyTrinhISO02s)
            .HasForeignKey(b => b.id_quytrinh);

            modelBuilder.Entity<KhachHang>()
            .HasOptional(b => b.TapDoan)
            .WithMany(p => p.KhachHangs)
            .HasForeignKey(b => b.Id_TapDoan);

            modelBuilder.Entity<KhachHang>()
           .HasOptional(b => b.KhuVuc)
           .WithMany(p => p.KhachHangs)
           .HasForeignKey(b => b.Id_KhuVuc);

            modelBuilder.Entity<NgayTaoTechnicialBuldKetQuaHieuChinh>()
            .HasRequired(b => b.PhanCong)
            .WithMany(p => p.NgayTaoTechnicialBuldKetQuaHieuChinhs)
            .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<NgayTaoTechnicialBuldTruocHieuChinh>()
            .HasRequired(b => b.PhanCong)
            .WithMany(p => p.NgayTaoTechnicialBuldTruocHieuChinhs)
            .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<ChuanSuDung>()
             .HasRequired(b => b.PhanCong)
             .WithMany(p => p.ChuanSuDungs)
             .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<GiaTriTruocHieuChinh>()
             .HasRequired(b => b.PhanCong)
             .WithMany(p => p.GiaTriTruocHieuChinhs)
             .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<KetQuaHieuChinh>()
             .HasRequired(b => b.PhanCong)
             .WithMany(p => p.KetQuaHieuChinhs)
             .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<DanhSachPhieuTheoDoiNhietDo>()
              .HasRequired(b => b.PhieuTheoDoiNhietDos)
              .WithMany(p => p.DanhSachPhieuTheoDoiNhietDos)
              .HasForeignKey(b => b.ID_PhieuTheoDoiNhietDo);

            modelBuilder.Entity<NhatKyBaoTriVaSuChua>()
            .HasRequired(b => b.DanhMucThietBi)
            .WithMany(p => p.NhatKyBaoTriVaSuChuas)
            .HasForeignKey(b => b.Id_DanhMucThietBi);

            modelBuilder.Entity<DuLieu_NhatKyBaoTriVaSuChua>()
            .HasRequired(b => b.NhatKyBaoTriVaSuChua)
            .WithMany(p => p.DuLieu_NhatKyBaoTriVaSuChuas)
            .HasForeignKey(b => b.Id_NhatKyBaoTriVaSuChua);

            modelBuilder.Entity<KeHoachHieuChuan>()
              .HasRequired(b => b.DanhMucThietBi)
              .WithMany(p => p.KeHoachHieuChuans)
              .HasForeignKey(b => b.Id_DanhMucThietBi);

            modelBuilder.Entity<BaoCaoDanhGia>()
              .HasRequired(b => b.DanhMucThietBi)
              .WithMany(p => p.BaoCaoDanhGias)
              .HasForeignKey(b => b.Id_DanhMucThietBi);

            modelBuilder.Entity<KeHoachHieuChuan>()
            .HasRequired(b => b.KeHoachHieuChuan_Form)
            .WithMany(p => p.KeHoachHieuChuans)
            .HasForeignKey(b => b.Id_KeHoachHieuChuan_Form);

            modelBuilder.Entity<PhanCong>()
             .HasOptional(p => p.Company)
             .WithMany(c => c.PhanCongs)
             .HasForeignKey(p => p.Id_NV);

            modelBuilder.Entity<BienBanHieuChuan>()
                .HasRequired(b => b.PhanCong)
                .WithMany(p => p.BienBanHieuChuans)
                .HasForeignKey(b => b.Id_PhanCong);

            modelBuilder.Entity<STD_DUT>()
                .HasRequired(b => b.BienBanHieuChuan)
                .WithMany(p => p.STD_DUTs)
                .HasForeignKey(b => b.Id_BienBanHieuChuan);

            modelBuilder.Entity<PhieuNhanThietBi>()
                 .HasRequired(b => b.ListNumber)
                 .WithMany(p => p.PhieuNhanThietBis)
                 .HasForeignKey(b => b.Id_ListNumber);

            modelBuilder.Entity<Phieuyeucauhieuchuan>()
                .HasRequired(b => b.PhieuNhanThietBi)
                .WithMany(p => p.Phieuyeucauhieuchuans)
                .HasForeignKey(b => b.Id_phieunhanthietbi);

            modelBuilder.Entity<PhieuyeucauhieuchuanDoi>()
                .HasRequired(b => b.Phieuyeucauhieuchuan)
                .WithMany(p => p.PhieuyeucauhieuchuanDois)
                .HasForeignKey(b => b.Id_phieuyeucauhieuchuan); 

            modelBuilder.Entity<Phieutrathietbi>()
                   .HasRequired(b => b.Giaychungnhan)
                   .WithMany(p => p.Phieutrathietbis)
                   .HasForeignKey(b => b.Id_Giaychungnhan); 

            modelBuilder.Entity<PhieuChuyenMauHieuChuan>()
                  .HasRequired(b => b.PhieuNhanThietBi)
                  .WithMany(p => p.PhieuChuyenMauHieuChuans)
                  .HasForeignKey(b => b.Id_PhieuNhanThietBi); 

            modelBuilder.Entity<TB_STD>()
                 .HasRequired(b => b.STD_DUT)
                 .WithMany(p => p.TB_STDs)
                 .HasForeignKey(b => b.ID_STD);

            modelBuilder.Entity<BanTinhDKDBD>()
                .HasRequired(b => b.TB_STD)
                .WithMany(p => p.BanTinhDKDBDs)
                .HasForeignKey(b => b.Id_TB_STD);

            modelBuilder.Entity<Giaychungnhan>()
               .HasRequired(b => b.BienBanHieuChuan)
               .WithMany(p => p.Giaychungnhans)
               .HasForeignKey(b => b.Id_BienBanHieuChuan);

            modelBuilder.Entity<PhanCong>()
               .HasRequired(b => b.Phieuyeucauhieuchuan)
               .WithMany(p => p.PhanCongs)
               .HasForeignKey(b => b.Id_pychc); 
          
            modelBuilder.Entity<ListNumber>()
            .HasRequired(b => b.KhachHang)
            .WithMany(p => p.ListNumbers)
            .HasForeignKey(b => b.Id_KhachHang);

            modelBuilder.Entity<BaoCaoSuaChua>()
           .HasRequired(b => b.Giaychungnhan)
           .WithMany(p => p.BaoCaoSuaChuas)
           .HasForeignKey(b => b.Id_giaychungnhan);

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var addedProducts1 = ChangeTracker.Entries<Phieuyeucauhieuchuan>()
               .Where(e => e.State == EntityState.Added)
               .Select(e => e.Entity);
            foreach (var product in addedProducts1)
            {
                PhieuyeucauhieuchuanDois.Add(new PhieuyeucauhieuchuanDoi
                {
                    Id_phieuyeucauhieuchuan= product.Id,
                    MaPhieu = product.MaPhieu,
                    Id_phieunhanthietbi = product.Id_phieunhanthietbi,
                    Diachihieuchuan = product.Diachihieuchuan,
                    Tencognty = product.Tencognty,
                    Diachicongty = product.Diachicongty,
                    Masothue = product.Masothue,
                    Soluong = product.Soluong,
                    NgayTao = product.NgayTao,
                    Dichvuyeucau = product.Dichvuyeucau,
                    Createby = product.Createby,
                    UpdateBy = product.UpdateBy,
                    Danhhuy = product.Danhhuy,
                    Status = 1
                });
            }
            return base.SaveChanges();
        }
    }
}
