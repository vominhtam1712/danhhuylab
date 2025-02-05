using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListClass.Model;

namespace ListClass.Dao
{
    public class DuLieu_NhatKyBaoTriVaSuChua_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public DuLieu_NhatKyBaoTriVaSuChua getRow(int? id)
        {
            if (id == null) return null;

            return dbContext.DuLieu_NhatKyBaoTriVaSuChua
                .AsNoTracking()
                .SingleOrDefault(m => m.Id == id);
        }
        public IEnumerable<DuLieu_NhatKyBaoTriVaSuChua> GetRowsByIds(IEnumerable<int> ids)
        {
            var idSet = new HashSet<int>(ids);
            return dbContext.DuLieu_NhatKyBaoTriVaSuChua
                .AsNoTracking()
                .Where(ln => idSet.Contains(ln.Id))
                .ToList();
        }
        public int Insert(DuLieu_NhatKyBaoTriVaSuChua company)
        {
            dbContext.DuLieu_NhatKyBaoTriVaSuChua.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(DuLieu_NhatKyBaoTriVaSuChua company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(DuLieu_NhatKyBaoTriVaSuChua company)
        {
            dbContext.DuLieu_NhatKyBaoTriVaSuChua.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
