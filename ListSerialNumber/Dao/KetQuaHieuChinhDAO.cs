using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class KetQuaHieuChinhDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<KetQuaHieuChinh> getListId(int? phanCongId)
        {
            return dbContext.KetQuaHieuChinhs
                .AsNoTracking()   
                .Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }
        public IEnumerable<KetQuaHieuChinh> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.KetQuaHieuChinhs
                .AsNoTracking() 
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public bool Check(int? Id_PhanCong, string buocsong)
        {
            return Id_PhanCong.HasValue &&
                   dbContext.KetQuaHieuChinhs
                            .Any(m => m.Id_PhanCong == Id_PhanCong && m.BuocSong == buocsong);
        }
        public float Stand_1(int? id, string buocSong)
        {
            if (id == null || string.IsNullOrEmpty(buocSong))
            {
                throw new ArgumentNullException("id hoặc buocSong không thể null");
            }

            var stdValues = dbContext.KetQuaHieuChinhs
                                     .Where(m => m.Id_PhanCong == id && m.BuocSong == buocSong)
                                     .Select(m => m.Deviation_1)  
                                     .ToList();

            if (!stdValues.Any())
            {
                return 0f;
            }

            float xMean = stdValues.Average();
            float sumOfSquares = stdValues.Select(val => (val - xMean) * (val - xMean)).Sum();
            return (float)Math.Sqrt(sumOfSquares / (stdValues.Count - 1));
        }

        public float Stand_2(int? id, string buocSong)
        {
            if (id == null || string.IsNullOrEmpty(buocSong))
            {
                throw new ArgumentNullException("id hoặc buocSong không thể null");
            }

            var stdValues = dbContext.KetQuaHieuChinhs
                                     .Where(m => m.Id_PhanCong == id && m.BuocSong == buocSong)
                                     .Select(m => m.Deviation_2)  
                                     .ToList();

            if (!stdValues.Any())
            {
                return 0f;
            }

            float xMean = stdValues.Average();
            float sumOfSquares = stdValues.Select(val => (val - xMean) * (val - xMean)).Sum();
            return (float)Math.Sqrt(sumOfSquares / (stdValues.Count - 1));
        }
        public float Average_1(int? id_Phancong, string buocsong)
        {
            var stdValues = dbContext.KetQuaHieuChinhs
                                     .Where(m => m.Id_PhanCong == id_Phancong && m.BuocSong == buocsong)
                                     .Select(m => m.Deviation_1)  
                                     .ToList();

            return stdValues.Average();
        }

        public float Average_2(int? id_Phancong, string buocsong)
        {
            var stdValues = dbContext.KetQuaHieuChinhs
                                     .Where(m => m.Id_PhanCong == id_Phancong && m.BuocSong == buocsong)
                                     .Select(m => m.Deviation_2)  
                                     .ToList();

            return stdValues.Average();
        }
        public bool CheckSerialNumberExists(int? Id_PhanCong)
        {
            return Id_PhanCong.HasValue && dbContext.KetQuaHieuChinhs
                                                      .Any(m => m.Id_PhanCong == Id_PhanCong);
        }
        public int Insert(KetQuaHieuChinh company)
        {
            dbContext.KetQuaHieuChinhs.Add(company);
            return dbContext.SaveChanges();
        }

        public int Update(KetQuaHieuChinh company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Delete(KetQuaHieuChinh company)
        {
            dbContext.KetQuaHieuChinhs.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
