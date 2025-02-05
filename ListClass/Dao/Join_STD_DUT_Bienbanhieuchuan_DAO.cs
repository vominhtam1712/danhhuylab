using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ListClass.Dao
{
    public class Join_STD_DUT_Bienbanhieuchuan_DAO
    {
        ListDBContext context = new ListDBContext();
        public List<Join_STD_DUT_Bienbanhieuchuan> GetListJoin(int? id)
        {
            var result = (from std_dut in context.STD_DUTs
                          join bbhc in context.BienBanHieuChuans
                              on std_dut.Id_BienBanHieuChuan equals bbhc.Id
                          where std_dut.Id_BienBanHieuChuan == id 
                          select new Join_STD_DUT_Bienbanhieuchuan
                          {
                              ID = std_dut.ID,
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
        public float CalculateSlope(int id_tb)
        { 
            var stdValues = context.STD_DUTs
                                   .Where(m => m.Id_BienBanHieuChuan == id_tb)
                                   .AsNoTracking()  
                                   .ToList();

            var x = stdValues.Select(m => m.TrungBinh_STD ?? 0).ToList();
            var y = stdValues.Select(m => m.TrungBinh_DUT ?? 0).ToList();
             
            float meanX = x.Average();
            float meanY = y.Average();
             
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < x.Count; i++)
            {
                float diffX = x[i] - meanX;
                float diffY = y[i] - meanY;
                numerator += diffX * diffY;
                denominator += diffY * diffY;
            }
             
            if (denominator == 0)
            {
                throw new ArgumentException("Mẫu số bằng 0. Không thể tính độ dốc.");
            }

            return numerator / denominator;
        }
        public float? TrungBinhCong_STD(int id_tb)
        { 
            var stdValues = context.STD_DUTs
                                   .Where(m => m.Id_BienBanHieuChuan == id_tb)
                                   .AsNoTracking() 
                                   .ToList();
             
            var x = stdValues.Select(m => m.TrungBinh_STD ?? 0).ToList();
            return x.Average();
        }

        public float? TrungBinhCong_DUT(int id_tb)
        { 
            var dutValues = context.STD_DUTs
                                   .Where(m => m.Id_BienBanHieuChuan == id_tb)
                                   .AsNoTracking() 
                                   .ToList();
             
            var y = dutValues.Select(m => m.TrungBinh_DUT ?? 0).ToList();
            return y.Average();
        }
        public float CalculateSlope(int? id)
        { 
            var stdValues = context.STD_DUTs.Where(m => m.Id_BienBanHieuChuan == id).AsNoTracking().ToList();
             
            var x = stdValues.Select(m => m.TrungBinh_STD ?? 0).ToList();
            var y = stdValues.Select(m => m.TrungBinh_DUT ?? 0).ToList(); 

            float meanX = x.Average();
            float meanY = y.Average();
             
            float numerator = 0;
            float denominator = 0;
            for (int i = 0; i < x.Count; i++)
            {
                float diffX = x[i] - meanX;
                float diffY = y[i] - meanY;
                numerator += diffX * diffY;
                denominator += diffY * diffY;
            } 
            if (denominator == 0)
            {
                throw new ArgumentException("Mẫu số bằng 0. Không thể tính độ dốc.");
            }
             
            return numerator / denominator;
        }
        public float? TrungBinhCong_STD(int? id)
        {
            var stdValues = context.STD_DUTs.Where(m => m.Id_BienBanHieuChuan == id).AsNoTracking().ToList();
            var x = stdValues.Select(m => m.TrungBinh_STD ?? 0).ToList();
            float xMean = x.Average();
            return xMean;
        }
        public float? TrungBinhCong_DUT(int? id)
        {
            var dutValues = context.STD_DUTs.Where(m => m.Id_BienBanHieuChuan == id).AsNoTracking().ToList();
            var y = dutValues.Select(m => m.TrungBinh_DUT ?? 0).ToList();
            float yMean = y.Average();
            return yMean;
        }
    }
}
