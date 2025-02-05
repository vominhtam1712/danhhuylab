using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class BanTinhDKDBD_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        Join_STD_DUT_Bienbanhieuchuan_DAO sp = new Join_STD_DUT_Bienbanhieuchuan_DAO();
        public IEnumerable<BanTinhDKDBD> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.BanTinhDKDBDs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public bool CheckSerialNumberExists(int? bienbanID)
        {
            return bienbanID.HasValue && dbContext.BanTinhDKDBDs
                                                      .Any(m => m.Id_BienBanHieuChuan == bienbanID);
        }
        public List<BanTinhDKDBD> getListId(int? id)
        {
            if (id == null)
            {
                return new List<BanTinhDKDBD>();
            }

            return dbContext.BanTinhDKDBDs
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public List<BanTinhDKDBD> getList()
        {
            var list = dbContext.BanTinhDKDBDs.Where(m => m.Status == 1);
            return list.ToList();
        }
        public List<BanTinhDKDBD> getList(int? id)
        {
            var list = dbContext.BanTinhDKDBDs.Where(m => m.Status == 1 && m.Id_BienBanHieuChuan == id);
            return list.ToList();
        }
        public float Create_Ua(float? id_tb, float? muckiemtra)
        { 
            var dutValues = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_tb && m.MucKiemTra_DUT == muckiemtra)
                .ToList();

            if (dutValues == null || dutValues.Count == 0)
                throw new InvalidOperationException("Không thể lấy giá trị dutValues hoặc danh sách rỗng.");
             
            var allValues = dutValues
                .SelectMany(dut => new[] { dut.Lan1_DUT, dut.Lan2_DUT, dut.Lan3_DUT, dut.Lan4_DUT, dut.Lan5_DUT })
                .ToList();

            if (allValues.Count == 0)
                throw new InvalidOperationException("Danh sách tất cả các giá trị rỗng.");
             
            float? mean = allValues.Average();
             
            float? variance = allValues
                .Select(val => (val - mean) * (val - mean))
                .Sum() / (allValues.Count - 1);
             
            float stdev = (float)Math.Sqrt((double)variance);
             
            float sqrt5 = (float)Math.Sqrt(5);
            float? dophangiai = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_tb && m.MucKiemTra_DUT == muckiemtra)
                .Select(m => m.TrungBinh_DUT).FirstOrDefault();

            if (dophangiai == null)
                throw new InvalidOperationException("Không thể lấy giá trị Độ phân giải.");

            float U_a = stdev / sqrt5 / dophangiai.Value * 100;

            return U_a;
        }

        public float Create_Ud(int id_bienbanhieuchuan, float? muckiemtra)
        {
            float? dophangiai = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_DUT == muckiemtra)
                .Select(m => m.DoPhanGiai_DUT)
                .FirstOrDefault();
            float Sqrt3 = (float)Math.Sqrt(3);
            float? TBdut = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_DUT == muckiemtra)
                .Select(m => m.TrungBinh_DUT)
                .FirstOrDefault();

            if (dophangiai == null || TBdut == null || TBdut == 0)
                throw new InvalidOperationException("Không thể lấy giá trị");

            float? U_d = dophangiai / Sqrt3 / TBdut;

            return (float)U_d;
        }
        public float Create_U_cer(int id_bienbanhieuchuan, float? muckiemtra)
        {
            float? STD_cer = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_STD == muckiemtra)
                .Select(m => m.STD_Cer_STD)
                .FirstOrDefault();
            float? U_cer = STD_cer/2;
            return (float)U_cer;
        }
        public float Create_U_od_std(int id_bienbanhieuchuan, float? muckiemtra)
        { 
            float? STD_Spec = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_STD == muckiemtra)
                .Select(m => m.STD_Spec_STD)
                .FirstOrDefault();
            float Sqrt3 = (float)Math.Sqrt(3);
            if (STD_Spec == null)
                throw new InvalidOperationException("Không thể lấy giá trị STD_Spec.");

            float? U_od_std = STD_Spec / Sqrt3;

            return (float)U_od_std;
        }
        public float Create_U_od(int id_bienbanhieuchuan, float? muckiemtra)
        { 
            float? Max = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_STD == muckiemtra)
                .Select(m => m.Max_STD)
                .FirstOrDefault();
            float? Min = dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_STD == muckiemtra)
                .Select(m => m.Min_STD)
                .FirstOrDefault();
            float? TB_STD = dbContext.TB_STDs
               .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra == muckiemtra)
               .Select(m => m.TrungBinh_STD)
               .FirstOrDefault();
            float Sqrt3 = (float)Math.Sqrt(3);
            if (TB_STD == null || Min==null || Max==null)
                throw new InvalidOperationException("Không thể lấy giá trị STD_Spec.");

            float? U_od = (Max-Min) /2 / Sqrt3 / TB_STD * 100;

            return (float)U_od;
        }
        public float Create_U_dd(int id_bienbanhieuchuan)
        {
            float slope = sp.CalculateSlope(id_bienbanhieuchuan);
            float Sqrt3 = (float)Math.Sqrt(3);
            float U_dd = slope / 2 / Sqrt3;
            return U_dd;
        }
        public float Create_saiso(int id_bienbanhieuchuan, float? muckiemtra)
        {
            float? TB_STD = dbContext.TB_STDs
              .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra == muckiemtra)
              .Select(m => m.TrungBinh_STD)
              .FirstOrDefault();
            float? TBdut = dbContext.STD_DUTs
              .Where(m => m.Id_BienBanHieuChuan == id_bienbanhieuchuan && m.MucKiemTra_DUT == muckiemtra)
              .Select(m => m.TrungBinh_DUT)
              .FirstOrDefault();
            float? saiso = (TB_STD- TBdut)*100/ TB_STD;
            return (float)saiso;
        }
        public int Insert(BanTinhDKDBD BanTinhDKDBD)
        {
            dbContext.BanTinhDKDBDs.Add(BanTinhDKDBD);
            return dbContext.SaveChanges();
        }
    }
}
