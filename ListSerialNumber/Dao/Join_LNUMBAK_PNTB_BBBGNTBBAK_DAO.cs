using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListClass.Dao
{
    public class Join_LNUMBAK_PNTB_BBBGNTBBAK_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<Join_LNUMBAK_PNTB_BBBGNTBBAK> GetListJoinBienBan1(int? Id)
        {
            var list = (from p in dbContext.PhieuNhanThietBis
                        where p.Status == 1
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join b in dbContext.BienBanBanGiaoNhanThietBis on p.Id_BienBanBanGiaoNhanThietBi equals b.Id
                        where b.Id == Id
                        orderby b.Id descending
                        select new Join_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_Number = l.Id,
                            Id_BienBan = b.Id,
                            Id_PhieuNhan = p.Id,
                            SoYeuCau = b.SoYeuCau,
                            NgayThucHien = b.NgayThucHien,
                            KhachHang = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            Tenthietbi = l.Tenthietbi,
                            Img = l.Image,
                            Hang = l.Hang,
                            Model = l.Model,
                            SerialNumber = l.SerialNumber,
                            SoNhanDang = p.SoNhanDang,
                            NguoiThucHien = p.NguoiThucHien,
                            HienTrang = p.HienTrang,
                            GhiChu = p.GhiChu,
                            NgaytaoSanPham = l.NgaytaoSanPham,
                            Ngaytao = p.Ngaytao,
                            Nguoitao = b.Nguoitao,
                            Status = p.Status,
                        }).ToList();

            return list;
        }
        public List<Join_LNUMBAK_PNTB_BBBGNTBBAK> getlistjoinBienBanTrash(int? Id)
        {
            var list = (from p in dbContext.PhieuNhanThietBis
                        where p.Status != 1
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join b in dbContext.BienBanBanGiaoNhanThietBis on p.Id_BienBanBanGiaoNhanThietBi equals b.Id
                        where b.Id == Id
                        orderby b.Id descending
                        select new Join_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_Number = l.Id,
                            Id_BienBan = b.Id,
                            Id_PhieuNhan = p.Id,
                            SoYeuCau = b.SoYeuCau,
                            NgayThucHien = b.NgayThucHien,
                            KhachHang = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            Tenthietbi = l.Tenthietbi,
                            Img = l.Image,
                            Hang = l.Hang,
                            Model = l.Model,
                            SerialNumber = l.SerialNumber,
                            SoNhanDang = p.SoNhanDang,
                            NguoiThucHien = p.NguoiThucHien,
                            HienTrang = p.HienTrang,
                            GhiChu = p.GhiChu,
                            NgaytaoSanPham = l.NgaytaoSanPham,
                            Ngaytao = p.Ngaytao,
                            Nguoitao = b.Nguoitao,
                            Status = p.Status,
                        }).ToList();
            return list;
        }
        public List<Join_LNUMBAK_PNTB_BBBGNTBBAK> GetListJoinBienBanPdf(int? id)
        {
            var list = (from p in dbContext.PhieuNhanThietBis
                        where p.Status == 1
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join b in dbContext.BienBanBanGiaoNhanThietBis on p.Id_BienBanBanGiaoNhanThietBi equals b.Id
                        where b.Id == id
                        orderby b.Id descending
                        select new Join_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_Number = l.Id,
                            Id_BienBan = b.Id,
                            Id_PhieuNhan = p.Id,
                            SoYeuCau = b.SoYeuCau,
                            NgayThucHien = b.NgayThucHien,
                            KhachHang = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            Tenthietbi = l.Tenthietbi,
                            Img = l.Image,
                            Hang = l.Hang,
                            Model = l.Model,
                            SerialNumber = l.SerialNumber,
                            SoNhanDang = p.SoNhanDang,
                            NguoiThucHien = p.NguoiThucHien,
                            HienTrang = p.HienTrang,
                            GhiChu = p.GhiChu,
                            NgaytaoSanPham = l.NgaytaoSanPham,
                            Ngaytao = p.Ngaytao,
                            Nguoitao = b.Nguoitao,
                            Status = p.Status,
                        }).ToList();

            return list;
        }

        public List<Join_LNUMBAK_PNTB_BBBGNTBBAK> getListAll()
        {
            var list = (from p in dbContext.PhieuNhanThietBis
                        where p.Status == 1
                        join l in dbContext.ListNumbers on p.Id_ListNumber equals l.Id
                        join kh in dbContext.KhachHangs on l.Id_KhachHang equals kh.Id
                        join b in dbContext.BienBanBanGiaoNhanThietBis on p.Id_BienBanBanGiaoNhanThietBi equals b.Id
                        where b.Status == 1
                        orderby b.Id descending
                        select new Join_LNUMBAK_PNTB_BBBGNTBBAK
                        {
                            Id_Number = l.Id,
                            Id_BienBan = b.Id,
                            Id_PhieuNhan = p.Id,
                            SoYeuCau = b.SoYeuCau,
                            NgayThucHien = b.NgayThucHien,
                            KhachHang = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            Tenthietbi = l.Tenthietbi,
                            Img = l.Image,
                            Hang = l.Hang,
                            Model = l.Model,
                            SerialNumber = l.SerialNumber,
                            SoNhanDang = p.SoNhanDang,
                            NguoiThucHien = p.NguoiThucHien,
                            HienTrang = p.HienTrang,
                            GhiChu = p.GhiChu,
                            NgaytaoSanPham = l.NgaytaoSanPham,
                            Ngaytao = p.Ngaytao,
                            Nguoitao = b.Nguoitao,
                            Status = p.Status,
                        }).ToList();
            return list;
        }
    }
}
