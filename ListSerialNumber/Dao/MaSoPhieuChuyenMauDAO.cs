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
    public class MaSoPhieuChuyenMauDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public IEnumerable<MaSoPhieuChuyenMau> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.MaSoPhieuChuyenMaus
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.MaSoPhieuChuyenMaus.OrderByDescending(m => m.MaSoMau).FirstOrDefault();
            string nextcode;
            if (lastCode == null)
            {
                nextcode = "MSM0001";
            }
            else
            {
                var currentCode = lastCode.MaSoMau;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;
                if (number > 9999)
                {
                    throw new Exception("Đã đạt tới giới hạn số phiếu.");
                }
                nextcode = $"MSM{number:D4}";
            }
            return nextcode;
        }
        public List<MaSoPhieuChuyenMau> getListId(int? Id)
        {
            return dbContext.MaSoPhieuChuyenMaus.Where(m => m.Id == Id).ToList();
        }
        public int GetCountIndex()
        {
            return dbContext.MaSoPhieuChuyenMaus.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.MaSoPhieuChuyenMaus.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<MaSoPhieuChuyenMau> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.MaSoPhieuChuyenMaus.Where(m => m.Status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.MaSoMau.ToLower().Contains(Name)
                || m.NguoiTao.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<MaSoPhieuChuyenMau> getListTrash(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.MaSoPhieuChuyenMaus.Where(m => m.Status != 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.MaSoMau.ToLower().Contains(Name)
                || m.NguoiTao.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public List<MaSoPhieuChuyenMau> getList(string status = "All")
        {
            List<MaSoPhieuChuyenMau> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.MaSoPhieuChuyenMaus.Where(m => m.Status == 1).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = dbContext.MaSoPhieuChuyenMaus.Where(m => m.Status != 1).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.MaSoPhieuChuyenMaus.ToList();
                        break;
                    }
            }
            return list;
        }
        public MaSoPhieuChuyenMau getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.MaSoPhieuChuyenMaus.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public int Insert(MaSoPhieuChuyenMau ListNumber)
        {
            dbContext.MaSoPhieuChuyenMaus.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(MaSoPhieuChuyenMau ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(MaSoPhieuChuyenMau ListNumber)
        {
            dbContext.MaSoPhieuChuyenMaus.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
