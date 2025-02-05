using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class TB_STD_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public IEnumerable<TB_STD> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.TB_STDs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.ID))
                .ToList();
        }
        public List<TB_STD> getList(int? id)
        { 
            if (id == null)
                return new List<TB_STD>();
             
            return dbContext.TB_STDs
                .Where(m => m.Status == 1 && m.Id_BienBanHieuChuan == id)
                .ToList();
        }

        public int Insert(TB_STD TB_STD)
        {
            dbContext.TB_STDs.Add(TB_STD);
            return dbContext.SaveChanges();
        }
        public int Update(TB_STD TB_STD)
        {
            dbContext.Entry(TB_STD).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(TB_STD TB_STD)
        {
            dbContext.TB_STDs.Remove(TB_STD);
            return dbContext.SaveChanges();
        }
    }
}
