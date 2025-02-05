using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class LampSystemsDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public LampSystem GetById(int id)
        {
            return dbContext.LampSystems
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public bool CheckExists(string lampSystems)
        { 
            return dbContext.LampSystems.Any(m => m.Lamp_Systems == lampSystems);
        }
        public bool Check1(int id)
        {
            return dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.Any(m => m.Id_LampSystems == id);
        }
        public bool Check2(int id)
        {
            return dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.Any(m => m.Id_LampSystems == id);
        }
        public IEnumerable<LampSystem> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.LampSystems
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public PagedList.IPagedList<LampSystem> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.LampSystems.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Lamp_Systems.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public List<LampSystem> getList(string status = "All")
        {
            List<LampSystem> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.LampSystems.OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.LampSystems.ToList();
                        break;
                    }
            }
            return list;
        }
        public int Insert(LampSystem ListNumber)
        {
            dbContext.LampSystems.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(LampSystem ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(LampSystem ListNumber)
        {
            dbContext.LampSystems.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
