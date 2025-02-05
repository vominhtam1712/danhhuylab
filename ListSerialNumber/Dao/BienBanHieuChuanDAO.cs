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
    public class BienBanHieuChuanDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public string Layphieutudong()
        {
            var lastCode = dbContext.BienBanHieuChuans
                .Where(m => m.MaBienBan.StartsWith("MBB"))
                .Select(m => m.MaBienBan)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "MBB0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"MBB{number:D4}";
                }
                else
                {
                    nextcode = $"MBB{number}";
                }
            }
            return nextcode;
        } 
        public List<BienBanHieuChuan> getListId(int? phanCongId)
        {
            return dbContext.BienBanHieuChuans
                .Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }
        public List<BienBanHieuChuan> getListIdbienban(int? bienbanId)
        {
            return dbContext.BienBanHieuChuans
                .AsNoTracking()
                .Where(b => b.Id == bienbanId)
                .ToList();
        }
        public BienBanHieuChuan GetById(int id)
        {
            return dbContext.BienBanHieuChuans
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }

        public IEnumerable<BienBanHieuChuan> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.BienBanHieuChuans
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public PagedList.IPagedList<BienBanHieuChuan> getList(int? id, string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.BienBanHieuChuans
                .AsNoTracking()
                .Where(m => m.Status == 1 && m.Id_PhanCong == id);

            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.MaBienBan.ToLower().Contains(Name.ToLower()));
            }

            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }

        public PagedList.IPagedList<BienBanHieuChuan> getListTrash(int id, string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.BienBanHieuChuans
                .AsNoTracking()
                .Where(m => m.Status != 1 && m.Id_PhanCong == id);

            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.MaBienBan.ToLower().Contains(Name.ToLower()));
            }

            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public bool CheckSerialNumberExists(int? Id_PhanCong)
        {
            return Id_PhanCong.HasValue && dbContext.BienBanHieuChuans
                .AsNoTracking()
                .Any(m => m.Id_PhanCong == Id_PhanCong);
        }
        public List<BienBanHieuChuan> getList(string status = "All")
        {
            List<BienBanHieuChuan> list;
            switch (status)
            {
                case "Index":
                    list = dbContext.BienBanHieuChuans
                        .AsNoTracking()
                        .Where(m => m.Status == 1)
                        .OrderByDescending(m => m.Id)
                        .ToList();
                    break;
                default:
                    list = dbContext.BienBanHieuChuans
                        .AsNoTracking()
                        .ToList();
                    break;
            }
            return list;
        }
        public BienBanHieuChuan getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.BienBanHieuChuans.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public int Insert(BienBanHieuChuan ListNumber)
        {
            dbContext.BienBanHieuChuans.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(BienBanHieuChuan bienbanhieuchuans)
        {
            dbContext.Entry(bienbanhieuchuans).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(BienBanHieuChuan ListNumber)
        {
            dbContext.BienBanHieuChuans.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
