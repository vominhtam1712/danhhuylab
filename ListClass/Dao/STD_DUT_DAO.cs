using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class STD_DUT_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public IEnumerable<STD_DUT> GetRowsByIds(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
                return Enumerable.Empty<STD_DUT>();

            return dbContext.STD_DUTs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.ID))
                .ToList();
        }
        public bool CheckSerialNumberExists(int? bienbanID)
        {
            return bienbanID.HasValue && dbContext.STD_DUTs
                                                      .Any(m => m.Id_BienBanHieuChuan == bienbanID);
        }
        public List<STD_DUT> getListId(int? id)
        {
            if (id == null)
            {
                return new List<STD_DUT>();
            }

            return dbContext.STD_DUTs
                .Where(m => m.Id_BienBanHieuChuan == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public List<STD_DUT> getListIdstd(int? id)
        {
            if (id == null)
            {
                return new List<STD_DUT>();
            }

            return dbContext.STD_DUTs
                .Where(m => m.ID == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public List<STD_DUT> getList(int? ids)
        {
            var list = dbContext.STD_DUTs.Where(m => m.Status == 1 && m.Id_BienBanHieuChuan == ids).AsQueryable();
            return list.ToList();
        }
        public int Insert(STD_DUT STD_DUT)
        {
            dbContext.STD_DUTs.Add(STD_DUT);
            return dbContext.SaveChanges();
        }
    }
}
