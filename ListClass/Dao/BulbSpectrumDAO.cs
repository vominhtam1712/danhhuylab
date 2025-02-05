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
    public class BulbSpectrumDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public BulbSpectrum GetById(int id)
        {
            return dbContext.BulbSpectrums
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public bool CheckExists(string bulbSpectrum)
        {
            return dbContext.BulbSpectrums.Any(m => m.Bulb_Spectrum == bulbSpectrum);
        }
        public bool Check1(int id)
        {
            return dbContext.NgayTaoTechnicialBuldTruocHieuChinhs.Any(m => m.Id_BulbSpectrum == id);
        }
        public bool Check2(int id)
        {
            return dbContext.NgayTaoTechnicialBuldKetQuaHieuChinhs.Any(m => m.Id_BulbSpectrum == id);
        }
        public IEnumerable<BulbSpectrum> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.BulbSpectrums
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public PagedList.IPagedList<BulbSpectrum> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.BulbSpectrums.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Bulb_Spectrum.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public List<BulbSpectrum> getList(string status = "All")
        {
            List<BulbSpectrum> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.BulbSpectrums.OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.BulbSpectrums.ToList();
                        break;
                    }
            }
            return list;
        }
        public int Insert(BulbSpectrum ListNumber)
        {
            dbContext.BulbSpectrums.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(BulbSpectrum ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(BulbSpectrum ListNumber)
        {
            dbContext.BulbSpectrums.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
