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
    public class PhanCongDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public bool CheckSerialNumberExists(int? Id_pychc)
        {
            if (Id_pychc == null)
            {
                return false;
            }

            return dbContext.PhanCongs
                .Any(m => m.Id_pychc == Id_pychc.Value && m.Status == 1);
        }

        public List<PhanCong> getListId(int? id)
        {
            if (id == null)
            {
                return new List<PhanCong>();
            }

            return dbContext.PhanCongs
                .Where(m => m.Id == id.Value)
                .AsNoTracking()   
                .ToList();
        }
        public List<PhanCong> getListIdphancong(int? id)
        {
            if (id == null)
            {
                return new List<PhanCong>();
            }

            return dbContext.PhanCongs
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }

        public PagedList.IPagedList<PhanCong> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.PhanCongs.Where(m => m.Status == 1).AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                var lowerName = Name.ToLower();
                list = list.Where(m => m.MaPC.ToLower().Contains(lowerName));
            }

            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }

        public IEnumerable<PhanCong> GetRowsByIds(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<PhanCong>();
            }

            return dbContext.PhanCongs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.PhanCongs
                .Where(m => m.MaPC.StartsWith("MPC"))
                .Select(m => m.MaPC)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "MPC0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"MPC{number:D4}";
                }
                else
                {
                    nextcode = $"MPC{number}";
                }
            }
            return nextcode;
        }
        public PhanCong getRow(int? id)
        {
            return id == null ? null : dbContext.PhanCongs
                .AsNoTracking() 
                .FirstOrDefault(m => m.Id == id);
        }
         
        public int Insert(PhanCong company)
        {
            dbContext.PhanCongs.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(PhanCong company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(PhanCong company)
        {
            dbContext.PhanCongs.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
