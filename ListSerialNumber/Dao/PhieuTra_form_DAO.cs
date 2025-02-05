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
    public class PhieuTra_form_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<PhieuTra_form> getListId(int? ids)
        {
            return dbContext.PhieuTra_forms.Where(m => m.Id == ids).ToList();
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.PhieuTra_forms.OrderByDescending(m => m.MaPhieuTra).FirstOrDefault();
            string nextcode;
            if (lastCode == null)
            {
                nextcode = "PT0001";
            }
            else
            {
                var currentCode = lastCode.MaPhieuTra;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;
                if (number > 9999)
                {
                    throw new Exception("Đã đạt tới giới hạn số phiếu.");
                }
                nextcode = $"PT{number:D4}";
            }
            return nextcode;
        }
        public PagedList.IPagedList<PhieuTra_form> getList(string Search, int pageSize, int pageNumber)
        {
            var list = dbContext.PhieuTra_forms.Where(m => m.Status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Search))
            {
                list = list.Where(m => m.MaPhieuTra.ToLower().Contains(Search));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }

        public int GetCountIndex()
        {
            return dbContext.PhieuTra_forms.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.PhieuTra_forms.Count(m => m.Status != 1);
        }
        public IEnumerable<PhieuTra_form> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.PhieuTra_forms
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public int Insert(PhieuTra_form ListNumber)
        {
            dbContext.PhieuTra_forms.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(PhieuTra_form ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(PhieuTra_form ListNumber)
        {
            dbContext.PhieuTra_forms.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
