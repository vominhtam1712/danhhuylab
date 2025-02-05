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
    public class PhanCongGhiChuDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<PhanCongGhiChu> getListId(int? phanCongId)
        {
            return dbContext.PhanCongGhiChus
                .AsNoTracking()
                .Where(b => b.Id_PhanCong == phanCongId)
                .ToList();
        }
        public List<Join_PhanCongGhiChu> getlistJoin(int? id)
        {  
            var query = from pcgc in dbContext.PhanCongGhiChus.AsNoTracking()
                        join pc in dbContext.PhanCongs.AsNoTracking() on pcgc.Id_PhanCong equals pc.Id
                        join pychc in dbContext.Phieuyeucauhieuchuans.AsNoTracking() on pc.Id_pychc equals pychc.Id
                        join pntb in dbContext.PhieuNhanThietBis.AsNoTracking() on pychc.Id_phieunhanthietbi equals pntb.Id
                        join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                        where pcgc.Id_PhanCong == id
                        select new Join_PhanCongGhiChu
                        {
                            Id_PhanCongGhiChu=pcgc.Id,
                            Id_PhanCong=pc.Id,
                            GhiChu_PhanCongGhiChu=pcgc.GhiChu,
                            Ngaytao=pcgc.NgayTao,
                            TenKH=kh.TenKH,
                            Model=lnb.Model,
                            SN=lnb.SerialNumber,
                            Hang=lnb.Hang,
                            TenTB=lnb.Tenthietbi,
                            SoNhanDang=pntb.SoNhanDang,
                        };  
            return query.ToList();
        }
        public bool CheckExists(int? id_phancong)
        { 
            return dbContext.PhanCongGhiChus.Any(m => m.Id_PhanCong == id_phancong);
        }
        public IEnumerable<PhanCongGhiChu> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.PhanCongGhiChus
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public int Insert(PhanCongGhiChu company)
        {
            dbContext.PhanCongGhiChus.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(PhanCongGhiChu company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(PhanCongGhiChu company)
        {
            dbContext.PhanCongGhiChus.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
