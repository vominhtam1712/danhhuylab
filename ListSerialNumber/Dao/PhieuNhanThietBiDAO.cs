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
    public class PhieuNhanThietBiDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int getCountIndex()
        {
            return dbContext.PhieuNhanThietBis.Count(m => m.Status == 1);
        }
        public PagedList.IPagedList<Join_phieunhanthietbi_listnumber_khachhang> getlistIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = from pntb in dbContext.PhieuNhanThietBis.AsNoTracking()
                        join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                        join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                        select new Join_phieunhanthietbi_listnumber_khachhang
                        {
                            Id_pntb = pntb.Id,
                            SoYeuCau = pntb.SoYeuCau,
                            SoNhanDang = pntb.SoNhanDang,
                            NgayThucHien = pntb.NgayThucHien,
                            NgayTao = pntb.Ngaytao,
                            NguoiTao = pntb.Nguoitao,
                            TenKH = kh.TenKH,
                            Img = lnb.Image,
                            TenTB = lnb.Tenthietbi,
                            Hang = lnb.Hang,
                            Model  = lnb.Model,
                            SoSN  = lnb.SerialNumber,
                            NguoiThucHien = pntb.NguoiThucHien,
                            HienTrang = pntb.HienTrang,
                            GhiChu = pntb.GhiChu,
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

            var result = query.OrderByDescending(m => m.Id_pntb)
                              .ToPagedList(pageNumber, pageSize);

            return result;
        }
        public List<Join_phieunhanthietbi_listnumber_khachhang> getlistDetails(int? id)
        {
            if (id == null) return new List<Join_phieunhanthietbi_listnumber_khachhang>();

            var list = from pntb in dbContext.PhieuNhanThietBis.AsNoTracking()
                       where pntb.Id == id
                       join lnb in dbContext.ListNumbers.AsNoTracking() on pntb.Id_ListNumber equals lnb.Id
                       join kh in dbContext.KhachHangs.AsNoTracking() on lnb.Id_KhachHang equals kh.Id
                       select new Join_phieunhanthietbi_listnumber_khachhang
                       {
                           Id_pntb = pntb.Id,
                           SoYeuCau = pntb.SoYeuCau,
                           SoNhanDang = pntb.SoNhanDang,
                           NgayThucHien = pntb.NgayThucHien,
                           NgayTao = pntb.Ngaytao,
                           NguoiTao = pntb.Nguoitao,
                           TenKH = kh.TenKH,
                           DiaChi = kh.DiaChi,
                           LienHe = kh.LienHe,
                           Img = lnb.Image,
                           TenTB = lnb.Tenthietbi,
                           Hang = lnb.Hang,
                           Model = lnb.Model,
                           SoSN = lnb.SerialNumber,
                           NguoiThucHien = pntb.NguoiThucHien,
                           HienTrang = pntb.HienTrang,
                           GhiChu = pntb.GhiChu,
                       };

            var result = list.OrderByDescending(m => m.Id_pntb)
                             .GroupBy(m => m.Id_pntb)
                             .Select(g => g.FirstOrDefault())
                             .ToList();

            return result;
        }
        public bool CheckSerialNumberExists(string sonhandang)
        { 
            return dbContext.PhieuNhanThietBis.Any(m => m.SoNhanDang == sonhandang);
        }
        public IEnumerable<PhieuNhanThietBi> GetRowsByIds(IEnumerable<int> ids)
        { 
            return dbContext.PhieuNhanThietBis
                .AsNoTracking()  
                .Where(ln => ids.Contains(ln.Id))  
                .ToList();
        } 
        public List<PhieuNhanThietBi> getList(string status = "All")
        { 
            IQueryable<PhieuNhanThietBi> query = dbContext.PhieuNhanThietBis;

            if (status == "Index")
            {
                query = query.Where(m => m.Status == 1);  
            }

            return query.OrderByDescending(m => m.Id).ToList(); 
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.PhieuNhanThietBis
                .Where(m => m.SoYeuCau.StartsWith("YC"))
                .Select(m => m.SoYeuCau)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "YC0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(2);
                var number = int.Parse(numberPart);
                number++;
                 
                if (number < 10000)
                {
                    nextcode = $"YC{number:D4}";
                }
                else
                {
                    nextcode = $"YC{number}";  
                }
            }
            return nextcode;
        }

        public List<PhieuNhanThietBi> getListId(int? id)
        {
            if (id == null)
            {
                return new List<PhieuNhanThietBi>();
            }

            return dbContext.PhieuNhanThietBis
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public PhieuNhanThietBi getRow(int? id)
        { 
            return id == null ? null : dbContext.PhieuNhanThietBis.FirstOrDefault(m => m.Id == id);
        }
        public PhieuNhanThietBi getRow(string SoNhanDang)
        { 
            return string.IsNullOrEmpty(SoNhanDang) ? null : dbContext.PhieuNhanThietBis.FirstOrDefault(m => m.SoNhanDang == SoNhanDang);
        } 
        public int Insert(PhieuNhanThietBi ListNumber)
        {
            dbContext.PhieuNhanThietBis.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(PhieuNhanThietBi ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(PhieuNhanThietBi ListNumber)
        {
            dbContext.PhieuNhanThietBis.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
