using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class GiaTriTruocHieuChinhDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<GiaTriTruocHieuChinh> getListId(int? phanCongId)
        {
            return dbContext.GiaTriTruocHieuChinhs
                .AsNoTracking()
                .Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }
        public IEnumerable<GiaTriTruocHieuChinh> GetRowsByIds(IEnumerable<int> ids)
        {
            var idSet = new HashSet<int>(ids); 
            return dbContext.GiaTriTruocHieuChinhs
                .AsNoTracking()
                .Where(ln => idSet.Contains(ln.Id))
                .ToList();
        }
        public bool CheckSerialNumberExists(int? Id_PhanCong)
        {
            return Id_PhanCong.HasValue && dbContext.GiaTriTruocHieuChinhs.Any(m => m.Id_PhanCong == Id_PhanCong);
        }
        public int Insert(GiaTriTruocHieuChinh company)
        {
            dbContext.GiaTriTruocHieuChinhs.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(GiaTriTruocHieuChinh company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(GiaTriTruocHieuChinh company)
        {
            dbContext.GiaTriTruocHieuChinhs.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
