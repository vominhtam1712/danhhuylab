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
    public class PhieuTheoDoiNhietDoDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public PagedList.IPagedList<PhieuTheoDoiNhietDo> getList(string Name, int? thang, int? nam, int pageSize, int pageNumber)
        {
            var query = dbContext.PhieuTheoDoiNhietDos.Where(m => m.status == 1);
             
            if (!string.IsNullOrEmpty(Name))
            {
                var lowerName = Name.ToLower();
                query = query.Where(m => m.MaPhieuTheoDoiNhietDo.Contains(lowerName) ||
                                          m.MSNhietKe_Model_No.Contains(lowerName));
            }
             
            if (thang.HasValue && nam.HasValue)
            {
                query = query.Where(m => m.Thang_Nam.Year == nam.Value && m.Thang_Nam.Month == thang.Value);
            }
            else if (nam.HasValue)
            {
                query = query.Where(m => m.Thang_Nam.Year == nam.Value);
            }
            else if (thang.HasValue)
            {
                query = query.Where(m => m.Thang_Nam.Month == thang.Value);
            }

            return query.OrderByDescending(m => m.Id)
                        .ToPagedList(pageNumber, pageSize);
        }

        public PagedList.IPagedList<PhieuTheoDoiNhietDo> getTrash(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.PhieuTheoDoiNhietDos.Where(m => m.status != 1);

            if (!string.IsNullOrEmpty(Name))
            {
                var lowerName = Name.ToLower(); 
                query = query.Where(m => m.MaPhieuTheoDoiNhietDo.Contains(lowerName));
            }

            return query.OrderByDescending(m => m.Id)
                        .ToPagedList(pageNumber, pageSize);
        }

        public List<PhieuTheoDoiNhietDo> getList(int? ids)
        { 
            return dbContext.PhieuTheoDoiNhietDos
                            .Where(m => m.status == 1 && m.Id == ids)
                            .ToList();  
        }

        public List<PhieuTheoDoiNhietDo> getListId(int? id)
        {
            if (id == null) return new List<PhieuTheoDoiNhietDo>();  
            return dbContext.PhieuTheoDoiNhietDos
                            .Where(m => m.Id == id)
                            .ToList();
        }
        public string Layphieutudong()
        { 
            var lastCode = dbContext.PhieuTheoDoiNhietDos
                                    .Where(m => m.MaPhieuTheoDoiNhietDo != null)
                                    .OrderByDescending(m => m.MaPhieuTheoDoiNhietDo)
                                    .FirstOrDefault();

            string nextcode;
            if (lastCode == null)
            {
                nextcode = "PTD0001";  
            }
            else
            { 
                var currentCode = lastCode.MaPhieuTheoDoiNhietDo;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart) + 1;  
                if (number > 9999)
                {
                    throw new Exception("Đã đạt tới giới hạn số phiếu.");
                }
                nextcode = $"PTD{number:D4}";  
            }
            return nextcode;
        }

        public int Insert(PhieuTheoDoiNhietDo ListNumber)
        {
            dbContext.PhieuTheoDoiNhietDos.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public IEnumerable<PhieuTheoDoiNhietDo> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.PhieuTheoDoiNhietDos
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
    }
}
