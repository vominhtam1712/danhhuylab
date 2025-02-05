using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_KhachHang_Listnumber_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public PagedList.IPagedList<Join_KhachHang_Listnumber> getlistjoin(string Name, int pageSize, int pageNumber)
        { 
            var query = from listnumber in dbContext.ListNumbers.AsNoTracking()
                        join kh in dbContext.KhachHangs.AsNoTracking() on listnumber.Id_KhachHang equals kh.Id
                        join pntb in dbContext.PhieuNhanThietBis.AsNoTracking() on listnumber.Id equals pntb.Id_ListNumber into pntbJoin
                        from pntb in pntbJoin.DefaultIfEmpty()
                        join pychc in dbContext.Phieuyeucauhieuchuans.AsNoTracking() on pntb.Id equals pychc.Id_phieunhanthietbi into pychcJoin
                        from pychc in pychcJoin.DefaultIfEmpty()
                        join pc in dbContext.PhanCongs.AsNoTracking() on pychc.Id equals pc.Id_pychc into pcJoin
                        from pc in pcJoin.DefaultIfEmpty()
                        join bbhc in dbContext.BienBanHieuChuans.AsNoTracking() on pc.Id equals bbhc.Id_PhanCong into bbhcJoin
                        from bbhc in bbhcJoin.DefaultIfEmpty()
                        join gcn in dbContext.Giaychungnhans.AsNoTracking() on bbhc.Id equals gcn.Id_BienBanHieuChuan into gcnJoin
                        from gcn in gcnJoin.DefaultIfEmpty()
                        where listnumber.Status == 1
                        select new Join_KhachHang_Listnumber
                        {
                            Id_KhachHang = kh.Id,
                            Id_Listnumber = listnumber.Id,
                            Tenthietbi = listnumber.Tenthietbi,
                            Hang = listnumber.Hang,
                            Img = listnumber.Image,
                            Model = listnumber.Model,
                            PhamViDo = listnumber.PhamViDo,
                            DoPhanGiai = listnumber.DoPhanGiai,
                            Serial = listnumber.SerialNumber,
                            TenKH = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            Email = kh.Email,
                            SDT = kh.SDT,
                            LienHe = kh.LienHe,
                            NhomNganh = kh.NhomNganh,
                            GhiChu = listnumber.GhiChu,
                            NgayTao = listnumber.NgaytaoSanPham,
                            Status = listnumber.Status,
                            NgayHCLai = gcn.NgayHieuChuanLai,
                        };
             
            if (!string.IsNullOrEmpty(Name))
            {
                string nameLower = Name.ToLower();
                query = query.Where(m => m.TenKH.ToLower().Contains(nameLower) ||
                                          m.Tenthietbi.ToLower().Contains(nameLower) ||
                                          m.Hang.ToLower().Contains(nameLower) ||
                                          m.Model.ToLower().Contains(nameLower) ||
                                          m.Serial.ToLower().Contains(nameLower) ||
                                          m.Email.ToLower().Contains(nameLower) ||
                                          m.SDT.ToLower().Contains(nameLower) ||
                                          m.LienHe.ToLower().Contains(nameLower) ||
                                          m.NhomNganh.ToLower().Contains(nameLower) ||
                                          m.GhiChu.ToLower().Contains(nameLower)); 
            }
            var result = query.OrderByDescending(m => m.Id_Listnumber)
                                  .ToPagedList(pageNumber, pageSize);
            return result;
        } 
        public List<Join_KhachHang_Listnumber> getlistjoin(string name, int? id)
        { 
            if (!id.HasValue)
            {
                return new List<Join_KhachHang_Listnumber>();
            }
             
            var query = from listnumber in dbContext.ListNumbers.AsNoTracking()
                        join kh in dbContext.KhachHangs.AsNoTracking() on listnumber.Id_KhachHang equals kh.Id
                        where listnumber.Status == 1 && listnumber.Id == id
                        select new Join_KhachHang_Listnumber
                        {
                            Id_KhachHang = kh.Id,
                            Id_Listnumber = listnumber.Id,
                            Tenthietbi = listnumber.Tenthietbi,
                            Hang = listnumber.Hang,
                            Img = listnumber.Image,
                            Model = listnumber.Model,
                            PhamViDo = listnumber.PhamViDo,
                            DoPhanGiai = listnumber.DoPhanGiai,
                            Serial = listnumber.SerialNumber,
                            TenKH = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            Email = kh.Email,
                            SDT = kh.SDT,
                            LienHe = kh.LienHe,
                            NhomNganh = kh.NhomNganh,
                            GhiChu = listnumber.GhiChu,
                            NgayTao = listnumber.NgaytaoSanPham,
                            Status = listnumber.Status,
                        }; 
            return query.ToList();
        }
    }
}
