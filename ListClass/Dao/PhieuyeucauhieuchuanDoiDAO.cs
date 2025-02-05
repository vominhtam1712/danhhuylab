using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListClass.Dao
{
    public class PhieuyeucauhieuchuanDoiDAO
    {
        ListDBContext dbContext = new ListDBContext();

        public int GetCountIndex()
        {
            return dbContext.PhieuyeucauhieuchuanDois.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.PhieuyeucauhieuchuanDois.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<Join_PYCHCDOI_PNTB_LISTNUMERBAK> GetPagedList(string Name, int pageSize, int pageNumber)
        {

            var query = from pntb in dbContext.PhieuNhanThietBis
                        join pychcdoi in dbContext.PhieuyeucauhieuchuanDois on pntb.Id equals pychcdoi.Id_phieunhanthietbi where pychcdoi.Status ==1
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id 
                        select new Join_PYCHCDOI_PNTB_LISTNUMERBAK
                        {
                            Id = pychcdoi.Id,
                            MaPhieu = pychcdoi.MaPhieu,
                            Id_phieunhanthietbi = pychcdoi.Id_phieunhanthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            SerialNumber = lnb.SerialNumber,
                            TenThietBi = lnb.Tenthietbi,
                            Hang = lnb.Hang,
                            Model = lnb.Model,
                            NgaytaoSanPham = lnb.NgaytaoSanPham,
                            Status = pychcdoi.Status
                        }; 
            if (!string.IsNullOrEmpty(Name))
            {
                Name = Name.ToLower();
                query = query.Where(m => m.MaPhieu.Contains(Name)
                                    || m.SoNhanDang.Contains(Name)
                                    || m.SerialNumber.Contains(Name)
                                    || m.Hang.Contains(Name)
                                    || m.Model.Contains(Name)
                                    || m.TenThietBi.Contains(Name));
            }

            var list = query
                .OrderByDescending(m => m.Id)
                .ToPagedList(pageNumber, pageSize);

            return list;
        }
        public PagedList.IPagedList<Join_PYCHCDOI_PNTB_LISTNUMERBAK> GetPagedListTrash(string Name, int pageSize, int pageNumber)
        {

            var query = from pntb in dbContext.PhieuNhanThietBis
                        join pychcdoi in dbContext.PhieuyeucauhieuchuanDois on pntb.Id equals pychcdoi.Id_phieunhanthietbi
                        where pychcdoi.Status != 1
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                        select new Join_PYCHCDOI_PNTB_LISTNUMERBAK
                        {
                            Id = pychcdoi.Id,
                            MaPhieu = pychcdoi.MaPhieu,
                            Id_phieunhanthietbi = pychcdoi.Id_phieunhanthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            SerialNumber = lnb.SerialNumber,
                            TenThietBi = lnb.Tenthietbi,
                            Hang = lnb.Hang,
                            Model = lnb.Model,
                            NgaytaoSanPham = lnb.NgaytaoSanPham,
                            Status = pychcdoi.Status
                        };
            if (!string.IsNullOrEmpty(Name))
            {
                Name = Name.ToLower();
                query = query.Where(m => m.MaPhieu.Contains(Name)
                                    || m.SoNhanDang.Contains(Name)
                                    || m.SerialNumber.Contains(Name)
                                    || m.Hang.Contains(Name)
                                    || m.Model.Contains(Name)
                                    || m.TenThietBi.Contains(Name));
            }

            var list = query
                .OrderByDescending(m => m.Id)
                .ToPagedList(pageNumber, pageSize);

            return list;
        }
        public PagedList.IPagedList<Join_PYCHCDOI_PNTB_LISTNUMERBAK> GetPagedListDelete(string Name, int pageSize, int pageNumber)
        {

            var query = from pntb in dbContext.PhieuNhanThietBis
                        join pychcdoi in dbContext.PhieuyeucauhieuchuanDois on pntb.Id equals pychcdoi.Id_phieunhanthietbi
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                        select new Join_PYCHCDOI_PNTB_LISTNUMERBAK
                        {
                            Id = pychcdoi.Id,
                            MaPhieu = pychcdoi.MaPhieu,
                            Id_phieunhanthietbi = pychcdoi.Id_phieunhanthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            SerialNumber = lnb.SerialNumber,
                            TenThietBi = lnb.Tenthietbi,
                            Hang = lnb.Hang,
                            Model = lnb.Model,
                            NgaytaoSanPham = lnb.NgaytaoSanPham,
                            Status = pychcdoi.Status
                        };
            if (!string.IsNullOrEmpty(Name))
            {
                Name = Name.ToLower();
                query = query.Where(m => m.MaPhieu.Contains(Name)
                                    || m.SoNhanDang.Contains(Name)
                                    || m.SerialNumber.Contains(Name)
                                    || m.Hang.Contains(Name)
                                    || m.Model.Contains(Name)
                                    || m.TenThietBi.Contains(Name));
            }

            var list = query
                .OrderByDescending(m => m.Id)
                .ToPagedList(pageNumber, pageSize);

            return list;
        }
        public IEnumerable<PhieuyeucauhieuchuanDoi> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.PhieuyeucauhieuchuanDois
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public int Insert(PhieuyeucauhieuchuanDoi phieuyeucauhieuchuan)
        {
            dbContext.PhieuyeucauhieuchuanDois.Add(phieuyeucauhieuchuan);
            return dbContext.SaveChanges();
        }
        public int Update(PhieuyeucauhieuchuanDoi phieuyeucauhieuchuan)
        {
            dbContext.Entry(phieuyeucauhieuchuan).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(PhieuyeucauhieuchuanDoi phieuyeucauhieuchuan)
        {
            dbContext.PhieuyeucauhieuchuanDois.Remove(phieuyeucauhieuchuan);
            return dbContext.SaveChanges();
        }

    }

}
