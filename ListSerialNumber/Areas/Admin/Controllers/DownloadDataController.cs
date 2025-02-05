using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Areas.Admin.Controllers
{
    public class DownloadDataController : LoginController
    {
        private readonly ListDBContext _context;
         
        public DownloadDataController(ListDBContext context)
        {
            _context = context;
        }
         
        public DownloadDataController() : this(new ListDBContext())
        {
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadSql()
        {
            var sqlContent = GenerateSql();
            var bytes = System.Text.Encoding.UTF8.GetBytes(sqlContent);
            return File(bytes, "application/sql", "DataBackup.sql");
        }
        private string GenerateSql()
        {
            var sqlBuilder = new StringBuilder();

            //LampSystems
            AppendInsertStatements(sqlBuilder, _context.LampSystems.ToList(), "LampSystems", new[] {
                "Id", "Lamp_Systems", "status"});

            //BulbSpectrums
            AppendInsertStatements(sqlBuilder, _context.BulbSpectrums.ToList(), "BulbSpectrums", new[] {
                "Id", "Bulb_Spectrum", "status"});

            //Table Users
            AppendInsertStatements(sqlBuilder, _context.Users.ToList(), "Users", new[] {
                "Id", "Name", "UserName", "Password", "Email","Phone", "Lever", "Status"});

            //QuyTrinhISOs
            AppendInsertStatements(sqlBuilder, _context.QuyTrinhISOs.ToList(), "QuyTrinhISOs", new[] {
                "id", "TenQuyTrinh", "NgayTao","NguoiTao","Status"});

            //QuyTrinhISO02s
            AppendInsertStatements(sqlBuilder, _context.QuyTrinhISO02s.ToList(), "QuyTrinhISO02s", new[] {
                "id","id_quytrinh","FileName", "FilePath"});

            //Phieutheodoinhietdo
            AppendInsertStatements(sqlBuilder, _context.PhieuTheoDoiNhietDos.ToList(), "PhieuTheoDoiNhietDos", new[] {
                "Id", "MaPhieuTheoDoiNhietDo", "MSNhietKe_Model_No","Phong","Thang_Nam","NhietDo_Dau","NhietDo_Sau","DoAm","NgayTao","NguoiTao","status"});

            //Danhsachphieutheodoinhietdo 
            AppendInsertStatements(sqlBuilder, _context.DanhSachPhieuTheoDoiNhietDos.ToList(), "DanhSachPhieuTheoDoiNhietDos", new[] {
                "ID", "ID_PhieuTheoDoiNhietDo", "Ngay_theo_doi", "NhietDo_Sang", "DoAm_Sang", "NhietDo_Chieu", "DoAm_Chieu", "KetLuan", "NguoiKiemTra", "Ngay", "status"});

            //Danhmucthietbi
            AppendInsertStatements(sqlBuilder, _context.DanhMucThietBis.ToList(), "DanhMucThietBis", new[] {
                "Id", "TenThietBi", "Serial", "MaThietBi", "Img", "HangSX", "NgayDuaVaoSuDung", "NguoiTao", "NgayTao", "Status"});

            //kehoachhieuchuanform
            AppendInsertStatements(sqlBuilder, _context.KeHoachHieuChuan_Forms.ToList(), "KeHoachHieuChuan_Forms", new[] {
                "Id", "MaPhieu", "NguoiTao", "NgayTao", "Status"});

            //kehoachhieuchuan
            AppendInsertStatements(sqlBuilder, _context.KeHoachHieuChuans.ToList(), "KeHoachHieuChuans", new[] {
                "Id", "Id_DanhMucThietBi", "Id_KeHoachHieuChuan_Form", "Jan_Plan", "Jan_Act", "Feb_Plan", "Feb_Act", "March_Plan", "March_Act", "April_Plan", "April_Act", "May_Plan", "May_Act", "June_Plan", "June_Act"
            , "July_Plan", "July_Act", "Aug_Plan", "Aug_Act", "Sep_Plan", "Sep_Act", "Oct_Plan", "Oct_Act", "Nov_Plan", "Nov_Act", "Dec_Plan", "Dec_Act", "NguoiTao", "NgayTao", "Status"});

            //Baocaodanhgia
            AppendInsertStatements(sqlBuilder, _context.BaoCaoDanhGias.ToList(), "BaoCaoDanhGias", new[] {
                "Id", "Id_DanhMucThietBi", "ThietBi_HoaChatHieuChuan", "NgayHieuChuan", "KetQuaHC", "KetLuan", "NgayHieuChuanKeTiep", "NguoiTao", "NgayTao", "Status"});

            //Nhatkybaotrivasuachua
            AppendInsertStatements(sqlBuilder, _context.NhatKyBaoTriVaSuChuas.ToList(), "NhatKyBaoTriVaSuChuas", new[] {
                "Id", "Id_DanhMucThietBi", "NgayLapDat", "NhatKyBaoTri","NguoiTao","NgayTao","Status"});

            //DulieuNhatkybaotrivasuachua
            AppendInsertStatements(sqlBuilder, _context.DuLieu_NhatKyBaoTriVaSuChua.ToList(), "DuLieu_NhatKyBaoTriVaSuChuas", new[] {
                "Id", "Id_NhatKyBaoTriVaSuChua", "Ngay", "MoTaSuCo", "HanhDongKhacPhuc", "KetQua", "NguoiSuaChua", "NguoiKiemTra", "Status"});

            //TapDoans
            AppendInsertStatements(sqlBuilder, _context.TapDoans.ToList(), "TapDoans", new[] {
                "Id", "Tentapdoan", "Status"});

            //KhuVucs
            AppendInsertStatements(sqlBuilder, _context.KhuVucs.ToList(), "KhuVucs", new[] {
                "Id", "Tenkhuvuc", "Status"});

            //Khachhangs
            AppendInsertStatements(sqlBuilder, _context.KhachHangs.ToList(), "KhachHangs", new[] {
                "Id", "MaKH", "TenKH", "DiaChi", "Email", "SDT", "LienHe", "Ghichu", "NhomNganh", "NguoiTao", "NgayTao", "Status","Id_TapDoan","Id_KhuVuc"});

            //Listnumbers 
            AppendInsertStatements(sqlBuilder, _context.ListNumbers.ToList(), "ListNumbers", new[] {
                "Id", "Id_KhachHang", "Tenthietbi", "Image", "Hang", "Model", "SerialNumber", "PhamViDo","DoPhanGiai","NguoiTao", "GhiChu", "NgaytaoSanPham", "Status"});
             

            //Phieunhanthietbi
            AppendInsertStatements(sqlBuilder, _context.PhieuNhanThietBis.ToList(), "PhieuNhanThietBis", new[] {
                "Id", "Id_ListNumber", "SoNhanDang", "SoYeuCau", "NguoiThucHien", "HienTrang", "GhiChu", "NgayThucHien", "Ngaytao", "Nguoitao", "Status"});
             

            //Phieuchuyenmauhieuchuan
            AppendInsertStatements(sqlBuilder, _context.PhieuChuyenMauHieuChuans.ToList(), "PhieuChuyenMauHieuChuans", new[] {
                "Id", "Id_PhieuNhanThietBi", "MaSoMau", "NgayNhan", "NguoiNhan", "ThongSoHieuChuan", "NguoiDuocPhanCong", "PhuongPhamHieuChuan", "NgayTraBaoCao", "NgayTaoPhieu", "NguoiTao", "Status"});

            //Phieuyeucauhieuchuan
            AppendInsertStatements(sqlBuilder, _context.Phieuyeucauhieuchuans.ToList(), "Phieuyeucauhieuchuans", new[] {
                "Id", "MaPhieu", "Id_phieunhanthietbi", "Diachihieuchuan", "Tencognty", "Diachicongty", "Masothue", "Soluong", "Dichvuyeucau", "Createby", "UpdateBy", "NgayTao", "Danhhuy", "Status"});

            //PhieuyeucauhieuchuanDoi
            AppendInsertStatements(sqlBuilder, _context.PhieuyeucauhieuchuanDois.ToList(), "PhieuyeucauhieuchuanDois", new[] {
                "Id", "MaPhieu","Id_phieuyeucauhieuchuan","Id_phieunhanthietbi", "Diachihieuchuan", "Tencognty", "Diachicongty", "Masothue", "Soluong", "Dichvuyeucau", "Createby", "UpdateBy", "NgayTao", "Danhhuy", "Status"});

            //Company
            AppendInsertStatements(sqlBuilder, _context.Companys.ToList(), "Companys", new[] {
                "Id", "Name", "UserName", "Password", "Email", "Phone", "Status","Img","SoCCCD","QueQuan","NamSinh","GioiTinh","NgayVaoLam", "BacDaoTao","ChuyenNganh","ChucVu"});

            //Roles
            AppendInsertStatements(sqlBuilder, _context.Roles.ToList(), "Roles", new[] {
                "Id","MaQuyen","NameRoles","Nhom","Status"});

            //Ngaynghis
            AppendInsertStatements(sqlBuilder, _context.Ngaynghis.ToList(), "Ngaynghis", new[] {
                "Id","Id_Company","Ngaybatdau","Ngayketthuc","LyDoXinPhep","TongNgay","Status"});

            //CompanyRoles
            AppendInsertStatements(sqlBuilder, _context.CompanyRoles.ToList(), "CompanyRoles", new[] {
                "Id","Id_Company", "Id_Role","MaQuyen"});

            //Phancong
            AppendInsertStatements(sqlBuilder, _context.PhanCongs.ToList(), "PhanCongs", new[] {
                "Id", "MaPC", "Id_pychc", "Id_NV", "NgayTao", "NguoiTao", "Status"});

            //Bienbanhieuchuan
            AppendInsertStatements(sqlBuilder, _context.BienBanHieuChuans.ToList(), "BienBanHieuChuans", new[] {
                "Id", "Id_PhanCong", "MaBienBan", "NhietDo", "DoAm", "TenHieuChuan", "SN", "NgayThucHien", "NgayTao", "Cal_Date", "Due_Day", "NguoiTao", "Status"});

            //Giaychungnhan
            AppendInsertStatements(sqlBuilder, _context.Giaychungnhans.ToList(), "Giaychungnhans", new[] {
                "Id", "Id_BienBanHieuChuan", "MaGCN","Type", "NguoiTao", "NgayTao", "NgayHieuChuan", "NgayHieuChuanLai", "status"}); 

            //Phieutrathietbis
            AppendInsertStatements(sqlBuilder, _context.Phieutrathietbis.ToList(), "Phieutrathietbis", new[] {
                "Id", "Id_Giaychungnhan", "MaPT", "PhuongThucGiaoTra", "TrangThaiThietbi","NguoiTao", "Ghichu", "NgayThucHien", "NgayTao", "status"});

            //STD_DUTs
            AppendInsertStatements(sqlBuilder, _context.STD_DUTs.ToList(), "STD_DUTs", new[] {
                "ID", "Id_BienBanHieuChuan", "MucKiemTra_DUT", "Lan1_DUT", "Lan2_DUT",
                "Lan3_DUT", "Lan4_DUT", "Lan5_DUT", "Max_DUT", "Min_DUT",
                "TrungBinh_DUT", "DoOnDinh_DUT", "U_DUT", "DoPhanGiai_DUT", "MucKiemTra_STD",
                "Lan1_STD", "Lan2_STD", "Lan3_STD", "Lan4_STD", "Lan5_STD",
                "Max_STD", "Min_STD", "TrungBinh_STD", "DoOnDinh_STD", "U_STD",
                "STD_Cer_STD", "STD_Spec_STD", "NguoiTao", "Status"});

            //TB_STDs
            AppendInsertStatements(sqlBuilder, _context.TB_STDs.ToList(), "TB_STDs", new[] {
                "ID", "ID_STD", "Id_BienBanHieuChuan", "MucKiemTra", "TrungBinh_STD", "NguoiTao", "Status"});

            //BanTinhDKDBDs
            AppendInsertStatements(sqlBuilder, _context.BanTinhDKDBDs.ToList(), "BanTinhDKDBDs", new[] {
                "Id", "Id_TB_STD", "Id_BienBanHieuChuan", "MucKiemTra", "U_a", "U_d", "U_cer",
                "U_od_std", "U_od", "U_dd", "U_c", "K", "U_morong", "Saiso",
                "DoDKDB", "NguoiTao", "Status"});

            //BaoCaoSuaChuas
            AppendInsertStatements(sqlBuilder, _context.BaoCaoSuaChuas.ToList(), "BaoCaoSuaChuas", new[] {
                "Id", "Id_giaychungnhan", "MaBaoCao", "CacHangSuDung", "NguoiTao", "NgayTao","Status"});
              
            //Chuansudungs
            AppendInsertStatements(sqlBuilder, _context.ChuanSuDungs.ToList(), "ChuanSuDungs", new[] {
                "Id", "Id_PhanCong", "Model", "Description", "Serial", "Cetificate","Cal_Date","Cal_Due","NgayTao","NguoiTao","Status"});

            //NgayTaoTechnicialBuldTruocHieuChinhs
            AppendInsertStatements(sqlBuilder, _context.NgayTaoTechnicialBuldTruocHieuChinhs.ToList(), "NgayTaoTechnicialBuldTruocHieuChinhs", new[] {
                "Id", "Id_PhanCong", "Technicial","Id_BulbSpectrum","Id_LampSystems","NgayTao","NguoiTao","Status"});

            //GiaTriTruocHieuChinhs
            AppendInsertStatements(sqlBuilder, _context.GiaTriTruocHieuChinhs.ToList(), "GiaTriTruocHieuChinhs", new[] {
                "Id", "Id_PhanCong", "BuocSong", "Measurand_1","Reference_1","Instrument_1","Deviation_1","Measurand_2","Reference_2","Instrument_2","Deviation_2","NgayTao","NguoiTao","Status"});

            //NgayTaoTechnicialBuldKetQuaHieuChinhs
            AppendInsertStatements(sqlBuilder, _context.NgayTaoTechnicialBuldKetQuaHieuChinhs.ToList(), "NgayTaoTechnicialBuldKetQuaHieuChinhs", new[] {
                 "Id", "Id_PhanCong", "Technicial","Id_BulbSpectrum","Id_LampSystems","NgayTao","NguoiTao","Status"});

            //KetQuaHieuChinhs
            AppendInsertStatements(sqlBuilder, _context.KetQuaHieuChinhs.ToList(), "KetQuaHieuChinhs", new[] {
                "Id", "Id_PhanCong", "BuocSong", "Measurand_1","Reference_1","Instrument_1","Deviation_1","Measurand_2","Reference_2","Instrument_2","Deviation_2","NgayTao","NguoiTao","Status"});

            //PhanCongGhiChus
            AppendInsertStatements(sqlBuilder, _context.PhanCongGhiChus.ToList(), "PhanCongGhiChus", new[] {
                "Id", "Id_PhanCong", "GhiChu", "NguoiTao","NgayTao"});

            //GiayCNSanPhams
            AppendInsertStatements(sqlBuilder, _context.GiayCNSanPhams.ToList(), "GiayCNSanPhams", new[] {
                "Id", "Id_ListNumber", "FileName", "FilePath"});


            return sqlBuilder.ToString();
        }

        private void AppendInsertStatements(StringBuilder sqlBuilder, IEnumerable<object> items, string tableName, string[] columns)
        { 
            sqlBuilder.AppendLine($"SET IDENTITY_INSERT {tableName} ON;");

            sqlBuilder.AppendLine($"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES");

            var firstItem = true; 

            foreach (var item in items)
            {
                var values = columns.Select(col => GetValue(item, col)).ToArray();

                if (firstItem)
                {
                    firstItem = false;  
                    sqlBuilder.Append($"({string.Join(", ", values)})"); 
                }
                else
                {
                    sqlBuilder.AppendLine($", ({string.Join(", ", values)})"); 
                }
            }

            if (items.Any())
            {
                sqlBuilder.AppendLine(";");  
            }
             
            sqlBuilder.AppendLine($"SET IDENTITY_INSERT {tableName} OFF;");
            sqlBuilder.AppendLine();
        }
        private string GetValue(object item, string propertyName)
        {
            var value = item.GetType().GetProperty(propertyName)?.GetValue(item, null);

            if (value is null) return "NULL";

            if (value is string s)
                return $"N'{s.Replace("'", "''")}'";  

            if (value is DateTime d)
                return $"'{d:yyyy-MM-dd HH:mm:ss}'";  

            return value.ToString();
        }
    }
}