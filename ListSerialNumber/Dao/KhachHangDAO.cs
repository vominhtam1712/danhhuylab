using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class KhachHangDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public string Layphieutudong()
        {
            var lastCode = dbContext.KhachHangs
                .Where(m => m.MaKH.StartsWith("KH"))
                .Select(m => m.MaKH)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "KH0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(2);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"KH{number:D4}";
                }
                else
                {
                    nextcode = $"KH{number}";
                }
            }
            return nextcode;
        }
        public KhachHang GetById(int? id)
        {
            return dbContext.KhachHangs
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public int getCountIndex()
        {
            return dbContext.KhachHangs.Count(m => m.Status == 1);            
        }
        public int getCountTrash()
        {
            return dbContext.KhachHangs.Count(m => m.Status != 1);
        }
        public IEnumerable<KhachHang> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.KhachHangs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public PagedList.IPagedList<Join_KhachHang_TapDoan_KhuVuc> getList(string Name, string Tenkhuvuc, string Tentapdoan, int pageSize, int pageNumber)
        {
            var query = from kh in dbContext.KhachHangs.Where(m => m.Status == 1)
                        join td in dbContext.TapDoans on kh.Id_TapDoan equals td.Id into tdJoin
                        from td in tdJoin.DefaultIfEmpty()
                        join kv in dbContext.KhuVucs on kh.Id_KhuVuc equals kv.Id into kvJoin
                        from kv in kvJoin.DefaultIfEmpty()
                        select new Join_KhachHang_TapDoan_KhuVuc
                        {
                            Id = kh.Id,
                            MaKH = kh.MaKH,
                            TenKH = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            Email = kh.Email,
                            SDT = kh.SDT,
                            LienHe = kh.LienHe,
                            Ghichu = kh.Ghichu,
                            TenKhuVuc = kv != null ? kv.Tenkhuvuc : null,
                            TenTapDoan = td != null ? td.Tentapdoan : null
                        };
             
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m =>
                    m.MaKH.ToLower().Contains(Name) ||
                    m.TenTapDoan.ToLower().Contains(Name) ||
                    m.TenKH.ToLower().Contains(Name) ||
                    m.DiaChi.ToLower().Contains(Name) ||
                    m.Email.ToLower().Contains(Name) ||
                    m.SDT.ToLower().Contains(Name) ||
                    m.Ghichu.ToLower().Contains(Name) ||
                    m.TenKhuVuc.ToLower().Contains(Name) ||
                    m.LienHe.ToLower().Contains(Name));
            }
             
            if (!string.IsNullOrEmpty(Tenkhuvuc))
            {
                query = query.Where(m => m.TenKhuVuc == Tenkhuvuc);
            }
             
            if (!string.IsNullOrEmpty(Tentapdoan))
            {
                query = query.Where(m => m.TenTapDoan == Tentapdoan);
            }
             
            var resultQuery = query.AsNoTracking()
                                   .OrderByDescending(m => m.Id)
                                   .ToPagedList(pageNumber, pageSize);
            return resultQuery;
        } 
        public bool CheckExists(string tenhk)
        {
            if (string.IsNullOrWhiteSpace(tenhk))
            {
                return false;
            }
            return dbContext.KhachHangs.Any(m => m.TenKH == tenhk);
        }
        public List<KhachHang> getListAll()
        {
            return dbContext.KhachHangs.Where(m => m.Status == 1).ToList();
        }
        public List<KhachHang> getListOne(int? id)
        {
            return dbContext.KhachHangs.Where(m => m.Id == id && m.Status == 1).ToList();
        }

        public List<KhachHang> getList(string status = "All")
        {
            List<KhachHang> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.KhachHangs.Where(m => m.Status != 0).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.KhachHangs.ToList();
                        break;
                    }
            }
            return list;
        }
        public KhachHang getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.KhachHangs.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public List<KhachHang> getListId(int? id)
        {
            if (id == null)
            {
                return new List<KhachHang>();
            }

            return dbContext.KhachHangs
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public int Insert(KhachHang ListNumber)
        {
            dbContext.KhachHangs.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(KhachHang ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(KhachHang ListNumber)
        {
            dbContext.KhachHangs.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
