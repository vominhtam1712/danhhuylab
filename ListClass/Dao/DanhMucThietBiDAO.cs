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
    public class DanhMucThietBiDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public DanhMucThietBi GetById(int id)
        { 
            return dbContext.DanhMucThietBis
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }

        public PagedList.IPagedList<DanhMucThietBi> getList(string Name, int pageSize, int pageNumber)
        { 
            var list = dbContext.DanhMucThietBis.AsNoTracking().Where(m => m.Status == 1);
             
            if (!string.IsNullOrEmpty(Name))
            {
                var nameLower = Name.ToLower();
                list = list.Where(m => m.TenThietBi.ToLower().Contains(nameLower) ||
                                       m.MaThietBi.ToLower().Contains(nameLower) ||
                                       m.HangSX.ToLower().Contains(nameLower) ||
                                       m.Serial.ToLower().Contains(nameLower));
            }

            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        } 
        public IEnumerable<DanhMucThietBi> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.DanhMucThietBis
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }

        public int getCountIndex()
        {
            // Đếm số lượng các mục có Status = 1
            return dbContext.DanhMucThietBis.Count(m => m.Status == 1);
        }

        public int getCountTrash()
        { 
            return dbContext.DanhMucThietBis.Count(m => m.Status != 1);
        }

        public DanhMucThietBi getRow(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
             
            return dbContext.DanhMucThietBis.AsNoTracking().FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<DanhMucThietBi> getList()
        { 
            return dbContext.DanhMucThietBis.AsNoTracking().Where(m => m.Status == 1).ToList();
        }

        public List<DanhMucThietBi> getList(string status = "All")
        { 
            return status == "Index"
                ? dbContext.DanhMucThietBis.AsNoTracking().Where(m => m.Status != 0).OrderByDescending(m => m.Id).ToList()
                : dbContext.DanhMucThietBis.AsNoTracking().ToList();
        }

        public bool CheckSerialNumberExists(string serialNumber)
        { 
            return !string.IsNullOrWhiteSpace(serialNumber) &&
                   dbContext.DanhMucThietBis.Any(m => m.Serial == serialNumber && m.Status == 1);
        }

        public int Insert(DanhMucThietBi company)
        {
            dbContext.DanhMucThietBis.Add(company);
            return dbContext.SaveChanges();
        }

        public int Update(DanhMucThietBi company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Delete(DanhMucThietBi company)
        {
            dbContext.DanhMucThietBis.Remove(company);
            return dbContext.SaveChanges();
        }

    }
}
