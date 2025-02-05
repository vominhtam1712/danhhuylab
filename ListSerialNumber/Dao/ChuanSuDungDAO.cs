using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class ChuanSuDungDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<ChuanSuDung> getListId(int? phanCongId)
        {
            return dbContext.ChuanSuDungs
                .AsNoTracking()   
                .Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }

        public IEnumerable<ChuanSuDung> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.ChuanSuDungs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }

        public bool CheckSerialNumberExists(int? Id_PhanCong)
        { 
            return Id_PhanCong.HasValue && dbContext.ChuanSuDungs.Any(m => m.Id_PhanCong == Id_PhanCong);
        }

        public int Insert(ChuanSuDung company)
        {
            dbContext.ChuanSuDungs.Add(company);
            return dbContext.SaveChanges();
        }

        public int Update(ChuanSuDung company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Delete(ChuanSuDung company)
        {
            dbContext.ChuanSuDungs.Remove(company);
            return dbContext.SaveChanges();
        }

    }
}
