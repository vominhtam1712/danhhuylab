using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DAO
    {
        ListDBContext _context = new ListDBContext();
        public List<Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER> GetListJoin(int? ids)
        { 
            var result = (from std in _context.STD_DUTs
                          join bbhc in _context.BienBanHieuChuans on std.Id_BienBanHieuChuan equals bbhc.Id
                          join pc in _context.PhanCongs on bbhc.Id_PhanCong equals pc.Id
                          join pyc in _context.Phieuyeucauhieuchuans on pc.Id_pychc equals pyc.Id
                          join pntb in _context.PhieuNhanThietBis on pyc.Id_phieunhanthietbi equals pntb.Id
                          join ln in _context.ListNumbers on pntb.Id_ListNumber equals ln.Id
                          join kh in _context.KhachHangs on ln.Id_KhachHang equals kh.Id  
                          where bbhc.Id == ids  
                          select new Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER
                          {
                              // std
                              ID_STD = std.ID,
                              MucKiemTra_STD = std.MucKiemTra_STD,
                              Lan1_STD = std.Lan1_STD,
                              Lan2_STD = std.Lan2_STD,
                              Lan3_STD = std.Lan3_STD,
                              Lan4_STD = std.Lan4_STD,
                              Lan5_STD = std.Lan5_STD,
                              // dut
                              MucKiemTra_DUT = std.MucKiemTra_DUT,
                              Lan1_DUT = std.Lan1_DUT,
                              Lan2_DUT = std.Lan2_DUT,
                              Lan3_DUT = std.Lan3_DUT,
                              Lan4_DUT = std.Lan4_DUT,
                              Lan5_DUT = std.Lan5_DUT,
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
                              Hang = ln.Hang,
                              SerialNumber = ln.SerialNumber,
                              Model = ln.Model,
                              // BienBanBanGiaoNhanThietBis
                              SoYeuCau = pntb.SoYeuCau,
                              KhachHang = kh.TenKH,
                          })
                          .AsNoTracking()  
                          .ToList();

            return result;
        }
    }
}
