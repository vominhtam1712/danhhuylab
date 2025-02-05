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
    public class BienBanBanGiaoNhanThietBiDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public IEnumerable<BienBanBanGiaoNhanThietBi> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.BienBanBanGiaoNhanThietBis
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public List<BienBanBanGiaoNhanThietBi> getListId(int? id)
        {
            return dbContext.BienBanBanGiaoNhanThietBis.Where(m => m.Id == id).ToList();
        }
        public List<BienBanBanGiaoNhanThietBi> getList(string status = "All")
        {
            List<BienBanBanGiaoNhanThietBi> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.BienBanBanGiaoNhanThietBis.Where(m => m.Status == 1).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.BienBanBanGiaoNhanThietBis.ToList();
                        break;
                    }
            }
            return list;
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.BienBanBanGiaoNhanThietBis.OrderByDescending(m => m.SoYeuCau).FirstOrDefault();
            string nextcode;
            if (lastCode == null)
            {
                nextcode = "YC0001";
            }
            else
            {
                var currentCode = lastCode.SoYeuCau;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;
                if (number > 9999)
                {
                    throw new Exception("Đã đạt tới giới hạn số phiếu.");
                }
                nextcode = $"YC{number:D4}";
            }
            return nextcode;
        }
        public int GetCountIndex()
        {
            return dbContext.BienBanBanGiaoNhanThietBis.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.BienBanBanGiaoNhanThietBis.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<BienBanBanGiaoNhanThietBi> getList(string Name,int pageSize, int pageNumber)
        {
            var list = dbContext.BienBanBanGiaoNhanThietBis.Where(m => m.Status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.SoYeuCau.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<BienBanBanGiaoNhanThietBi> getListTrash(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.BienBanBanGiaoNhanThietBis.Where(m => m.Status != 1).AsQueryable();
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public List<BienBanBanGiaoNhanThietBi> getListAll()
        {
            return dbContext.BienBanBanGiaoNhanThietBis.Where(m => m.Status == 1).ToList();
        }
        public BienBanBanGiaoNhanThietBi getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.BienBanBanGiaoNhanThietBis.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public int Insert(BienBanBanGiaoNhanThietBi BienBanBanGiaoNhanThietBi)
        {
            dbContext.BienBanBanGiaoNhanThietBis.Add(BienBanBanGiaoNhanThietBi);
            return dbContext.SaveChanges();
        }
        public int Update(BienBanBanGiaoNhanThietBi BienBanBanGiaoNhanThietBi)
        {
            dbContext.Entry(BienBanBanGiaoNhanThietBi).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(BienBanBanGiaoNhanThietBi BienBanBanGiaoNhanThietBi)
        {
            dbContext.BienBanBanGiaoNhanThietBis.Remove(BienBanBanGiaoNhanThietBi);
            return dbContext.SaveChanges();
        }
    }
}
