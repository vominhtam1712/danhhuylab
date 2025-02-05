using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class NgayTaoTechnicialBuldTruocHieuChinhDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<NgayTaoTechnicialBuldTruocHieuChinh> getListId(int? phanCongId)
        {
            return dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.AsNoTracking()
                .Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }
        public bool CheckSerialNumberExists(int? Id_PhanCong)
        {
            if (Id_PhanCong.HasValue)
            {
                return dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.AsNoTracking().Any(m => m.Id_PhanCong == Id_PhanCong);
            }
            return false;
        }
        public IEnumerable<NgayTaoTechnicialBuldTruocHieuChinh> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.NgayTaoTechnicialBuldTruocHieuChinhs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        } 

        //public string GetBulbSpectrum(int idPhanCong)
        //{
        //    var result = dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.AsNoTracking()
        //        .FirstOrDefault(nt => nt.Id_PhanCong == idPhanCong);

        //    return result?.Bulb_Spectrum; 
        //}
        public string GetTechnicial(int idPhanCong)
        {
            var result = dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.AsNoTracking()
                .FirstOrDefault(nt => nt.Id_PhanCong == idPhanCong);

            return result?.Technicial;
        } 
        public int Insert(NgayTaoTechnicialBuldTruocHieuChinh company)
        {
            dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(NgayTaoTechnicialBuldTruocHieuChinh company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(NgayTaoTechnicialBuldTruocHieuChinh company)
        {
            dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
