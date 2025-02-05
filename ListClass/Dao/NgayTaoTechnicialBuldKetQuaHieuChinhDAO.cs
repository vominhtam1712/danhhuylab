using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class NgayTaoTechnicialBuldKetQuaHieuChinhDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<NgayTaoTechnicialBuldKetQuaHieuChinh> getListId(int? phanCongId)
        {
            return dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs
                .AsNoTracking().Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }
        public bool CheckSerialNumberExists(int? Id_PhanCong)
        {
            if (Id_PhanCong.HasValue)
            {
                return dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.AsNoTracking().Any(m => m.Id_PhanCong == Id_PhanCong);
            }
            return false;
        }
        public IEnumerable<NgayTaoTechnicialBuldKetQuaHieuChinh> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }

        //public string GetBulbSpectrum(int idPhanCong)
        //{
        //    var result = dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.AsNoTracking()
        //        .FirstOrDefault(nt => nt.Id_PhanCong == idPhanCong);

        //    return result?.Bulb_Spectrum;
        //}
        public string GetTechnicial(int idPhanCong)
        {
            var result = dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.AsNoTracking()
                .FirstOrDefault(nt => nt.Id_PhanCong == idPhanCong);

            return result?.Technicial;
        }
        public int Insert(NgayTaoTechnicialBuldKetQuaHieuChinh company)
        {
            dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(NgayTaoTechnicialBuldKetQuaHieuChinh company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(NgayTaoTechnicialBuldKetQuaHieuChinh company)
        {
            dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
