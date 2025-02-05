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
    public class PhieutrathietbiDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int getCountIndex()
        {
            return dbContext.Phieutrathietbis.Count(m => m.status == 1);
        }
        public PagedList.IPagedList<Join_phieutra_phieunhan_listnumber_khachhang> getlistIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = from pttb in dbContext.Phieutrathietbis.AsNoTracking()
                        join gcn in dbContext.Giaychungnhans.AsNoTracking() on pttb.Id_Giaychungnhan equals gcn.Id
                        join bbhc in dbContext.BienBanHieuChuans.AsNoTracking() on gcn.Id_BienBanHieuChuan equals bbhc.Id
                        join pc in dbContext.PhanCongs.AsNoTracking() on bbhc.Id_PhanCong equals pc.Id
                        join pychc in dbContext.Phieuyeucauhieuchuans.AsNoTracking() on pc.Id_pychc equals pychc.Id
                        join pntb in dbContext.PhieuNhanThietBis.AsNoTracking() on pychc.Id_phieunhanthietbi equals pntb.Id
                        join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                        select new Join_phieutra_phieunhan_listnumber_khachhang
                        {
                            SoGCN = gcn.MaGCN,
                            Id_phieutra = pttb.Id,
                            MaPT = pttb.MaPT,
                            SoYeuCau = pntb.SoYeuCau,
                            SoNhanDang = pntb.SoNhanDang,
                            NgayThucHien = pttb.NgayThucHien,
                            NgayTao = pttb.NgayTao,
                            NguoiTao = pttb.NguoiTao,
                            PhuongThucGiaoTra = pttb.PhuongThucGiaoTra,
                            TrangThaiThietBi = pttb.TrangThaiThietbi,
                            TenKH = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            Img = lnb.Image,
                            TenTB = lnb.Tenthietbi,
                            Hang = lnb.Hang,
                            Model = lnb.Model,
                            SoSN = lnb.SerialNumber,
                            GhiChu = pttb.Ghichu,
                        };

            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.SoYeuCau.Contains(lowerName) ||
                                          m.SoNhanDang.Contains(lowerName) ||
                                          m.TenKH.Contains(lowerName) ||
                                          m.TenTB.Contains(lowerName) ||
                                          m.Model.Contains(lowerName) ||
                                          m.Hang.Contains(lowerName) ||
                                          m.SoSN.Contains(lowerName));
            }

            var result = query.OrderByDescending(m => m.Id_phieutra)
                              .ToPagedList(pageNumber, pageSize);

            return result;
        }
        public List<Join_phieutra_phieunhan_listnumber_khachhang> getlistDetails(int? id)
        {
            if (id == null) return new List<Join_phieutra_phieunhan_listnumber_khachhang>();

            var list = from pttb in dbContext.Phieutrathietbis.AsNoTracking()
                       join gcn in dbContext.Giaychungnhans.AsNoTracking() on pttb.Id_Giaychungnhan equals gcn.Id
                       join bbhc in dbContext.BienBanHieuChuans.AsNoTracking() on gcn.Id_BienBanHieuChuan equals bbhc.Id
                       join pc in dbContext.PhanCongs.AsNoTracking() on bbhc.Id_PhanCong equals pc.Id
                       join pychc in dbContext.Phieuyeucauhieuchuans.AsNoTracking() on pc.Id_pychc equals pychc.Id
                       join pntb in dbContext.PhieuNhanThietBis.AsNoTracking() on pychc.Id_phieunhanthietbi equals pntb.Id
                       join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                       join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                       where pttb.Id == id
                       select new Join_phieutra_phieunhan_listnumber_khachhang
                       {
                           Id_phieutra = pttb.Id,
                           MaPT = pttb.MaPT,
                           SoYeuCau = pntb.SoYeuCau,
                           SoNhanDang = pntb.SoNhanDang,
                           NgayThucHien = pttb.NgayThucHien,
                           NgayTao = pttb.NgayTao,
                           NguoiTao = pttb.NguoiTao,
                           PhuongThucGiaoTra = pttb.PhuongThucGiaoTra,
                           TrangThaiThietBi = pttb.TrangThaiThietbi,
                           TenKH = kh.TenKH,
                           DiaChi = kh.DiaChi,
                           LienHe = kh.LienHe,
                           Img = lnb.Image,
                           TenTB = lnb.Tenthietbi,
                           Hang = lnb.Hang,
                           Model = lnb.Model,
                           SoSN = lnb.SerialNumber,
                           GhiChu = pttb.Ghichu,
                       };

            var result = list.OrderByDescending(m => m.Id_phieutra)
                             .GroupBy(m => m.Id_phieutra)
                             .Select(g => g.FirstOrDefault())
                             .ToList();

            return result;
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.Phieutrathietbis
                .Where(m => m.MaPT.StartsWith("PT"))
                .Select(m => m.MaPT)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "PT0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(2);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"PT{number:D4}";
                }
                else
                {
                    nextcode = $"PT{number}";
                }
            }
            return nextcode;
        }
        public IEnumerable<Phieutrathietbi> GetRowsByIds(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any()) return Enumerable.Empty<Phieutrathietbi>(); 

            return dbContext.Phieutrathietbis
                            .AsNoTracking()
                            .Where(ln => ids.Contains(ln.Id)) 
                            .ToList();
        }
        public int Insert(Phieutrathietbi ListNumber)
        {
            dbContext.Phieutrathietbis.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(Phieutrathietbi ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(Phieutrathietbi ListNumber)
        {
            dbContext.Phieutrathietbis.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
