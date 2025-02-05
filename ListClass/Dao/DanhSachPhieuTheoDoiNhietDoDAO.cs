using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class DanhSachPhieuTheoDoiNhietDoDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<DanhSachPhieuTheoDoiNhietDo> getList(int? id)
        {
            return dbContext.DanhSachPhieuTheoDoiNhietDos
                .AsNoTracking()
                .Where(m => m.status == 1 && m.ID_PhieuTheoDoiNhietDo == id)
                .ToList();
        }
        public int Insert(DanhSachPhieuTheoDoiNhietDo ListNumber)
        {
            dbContext.DanhSachPhieuTheoDoiNhietDos.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public IEnumerable<DanhSachPhieuTheoDoiNhietDo> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.DanhSachPhieuTheoDoiNhietDos
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.ID))
                .ToList();
        }
    }
}
