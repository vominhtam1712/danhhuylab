using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int GetCountIndex()
        {
            return dbContext.Phieuyeucauhieuchuans.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.Phieuyeucauhieuchuans.Count(m => m.Status != 1);
        }
        public List<Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK> getlistjoin(int? id)
        {
            if (id == null) return new List<Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK>(); 

            var list = from p in dbContext.PhieuNhanThietBis
                       join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                       join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                       join ph in dbContext.Phieuyeucauhieuchuans on p.Id equals ph.Id_phieunhanthietbi 
                       where ph.Status == 1 && ph.Id == id
                       select new Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK
                       {
                           Id_PhieuHieuChuan = ph.Id,
                           Id_phieunhanthietbi = ph.Id_phieunhanthietbi,
                           NgayThucHien = p.NgayThucHien,
                           KhachHang = kh.TenKH,
                           SoNhanDang = p.SoNhanDang,
                           DiaChi = kh.DiaChi,
                           MaPhieu = ph.MaPhieu,
                           Diachihieuchuan = ph.Diachihieuchuan,
                           LienHe = kh.LienHe,
                           Tencognty = ph.Tencognty,
                           Diachicongty = ph.Diachicongty,
                           Masothue = ph.Masothue,
                           Model = l.Model,
                           Img = l.Image,
                           SerialNumber = l.SerialNumber,
                           Soluong = ph.Soluong,
                           Dichvuyeucau = ph.Dichvuyeucau,
                           Danhhuy = ph.Danhhuy,
                           Nguoitao = ph.Createby,
                           Ngaytao_SanPham = l.NgaytaoSanPham, 
                           Ngaytao_PhieuNhanTB = p.Ngaytao,
                           NgayTao_PYC = ph.NgayTao,
                           Status = ph.Status
                       };

            var result = list.OrderByDescending(m => m.Id_PhieuHieuChuan) 
                             .GroupBy(m => m.Id_PhieuHieuChuan)
                             .Select(g => g.FirstOrDefault())  
                             .ToList();

            return result; 
        }
        public List<Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK> getlistjoin()
        { 
            var list = (from p in dbContext.PhieuNhanThietBis
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join ph in dbContext.Phieuyeucauhieuchuans on p.Id equals ph.Id_phieunhanthietbi 
                        where ph.Status == 1
                        orderby p.Ngaytao descending  
                        select new Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_PhieuHieuChuan = ph.Id,
                            Id_phieunhanthietbi = ph.Id_phieunhanthietbi,
                            NgayThucHien = p.NgayThucHien,
                            KhachHang = kh.TenKH,
                            SoNhanDang = p.SoNhanDang,
                            DiaChi = kh.DiaChi,
                            MaPhieu = ph.MaPhieu,
                            Diachihieuchuan = ph.Diachihieuchuan,
                            LienHe = kh.LienHe,
                            Tencognty = ph.Tencognty,
                            Diachicongty = ph.Diachicongty,
                            Masothue = ph.Masothue,
                            Model = l.Model,
                            Img = l.Image,
                            SerialNumber = l.SerialNumber,
                            Soluong = ph.Soluong,
                            Dichvuyeucau = ph.Dichvuyeucau,
                            Danhhuy = ph.Danhhuy,
                            Nguoitao = ph.Createby,
                            Ngaytao_SanPham = l.NgaytaoSanPham, 
                            Ngaytao_PhieuNhanTB = p.Ngaytao,
                            NgayTao_PYC = ph.NgayTao,
                            Status = ph.Status
                        })
                        .AsNoTracking() 
                        .ToList();
             
            var result = list.GroupBy(m => m.Id_PhieuHieuChuan)
                             .Select(g => g.FirstOrDefault()) 
                             .OrderByDescending(m => m.Id_PhieuHieuChuan) 
                             .ToList();

            return result;
        }
        public PagedList.IPagedList<Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK> getlistjoin(string name, int pageSize, int pageNumber)
        {
            var query = from p in dbContext.PhieuNhanThietBis
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join ph in dbContext.Phieuyeucauhieuchuans on p.Id equals ph.Id_phieunhanthietbi 
                        where ph.Status == 1
                        select new Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_PhieuHieuChuan = ph.Id,
                            Id_phieunhanthietbi = ph.Id_phieunhanthietbi,
                            NgayThucHien = p.NgayThucHien,
                            KhachHang = kh.TenKH,
                            SoNhanDang = p.SoNhanDang,
                            DiaChi = kh.DiaChi,
                            MaPhieu = ph.MaPhieu,
                            Diachihieuchuan = ph.Diachihieuchuan,
                            LienHe = kh.LienHe,
                            Tencognty = ph.Tencognty,
                            Diachicongty = ph.Diachicongty,
                            Masothue = ph.Masothue,
                            Model = l.Model,
                            Img = l.Image,
                            Tenthietbi = l.Tenthietbi,
                            SerialNumber = l.SerialNumber,
                            Soluong = ph.Soluong,
                            Dichvuyeucau = ph.Dichvuyeucau,
                            Danhhuy = ph.Danhhuy,
                            Nguoitao = ph.Createby,
                            Ngaytao_SanPham = l.NgaytaoSanPham, 
                            Ngaytao_PhieuNhanTB = p.Ngaytao,
                            NgayTao_PYC = ph.NgayTao,
                            Status = ph.Status
                        };
             
            if (!string.IsNullOrEmpty(name))
            {
                string lowerName = name.ToLower();
                query = query.Where(m => m.Diachihieuchuan.ToLower().Contains(lowerName)
                    || m.Tencognty.ToLower().Contains(lowerName)
                    || m.MaPhieu.ToLower().Contains(lowerName)
                    || m.SoNhanDang.ToLower().Contains(lowerName)
                    || m.Diachicongty.ToLower().Contains(lowerName)
                    || m.SerialNumber.ToLower().Contains(lowerName)
                    || m.Model.ToLower().Contains(lowerName)
                    || m.Masothue.ToLower().Contains(lowerName)
                    || m.Dichvuyeucau.ToLower().Contains(lowerName)
                    || m.Danhhuy.ToLower().Contains(lowerName));
            }
             
            var result = query
                        .OrderByDescending(m => m.Id_PhieuHieuChuan)  
                        .ToPagedList(pageNumber, pageSize);  

            return result;
        }
        public PagedList.IPagedList<Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK> getlistTrash(string name, int pageSize, int pageNumber)
        {
            var query = from p in dbContext.PhieuNhanThietBis
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join ph in dbContext.Phieuyeucauhieuchuans on p.Id equals ph.Id_phieunhanthietbi  
                        where ph.Status != 1
                        select new Join_PYCHCBAK_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_PhieuHieuChuan = ph.Id,
                            Id_phieunhanthietbi = ph.Id_phieunhanthietbi,
                            NgayThucHien = p.NgayThucHien,
                            KhachHang = kh.TenKH,
                            SoNhanDang = p.SoNhanDang,
                            DiaChi = kh.DiaChi,
                            MaPhieu = ph.MaPhieu,
                            Diachihieuchuan = ph.Diachihieuchuan,
                            LienHe = kh.LienHe,
                            Tencognty = ph.Tencognty,
                            Diachicongty = ph.Diachicongty,
                            Masothue = ph.Masothue,
                            Model = l.Model,
                            Img = l.Image,
                            Tenthietbi = l.Tenthietbi,
                            SerialNumber = l.SerialNumber,
                            Soluong = ph.Soluong,
                            Dichvuyeucau = ph.Dichvuyeucau,
                            Danhhuy = ph.Danhhuy,
                            Nguoitao = ph.Createby,
                            Ngaytao_SanPham = l.NgaytaoSanPham, 
                            Ngaytao_PhieuNhanTB = p.Ngaytao,
                            NgayTao_PYC = ph.NgayTao,
                            Status = ph.Status
                        };
             
            if (!string.IsNullOrEmpty(name))
            {
                string lowerName = name.ToLower();
                query = query.Where(m => m.Diachihieuchuan.Contains(lowerName)
                    || m.Tencognty.Contains(lowerName)
                    || m.MaPhieu.Contains(lowerName)
                    || m.SoNhanDang.Contains(lowerName)
                    || m.Diachicongty.Contains(lowerName)
                    || m.SerialNumber.Contains(lowerName)
                    || m.Model.Contains(lowerName)
                    || m.Masothue.Contains(lowerName)
                    || m.Dichvuyeucau.Contains(lowerName)
                    || m.Danhhuy.Contains(lowerName));
            }
             
            var result = query
                        .OrderByDescending(m => m.Id_PhieuHieuChuan)  
                        .ToPagedList(pageNumber, pageSize); 

            return result;
        }
    }
}
