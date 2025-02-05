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
    public class PhieuChuyenMauHieuChuanDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public IEnumerable<PhieuChuyenMauHieuChuan> GetRowsByIds(IEnumerable<int> ids)
        { 
            const int batchSize = 1000;  
            var idList = ids.ToList();
            var result = new List<PhieuChuyenMauHieuChuan>();

            for (int i = 0; i < idList.Count; i += batchSize)
            {
                var batchIds = idList.Skip(i).Take(batchSize);
                result.AddRange(dbContext.PhieuChuyenMauHieuChuans
                    .AsNoTracking()
                    .Where(ln => batchIds.Contains(ln.Id))
                    .ToList());
            }

            return result;
        }
        public int getCountIndex()
        {
            return dbContext.PhieuChuyenMauHieuChuans.Count(m => m.Status == 1);
        }
        public PagedList.IPagedList<Join_phieuchuyen_phieunhan_listnumber_khachhang> getlistIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = from pctb in dbContext.PhieuChuyenMauHieuChuans.AsNoTracking()
                        join pntb in dbContext.PhieuNhanThietBis.AsNoTracking() on pctb.Id_PhieuNhanThietBi equals pntb.Id
                        join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                        select new Join_phieuchuyen_phieunhan_listnumber_khachhang
                        {
                            Id_phieuchuyen = pctb.Id,
                            Ma_phieuchuyen = pctb.MaSoMau,
                            SoNhanDang = pntb.SoNhanDang,
                            TenKH = kh.TenKH,
                            SoSN = lnb.SerialNumber,
                            NguoiNhan = pctb.NguoiNhan,
                            NguoiDuocPC = pctb.NguoiDuocPhanCong,
                            PhuocPhapHC = pctb.PhuongPhamHieuChuan,
                            ThongSoHC = pctb.ThongSoHieuChuan,
                            NguoiTao = pctb.NguoiTao, 
                            NgayNhan = pctb.NgayNhan,
                            NgayTra = pctb.NgayTraBaoCao, 
                        };

            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.Ma_phieuchuyen.Contains(lowerName) ||
                                          m.SoNhanDang.Contains(lowerName) ||
                                          m.TenKH.Contains(lowerName) ||
                                          m.SoSN.Contains(lowerName) || 
                                          m.SoSN.Contains(lowerName));
            }

            var result = query.OrderByDescending(m => m.Id_phieuchuyen)
                              .ToPagedList(pageNumber, pageSize);

            return result;
        }
        public List<Join_phieuchuyen_phieunhan_listnumber_khachhang> getlistDetails(int? id)
        {
            if (id == null) return new List<Join_phieuchuyen_phieunhan_listnumber_khachhang>();

            var list = from pctb in dbContext.PhieuChuyenMauHieuChuans.AsNoTracking()
                       where pctb.Id == id
                       join pntb in dbContext.PhieuNhanThietBis.AsNoTracking() on pctb.Id_PhieuNhanThietBi equals pntb.Id
                       join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                       join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                       select new Join_phieuchuyen_phieunhan_listnumber_khachhang
                       {
                           Id_phieuchuyen = pctb.Id,
                           Ma_phieuchuyen = pctb.MaSoMau,
                           SoNhanDang = pntb.SoNhanDang,
                           TenKH = kh.TenKH,
                           SoSN = lnb.SerialNumber,
                           Img = lnb.Image,
                           NguoiNhan = pctb.NguoiNhan,
                           NguoiDuocPC = pctb.NguoiDuocPhanCong,
                           PhuocPhapHC = pctb.PhuongPhamHieuChuan,
                           ThongSoHC = pctb.ThongSoHieuChuan,
                           NguoiTao = pctb.NguoiTao,
                           NgayNhan = pctb.NgayNhan,
                           NgayTra = pctb.NgayTraBaoCao,
                       };

            var result = list.OrderByDescending(m => m.Id_phieuchuyen)
                             .GroupBy(m => m.Id_phieuchuyen)
                             .Select(g => g.FirstOrDefault())
                             .ToList();

            return result;
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.PhieuChuyenMauHieuChuans
                .Where(m => m.MaSoMau.StartsWith("MSM"))
                .Select(m => m.MaSoMau)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "MSM0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"MSM{number:D4}";
                }
                else
                {
                    nextcode = $"MSM{number}";
                }
            }
            return nextcode;
        } 
        public PhieuChuyenMauHieuChuan getRow(int? id)
        {
            return id.HasValue
                ? dbContext.PhieuChuyenMauHieuChuans
                    .AsNoTracking()  
                    .FirstOrDefault(m => m.Id == id.Value)  
                : null;  
        }
        public int Insert(PhieuChuyenMauHieuChuan ListNumber)
        {
            dbContext.PhieuChuyenMauHieuChuans.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(PhieuChuyenMauHieuChuan ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(PhieuChuyenMauHieuChuan ListNumber)
        {
            dbContext.PhieuChuyenMauHieuChuans.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
