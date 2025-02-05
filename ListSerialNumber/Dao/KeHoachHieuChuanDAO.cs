using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class KeHoachHieuChuanDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public bool CheckSerialNumberExists(int? Id_DanhMucThietBi, int id)
        {
            return Id_DanhMucThietBi.HasValue &&
                   dbContext.KeHoachHieuChuans
                            .Any(m => m.Id_DanhMucThietBi == Id_DanhMucThietBi && m.Id_KeHoachHieuChuan_Form == id);
        }
        public List<KeHoachHieuChuan> getList(string status = "All")
        {
            IQueryable<KeHoachHieuChuan> query = dbContext.KeHoachHieuChuans.AsQueryable();

            if (status == "Index")
            {
                query = query.Where(m => m.Status == 1);
            }

            return query.OrderByDescending(m => m.Id)
                        .AsNoTracking()  
                        .ToList();
        }
        public IEnumerable<KeHoachHieuChuan> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.KeHoachHieuChuans
                            .AsNoTracking()  
                            .Where(ln => ids.Contains(ln.Id))
                            .ToList(); 
        }
        public KeHoachHieuChuan getRow(int? id)
        {
            return id == null ? null : dbContext.KeHoachHieuChuans
                                               .AsNoTracking()  
                                               .FirstOrDefault(m => m.Id == id);
        }
        public int Insert(KeHoachHieuChuan ListNumber)
        {
            dbContext.KeHoachHieuChuans.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(KeHoachHieuChuan bienbanhieuchuans)
        {
            dbContext.Entry(bienbanhieuchuans).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(KeHoachHieuChuan ListNumber)
        {
            dbContext.KeHoachHieuChuans.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
