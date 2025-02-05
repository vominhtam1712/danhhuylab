using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class KeHoachHieuChuan_Form_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<KeHoachHieuChuan_Form> getListId(int? id)
        {
            return dbContext.KeHoachHieuChuan_Forms
                            .AsNoTracking()
                            .Where(m => m.Id == id)
                            .ToList();
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.KeHoachHieuChuan_Forms
                                     .Where(m => m.MaPhieu.StartsWith("KHHC"))
                                     .OrderByDescending(m => m.MaPhieu)
                                     .FirstOrDefault();

            string nextcode = "KHHC0001";
            if (lastCode != null)
            {
                var currentCode = lastCode.MaPhieu;
                var numberPart = currentCode.Substring(4);
                var number = int.Parse(numberPart) + 1;

                if (number > 9999)
                {
                    number += 10000;
                }

                nextcode = $"KHHC{number}";
            }
            return nextcode;
        }
        public int GetCountList(int? id)
        {
            return dbContext.KeHoachHieuChuan_Forms.Count(m => m.Status == 1 && m.Id == id);
        }

        public int GetCountIndex()
        {
            return dbContext.KeHoachHieuChuan_Forms.Count(m => m.Status == 1);
        }

        public int GetCountTrash()
        {
            return dbContext.KeHoachHieuChuan_Forms.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<KeHoachHieuChuan_Form> getListIndex(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.KeHoachHieuChuan_Forms.Where(m => m.Status == 1).AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                Name = Name.ToLower();  
                query = query.Where(m => m.MaPhieu.ToLower().Contains(Name) || m.NguoiTao.ToLower().Contains(Name));
            }

            return query.OrderByDescending(m => m.Id)
                        .AsNoTracking()  
                        .ToPagedList(pageNumber, pageSize);
        }

        public PagedList.IPagedList<KeHoachHieuChuan_Form> getListTrash(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.KeHoachHieuChuan_Forms.Where(m => m.Status != 1).AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                Name = Name.ToLower();
                query = query.Where(m => m.MaPhieu.ToLower().Contains(Name) || m.NguoiTao.ToLower().Contains(Name));
            }

            return query.OrderByDescending(m => m.Id)
                        .AsNoTracking()  
                        .ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable<KeHoachHieuChuan_Form> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.KeHoachHieuChuan_Forms
                            .AsNoTracking()  
                            .Where(ln => ids.Contains(ln.Id))
                            .ToList(); 
        }
        public List<KeHoachHieuChuan_Form> getList(string status = "All")
        {
            IQueryable<KeHoachHieuChuan_Form> query = dbContext.KeHoachHieuChuan_Forms.AsQueryable();

            if (status == "Index")
            {
                query = query.Where(m => m.Status != 0);
            }

            return query.OrderByDescending(m => m.Id)
                        .AsNoTracking()
                        .ToList();
        }
        public KeHoachHieuChuan_Form getRow(int? id)
        {
            return id == null ? null : dbContext.KeHoachHieuChuan_Forms
                                               .AsNoTracking() 
                                               .FirstOrDefault(m => m.Id == id);
        }
        public int Insert(KeHoachHieuChuan_Form company)
        {
            dbContext.KeHoachHieuChuan_Forms.Add(company);
            return dbContext.SaveChanges();
        }

        public int Update(KeHoachHieuChuan_Form company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Delete(KeHoachHieuChuan_Form company)
        {
            dbContext.KeHoachHieuChuan_Forms.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
