using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD_DAO
    {
        ListDBContext _context = new ListDBContext();
        public List<Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD> GetListJoin(int? id)
        {
            var result = (from std in _context.STD_DUTs
                          join bbhc in _context.BienBanHieuChuans on std.Id_BienBanHieuChuan equals bbhc.Id
                          join pc in _context.PhanCongs on bbhc.Id_PhanCong equals pc.Id
                          join pyc in _context.Phieuyeucauhieuchuans on pc.Id_pychc equals pyc.Id
                          join pntb in _context.PhieuNhanThietBis on pyc.Id_phieunhanthietbi equals pntb.Id
                          join ln in _context.ListNumbers on pntb.Id_ListNumber equals ln.Id
                          join kh in _context.KhachHangs on ln.Id_KhachHang equals kh.Id 
                          join dkdb in _context.BanTinhDKDBDs on std.Id_BienBanHieuChuan equals dkdb.Id_BienBanHieuChuan
                          join tbstd in _context.TB_STDs on std.Id_BienBanHieuChuan equals tbstd.Id_BienBanHieuChuan
                          where std.MucKiemTra_STD == dkdb.MucKiemTra
                                && std.MucKiemTra_STD == tbstd.MucKiemTra
                                && std.Id_BienBanHieuChuan == id  
                          select new Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD
                          {
                              // std
                              ID_STD = std.ID,
                              MucKiemTra_STD = std.MucKiemTra_STD,
                              Lan1_STD = std.Lan1_STD,
                              Lan2_STD = std.Lan2_STD,
                              Lan3_STD = std.Lan3_STD,
                              Lan4_STD = std.Lan4_STD,
                              Lan5_STD = std.Lan5_STD,
                              Max_STD = std.Max_STD,
                              Min_STD = std.Min_STD,
                              TrungBinh_STD = std.TrungBinh_STD,
                              U_STD = std.U_STD,
                              STD_Cer_STD = std.STD_Cer_STD,
                              STD_Spec_STD = std.STD_Spec_STD,

                              MucKiemTra_DUT = std.MucKiemTra_DUT,
                              Lan1_DUT = std.Lan1_DUT,
                              Lan2_DUT = std.Lan2_DUT,
                              Lan3_DUT = std.Lan3_DUT,
                              Lan4_DUT = std.Lan4_DUT,
                              Lan5_DUT = std.Lan5_DUT,
                              Max_DUT = std.Max_DUT,
                              Min_DUT = std.Min_DUT,
                              TrungBinh_DUT = std.TrungBinh_DUT,
                              DoOnDinh_DUT = std.DoOnDinh_DUT,
                              U_DUT = std.U_DUT,
                              DoPhanGiai = std.DoPhanGiai_DUT,

                              // bbhc
                              Id_BienBanHieuChuan = bbhc.Id,
                              NhietDo = bbhc.NhietDo,
                              DoAm = bbhc.DoAm,
                              TenHieuChuan = bbhc.TenHieuChuan,
                              SN = bbhc.SN,
                              NgayThucHien = bbhc.NgayThucHien,

                              // Phancong
                              Id_PhanCong = pc.Id,
                              Id_pychc = pc.Id_pychc,
                              Id_phieunhanthietbi = pyc.Id_phieunhanthietbi,
                              Id_ListNumber = pntb.Id_ListNumber,
                              Tenthietbi = ln.Tenthietbi,
                              SerialNumber = ln.SerialNumber,
                              KhachHang = kh.TenKH,
                              Hang = ln.Hang,
                              Model = ln.Model,

                              // BienBanBanGiaoNhanThietBis
                              SoYeuCau = pntb.SoYeuCau,

                              // dkdbd
                              Id_dkdbd = dkdb.Id,
                              U_a = dkdb.U_a,
                              U_d = dkdb.U_d,
                              U_cer = dkdb.U_cer,
                              U_od_std = dkdb.U_od_std,
                              U_od = dkdb.U_od,
                              U_dd = dkdb.U_dd,
                              U_c = dkdb.U_c,
                              K = dkdb.K,
                              U_morong = dkdb.U_morong,
                              Saiso = dkdb.Saiso,
                              DoDKDB = dkdb.DoDKDB,

                              Id_tbstd = tbstd.ID,
                              TB_STD = tbstd.TrungBinh_STD
                          })
                          .AsNoTracking()  
                          .ToList();  

            return result;
        }
    }
}
