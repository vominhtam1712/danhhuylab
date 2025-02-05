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
    public class TapDoanDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int getCountIndex()
        {
            return dbContext.TapDoans.Count(m => m.Status == 1);
        }
        public int getCountTrash()
        {
            return dbContext.TapDoans.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<TapDoan> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.TapDoans.Where(m => m.Status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Tentapdoan.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<TapDoan> getTrash(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.TapDoans.Where(m => m.Status != 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Tentapdoan.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable<TapDoan> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.TapDoans
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public bool CheckExists(List<TapDoan> ids)
        {
            var idList = ids.Select(t => t.Id).ToList();  

            return dbContext.KhachHangs.Any(m => idList.Contains(m.Id_TapDoan.Value));
        }
        public List<string> GetTapDoanNamesWithKhachHang(List<TapDoan> ids)
        {
            var idList = ids.Select(t => t.Id).ToList();

            var tapDoanNames = dbContext.TapDoans
                                        .Where(m => idList.Contains(m.Id))
                                        .Select(m => m.Tentapdoan)
                                        .Distinct()
                                        .ToList();

            return tapDoanNames;
        }
        public TapDoan GetById(int id)
        {
            return dbContext.TapDoans
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public List<TapDoan> getList(string status = "All")
        {
            List<TapDoan> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.TapDoans.Where(m => m.Status != 0).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.TapDoans.ToList();
                        break;
                    }
            }
            return list;
        }
        public TapDoan getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.TapDoans.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public List<TapDoan> getListId(int? id)
        {
            if (id == null)
            {
                return new List<TapDoan>();
            }

            return dbContext.TapDoans
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public int Insert(TapDoan ListNumber)
        {
            dbContext.TapDoans.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(TapDoan ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(TapDoan ListNumber)
        {
            dbContext.TapDoans.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
