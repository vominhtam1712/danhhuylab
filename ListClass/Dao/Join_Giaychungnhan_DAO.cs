using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_Giaychungnhan_DAO
    {
        ListDBContext _context = new ListDBContext();
        public List<Join_Giaychungnhan> GetListJoin(int? id)
        {
            var result = (from gcn in _context.Giaychungnhans.AsNoTracking()
                          join bbhc in _context.BienBanHieuChuans.AsNoTracking()
                              on gcn.Id_BienBanHieuChuan equals bbhc.Id
                          join std in _context.STD_DUTs.AsNoTracking()
                              on bbhc.Id equals std.Id_BienBanHieuChuan
                          join tbstd in _context.TB_STDs.AsNoTracking()
                              on std.ID equals tbstd.ID_STD
                          join dkdb in _context.BanTinhDKDBDs.AsNoTracking()
                              on tbstd.ID equals dkdb.Id_TB_STD
                          join pc in _context.PhanCongs.AsNoTracking()
                              on bbhc.Id_PhanCong equals pc.Id
                          join pyc in _context.Phieuyeucauhieuchuans.AsNoTracking()
                              on pc.Id_pychc equals pyc.Id
                          join pntb in _context.PhieuNhanThietBis.AsNoTracking()
                              on pyc.Id_phieunhanthietbi equals pntb.Id 
                          join ln in _context.ListNumbers.AsNoTracking()
                              on pntb.Id_ListNumber equals ln.Id
                          join kh in _context.KhachHangs.AsNoTracking()
                              on ln.Id_KhachHang equals kh.Id
                          join csd in _context.ChuanSuDungs.AsNoTracking()
                              on pc.Id equals csd.Id_PhanCong
                          join ngaytaotruoc in _context.NgayTaoTechnicialBuldTruocHieuChinhs.AsNoTracking()
                              on pc.Id equals ngaytaotruoc.Id_PhanCong
                          join ngaytaosau in _context.NgayTaoTechnicialBuldKetQuaHieuChinhs.AsNoTracking()
                              on pc.Id equals ngaytaosau.Id_PhanCong
                          join giatritruoc in _context.GiaTriTruocHieuChinhs.AsNoTracking()
                              on pc.Id equals giatritruoc.Id_PhanCong
                          join giatrisau in _context.KetQuaHieuChinhs.AsNoTracking()
                              on pc.Id equals giatrisau.Id_PhanCong
                          join lampsystem1 in _context.LampSystems.AsNoTracking()
                          on ngaytaotruoc.Id_LampSystems equals lampsystem1.Id
                          join bulbspectrums1 in _context.BulbSpectrums.AsNoTracking()
                            on ngaytaotruoc.Id_BulbSpectrum equals bulbspectrums1.Id  
                          join lampsystem2 in _context.LampSystems.AsNoTracking()
                            on ngaytaosau.Id_LampSystems equals lampsystem2.Id
                          join bulbspectrums2 in _context.BulbSpectrums.AsNoTracking()
                            on ngaytaosau.Id_BulbSpectrum equals bulbspectrums2.Id
                          where std.MucKiemTra_STD == dkdb.MucKiemTra
                                && std.MucKiemTra_STD == tbstd.MucKiemTra
                                && gcn.Id == id
                          select new Join_Giaychungnhan
                          {
                              Id_GCN = gcn.Id,
                              NgayHieuChuan = gcn.NgayHieuChuan,
                              Type = gcn.Type,
                              NgayHieuChuanLai = gcn.NgayHieuChuanLai,
                              SoNhanDang = pntb.SoNhanDang,

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
                               
                              Id_BienBanHieuChuan = bbhc.Id,
                              NhietDo = bbhc.NhietDo,
                              DoAm = bbhc.DoAm,
                              TenHieuChuan = bbhc.TenHieuChuan,
                              SN = bbhc.SN,
                              NgayThucHien = bbhc.NgayThucHien,
                              Cal_Date = bbhc.Cal_Date,
                              Due_Day = bbhc.Due_Day,
                               
                              Id_PhanCong = pc.Id,
                              Id_pychc = pc.Id_pychc,
                              Id_phieunhanthietbi = pyc.Id_phieunhanthietbi,
                              Id_ListNumber = pntb.Id_ListNumber,
                              Tenthietbi = ln.Tenthietbi,
                              Hang = ln.Hang,
                              SerialNumber = ln.SerialNumber,
                              Model = ln.Model,
                              PhammVido_listnumber = ln.PhamViDo,
                              DoPhanGiai_listnumber = ln.DoPhanGiai,
                               
                              SoYeuCau = pntb.SoYeuCau,
                              KhachHang = kh.TenKH,

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
                              TB_STD = tbstd.TrungBinh_STD,

                              Model_ChuanSuDung = csd.Model,
                              Description_ChuanSuDung = csd.Description,
                              Serial_ChuanSuDung = csd.Serial,
                              Cetificate_ChuanSuDung = csd.Cetificate,
                              Cal_Date_ChuanSuDung = csd.Cal_Date,
                              Cal_Due_ChuanSuDung = csd.Cal_Due,

                              NgayTao_NgayTaoTechnicialBuldTruocHieuChinh = ngaytaotruoc.NgayTao,
                              Technicial_NgayTaoTechnicialBuldTruocHieuChinh = ngaytaotruoc.Technicial,
                              Bulb_Spectrum_NgayTaoTechnicialBuldTruocHieuChinh = bulbspectrums1.Bulb_Spectrum,
                              LampSystem_NgayTaoTechnicialBuldTruocHieuChinh = lampsystem1.Lamp_Systems,
                              
                              NgayTao_NgayTaoTechnicialBuldKetQuaHieuChinh = ngaytaosau.NgayTao,
                              Technicial_NgayTaoTechnicialBuldKetQuaHieuChinh = ngaytaosau.Technicial,
                              Bulb_Spectrum_NgayTaoTechnicialBuldKetQuaHieuChinh = bulbspectrums2.Bulb_Spectrum,
                              LampSystem_NgayTaoTechnicialBuldKetQuaHieuChinh = lampsystem2.Lamp_Systems,

                              Id_GiaTriTruocHieuChinh = giatritruoc.Id,
                              BuocSong_GiaTriTruocHieuChinh = giatritruoc.BuocSong,
                              Measurand_1_GiaTriTruocHieuChinh = giatritruoc.Measurand_1,
                              Measurand_2_GiaTriTruocHieuChinh = giatritruoc.Measurand_2, 
                              Reference_1_GiaTriTruocHieuChinh = giatritruoc.Reference_1,
                              Reference_2_GiaTriTruocHieuChinh = giatritruoc.Reference_2, 
                              Instrument_1_GiaTriTruocHieuChinh = giatritruoc.Instrument_1,
                              Instrument_2_GiaTriTruocHieuChinh = giatritruoc.Instrument_2, 
                              Deviation_1_GiaTriTruocHieuChinh = giatritruoc.Deviation_1, 
                              Deviation_2_GiaTriTruocHieuChinh = giatritruoc.Deviation_2,

                              Id_KetQuaHieuChinh = giatrisau.Id,
                              BuocSong_KetQuaHieuChinh = giatrisau.BuocSong,
                              Measurand_1_KetQuaHieuChinh = giatrisau.Measurand_1,
                              Measurand_2_KetQuaHieuChinh = giatrisau.Measurand_2,
                              Reference_1_KetQuaHieuChinh = giatrisau.Reference_1,
                              Reference_2_KetQuaHieuChinh = giatrisau.Reference_2, 
                              Instrument_1_KetQuaHieuChinh = giatrisau.Instrument_1,
                              Instrument_2_KetQuaHieuChinh = giatrisau.Instrument_2,
                              Deviation_1_KetQuaHieuChinh = giatrisau.Deviation_1,
                              Deviation_2_KetQuaHieuChinh = giatrisau.Deviation_2,
                               
                          })
              .ToList();

            return result;
        }
    }
}
