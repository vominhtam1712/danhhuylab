using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_STD_DUT_PhanCong_DAO
    {
        ListDBContext context = new ListDBContext();
        public List<Join_STD_DUT_PhanCong> GetListJoin(int? id)
        {
            var result = (from std_dut in context.STD_DUTs
                          join bbhc in context.BienBanHieuChuans
                              on std_dut.Id_BienBanHieuChuan equals bbhc.Id
                          join pc in context.PhanCongs
                              on bbhc.Id_PhanCong equals pc.Id
                          where std_dut.Id_BienBanHieuChuan == id
                          select new Join_STD_DUT_PhanCong
                          {
                              ID = std_dut.ID,
                              Id_PhanCong = pc.Id,
                              Id_BienBanHieuChuan = std_dut.Id_BienBanHieuChuan,
                              MucKiemTra_DUT = std_dut.MucKiemTra_DUT,
                              Lan1_DUT = std_dut.Lan1_DUT,
                              Lan2_DUT = std_dut.Lan2_DUT,
                              Lan3_DUT = std_dut.Lan3_DUT,
                              Lan4_DUT = std_dut.Lan4_DUT,
                              Lan5_DUT = std_dut.Lan5_DUT,
                              Max_DUT = std_dut.Max_DUT,
                              Min_DUT = std_dut.Min_DUT,
                              TrungBinh_DUT = std_dut.TrungBinh_DUT,
                              DoOnDinh_DUT = std_dut.DoOnDinh_DUT,
                              U_DUT = std_dut.U_DUT,
                              DoPhanGiai_DUT = std_dut.DoPhanGiai_DUT,
                              MucKiemTra_STD = std_dut.MucKiemTra_STD,
                              Lan1_STD = std_dut.Lan1_STD,
                              Lan2_STD = std_dut.Lan2_STD,
                              Lan3_STD = std_dut.Lan3_STD,
                              Lan4_STD = std_dut.Lan4_STD,
                              Lan5_STD = std_dut.Lan5_STD,
                              Max_STD = std_dut.Max_STD,
                              Min_STD = std_dut.Min_STD,
                              TrungBinh_STD = std_dut.TrungBinh_STD,
                              DoOnDinh_STD = std_dut.DoOnDinh_STD,
                              U_STD = std_dut.U_STD,
                              STD_Cer_STD = std_dut.STD_Cer_STD,
                              STD_Spec_STD = std_dut.STD_Spec_STD,
                              NhietDo = bbhc.NhietDo,
                              DoAm = bbhc.DoAm,
                              TenHieuChuan = bbhc.TenHieuChuan,
                              SN = bbhc.SN,
                              NgayThucHien = bbhc.NgayThucHien,
                              NguoiTao = std_dut.NguoiTao,
                              Status = std_dut.Status,
                          })
                          .AsNoTracking()  
                          .ToList();  
            return result;
        }
    }
}
