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
    public class Join_PhanCong_NhanVien_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int GetCountIndex()
        {
            return dbContext.PhanCongs.Count();
        }
        public int GetCountIndex1()
        {
            return dbContext.PhanCongs.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.PhanCongs.Count(m=>m.Status!=1);
        }
        public PagedList.IPagedList<Join_PhanCong_NhanVien> getlistjoin(string Name, int pageSize, int pageNumber, int? companyId = null)
        { 

            var query = from c in dbContext.Companys
                        join p in dbContext.PhanCongs on c.Id equals p.Id_NV
                        join pcgc in dbContext.PhanCongGhiChus on p.Id equals pcgc.Id_PhanCong into pcgcJoin
                        from pcgc in pcgcJoin.DefaultIfEmpty()
                        join pychc in dbContext.Phieuyeucauhieuchuans on p.Id_pychc equals pychc.Id
                        join pntb in dbContext.PhieuNhanThietBis on pychc.Id_phieunhanthietbi equals pntb.Id
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs on lnb.Id_KhachHang equals kh.Id
                        select new Join_PhanCong_NhanVien
                        {
                            Id = p.Id,
                            Id_NV = c.Id,
                            Name = c.Name,
                            MaPC = p.MaPC,
                            Id_pychc = p.Id_pychc,
                            TenThietBi = lnb.Tenthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            TenCty = kh.TenKH,
                            TenCty_XuatHD = pychc.Tencognty,
                            Model = lnb.Model,
                            SerialNumber = lnb.SerialNumber,
                            MaPhieu = pychc.MaPhieu,
                            NgayTao = p.NgayTao,
                            NguoiTao = p.NguoiTao,
                            Status = p.Status,
                            GhiChu = pcgc != null ? pcgc.GhiChu : null
                        };

            if (companyId.HasValue)
            {
                query = query.Where(m => m.Id_NV == companyId.Value);
            }

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m =>
                    m.MaPC.ToLower().Contains(Name) ||
                    m.SoNhanDang.ToLower().Contains(Name) ||
                    m.TenThietBi.ToLower().Contains(Name) ||
                    m.Model.ToLower().Contains(Name) ||
                    m.SerialNumber.ToLower().Contains(Name) ||
                    m.GhiChu.ToLower().Contains(Name) ||
                    m.NguoiTao.ToLower().Contains(Name) || 
                    m.Name.ToLower().Contains(Name));
            }

            var resultQuery = query.AsNoTracking()
                                   .OrderByDescending(m => m.NgayTao) 
                                   .ToList(); 

            var groupedResult = resultQuery
                .GroupBy(m => m.MaPC)
                .Select(g => g.FirstOrDefault())  
                .ToList();

            return groupedResult.ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<Join_PhanCong_NhanVien> getlistIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerCaseName = Name?.ToLower();

            var query = from c in dbContext.Companys
                        join p in dbContext.PhanCongs on c.Id equals p.Id_NV 
                        join pychc in dbContext.Phieuyeucauhieuchuans on p.Id_pychc equals pychc.Id
                        join pntb in dbContext.PhieuNhanThietBis on pychc.Id_phieunhanthietbi equals pntb.Id
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs on lnb.Id_KhachHang equals kh.Id
                        select new Join_PhanCong_NhanVien
                        {
                            Id = p.Id,
                            Id_NV = c.Id,
                            Name = c.Name,
                            MaPC = p.MaPC,
                            Id_pychc = p.Id_pychc,
                            TenThietBi = lnb.Tenthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            TenCty = kh.TenKH,
                            TenCty_XuatHD = pychc.Tencognty,
                            Model = lnb.Model,
                            SerialNumber = lnb.SerialNumber,
                            MaPhieu = pychc.MaPhieu,
                            NgayTao = p.NgayTao,
                            NguoiTao = p.NguoiTao,
                            Status = p.Status, 
                        };

            query = query.Where(m => m.Status == 1);

            if (!string.IsNullOrEmpty(lowerCaseName))
            {
                query = query.Where(m =>
                    m.MaPC.ToLower().Contains(lowerCaseName) ||
                    m.SoNhanDang.ToLower().Contains(lowerCaseName) ||
                    m.TenThietBi.ToLower().Contains(lowerCaseName) ||
                    m.Model.ToLower().Contains(lowerCaseName) ||
                    m.SerialNumber.ToLower().Contains(lowerCaseName) ||
                    m.Name.ToLower().Contains(lowerCaseName));
            }

            var resultQuery = query.AsNoTracking()
                                   .OrderByDescending(m => m.NgayTao) 
                                   .GroupBy(m => m.MaPC)
                                   .Select(g => g.FirstOrDefault()) 
                                   .ToList(); 

            return resultQuery.ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<Join_PhanCong_NhanVien> getlistLuuInHoSo(string name, int pageSize, int pageNumber)
        {
            var lowerCaseName = name?.ToLower();

            var query = from c in dbContext.Companys
                        join p in dbContext.PhanCongs on c.Id equals p.Id_NV
                        join pychc in dbContext.Phieuyeucauhieuchuans on p.Id_pychc equals pychc.Id
                        join pntb in dbContext.PhieuNhanThietBis on pychc.Id_phieunhanthietbi equals pntb.Id
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs on lnb.Id_KhachHang equals kh.Id
                        select new Join_PhanCong_NhanVien
                        {
                            Id = p.Id,
                            Id_NV = c.Id,
                            Name = c.Name,
                            MaPC = p.MaPC,
                            Id_pychc = p.Id_pychc,
                            TenThietBi = lnb.Tenthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            TenCty = kh.TenKH,
                            TenCty_XuatHD = pychc.Tencognty,
                            Model = lnb.Model,
                            SerialNumber = lnb.SerialNumber,
                            MaPhieu = pychc.MaPhieu,
                            NgayTao = p.NgayTao,
                            NguoiTao = p.NguoiTao,
                            Status = p.Status
                        };

            if (!string.IsNullOrEmpty(lowerCaseName))
            {
                query = query.Where(m =>
                    m.MaPC.ToLower().Contains(lowerCaseName) ||
                    m.SoNhanDang.ToLower().Contains(lowerCaseName) ||
                    m.TenThietBi.ToLower().Contains(lowerCaseName) ||
                    m.Model.ToLower().Contains(lowerCaseName) ||
                    m.SerialNumber.ToLower().Contains(lowerCaseName) ||
                    m.Name.ToLower().Contains(lowerCaseName));
            }
            var resultQuery = query.AsNoTracking()
                                   .GroupBy(m => m.MaPC)
                                   .Select(g => g.OrderByDescending(m => m.NgayTao).FirstOrDefault())
                                   .OrderByDescending(m => m.Id)  
                                   .ToList(); 

            return resultQuery.ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<Join_PhanCong_NhanVien> getlistTrash(string Name, int pageSize, int pageNumber)
        {
            var lowerCaseName = Name?.ToLower();

            var query = from c in dbContext.Companys
                        join p in dbContext.PhanCongs on c.Id equals p.Id_NV
                        join pychc in dbContext.Phieuyeucauhieuchuans on p.Id_pychc equals pychc.Id
                        join pntb in dbContext.PhieuNhanThietBis on pychc.Id_phieunhanthietbi equals pntb.Id
                        join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs on lnb.Id_KhachHang equals kh.Id
                        select new Join_PhanCong_NhanVien
                        {
                            Id = p.Id,
                            Id_NV = c.Id,
                            Name = c.Name,
                            MaPC = p.MaPC,
                            Id_pychc = p.Id_pychc,
                            TenThietBi = lnb.Tenthietbi,
                            SoNhanDang = pntb.SoNhanDang,
                            TenCty = kh.TenKH,
                            TenCty_XuatHD = pychc.Tencognty,
                            Model = lnb.Model,
                            SerialNumber = lnb.SerialNumber,
                            MaPhieu = pychc.MaPhieu,
                            NgayTao = p.NgayTao,
                            NguoiTao = p.NguoiTao,
                            Status = p.Status
                        };

            query = query.Where(m => m.Status != 1);

            if (!string.IsNullOrEmpty(lowerCaseName))
            {
                query = query.Where(m =>
                    m.MaPC.ToLower().Contains(lowerCaseName) ||
                    m.SoNhanDang.ToLower().Contains(lowerCaseName) ||
                    m.TenThietBi.ToLower().Contains(lowerCaseName) ||
                    m.Model.ToLower().Contains(lowerCaseName) ||
                    m.SerialNumber.ToLower().Contains(lowerCaseName) ||
                    m.Name.ToLower().Contains(lowerCaseName));
            }

            var resultQuery = query.AsNoTracking()
                                   .GroupBy(m => m.MaPC)
                                   .Select(g => g.OrderByDescending(m => m.NgayTao).FirstOrDefault())
                                   .OrderByDescending(m => m.Id) 
                                   .ToList(); 

            return resultQuery.ToPagedList(pageNumber, pageSize);
        }

    }

}
