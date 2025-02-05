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
    public class KhuVucDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int getCountIndex()
        {
            return dbContext.KhuVucs.Count(m => m.Status == 1);
        }
        public int getCountTrash()
        {
            return dbContext.KhuVucs.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<KhuVuc> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.KhuVucs.Where(m => m.Status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Tenkhuvuc.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        } 
        public IEnumerable<KhuVuc> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.KhuVucs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public KhuVuc GetById(int id)
        {
            return dbContext.KhuVucs
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public bool CheckExists(List<KhuVuc> ids)
        {
            var idList = ids.Select(t => t.Id).ToList();

            return dbContext.KhachHangs.Any(m => idList.Contains(m.Id_KhuVuc.Value));
        }
        public List<string> GetKhuVucNamesWithKhachHang(List<KhuVuc> ids)
        {
            var idList = ids.Select(t => t.Id).ToList();

            var tapDoanNames = dbContext.KhuVucs
                                        .Where(m => idList.Contains(m.Id))
                                        .Select(m => m.Tenkhuvuc)
                                        .Distinct()
                                        .ToList();

            return tapDoanNames;
        }
        public List<KhuVuc> getList(string status = "All")
        {
            List<KhuVuc> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.KhuVucs.Where(m => m.Status != 0).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.KhuVucs.ToList();
                        break;
                    }
            }
            return list;
        }
        public KhuVuc getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.KhuVucs.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public List<KhuVuc> getListId(int? id)
        {
            if (id == null)
            {
                return new List<KhuVuc>();
            }

            return dbContext.KhuVucs
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public int Insert(KhuVuc ListNumber)
        {
            dbContext.KhuVucs.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(KhuVuc ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(KhuVuc ListNumber)
        {
            dbContext.KhuVucs.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
