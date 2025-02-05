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
    public class Join_KeHoachHieuChuanKiemTra_ThietBi_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int GetCountIndex()
        {
            return dbContext.KeHoachHieuChuans.Count(m => m.Status == 1);
        }
        public int GetCountTrash()
        {
            return dbContext.KeHoachHieuChuans.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<Join_KeHoachHieuChuanKiemTra_ThietBi> getlistIndex(int? id, string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = from dmtb in dbContext.DanhMucThietBis.AsNoTracking()
                        join khhc in dbContext.KeHoachHieuChuans.AsNoTracking() on dmtb.Id equals khhc.Id_DanhMucThietBi
                        join khhcform in dbContext.KeHoachHieuChuan_Forms.AsNoTracking() on khhc.Id_KeHoachHieuChuan_Form equals khhcform.Id
                        where khhc.Status == 1 && khhcform.Id == id
                        select new Join_KeHoachHieuChuanKiemTra_ThietBi
                        {
                            Id = khhc.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_KeHoachHieuChuan_form = khhcform.Id,
                            TenThietBi = dmtb.TenThietBi,
                            MaThietBi = dmtb.MaThietBi,
                            Serial = dmtb.Serial,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            Jan_Plan = khhc.Jan_Plan,
                            Jan_Act = khhc.Jan_Act,
                            Feb_Plan = khhc.Feb_Plan,
                            Feb_Act = khhc.Feb_Act,
                            March_Plan = khhc.March_Plan,
                            March_Act = khhc.March_Act,
                            April_Plan = khhc.April_Plan,
                            April_Act = khhc.April_Act,
                            May_Plan = khhc.May_Plan,
                            May_Act = khhc.May_Act,
                            June_Plan = khhc.June_Plan,
                            June_Act = khhc.June_Act,
                            July_Plan = khhc.July_Plan,
                            July_Act = khhc.July_Act,
                            Aug_Plan = khhc.Aug_Plan,
                            Aug_Act = khhc.Aug_Act,
                            Sep_Plan = khhc.Sep_Plan,
                            Sep_Act = khhc.Sep_Act,
                            Oct_Plan = khhc.Oct_Plan,
                            Oct_Act = khhc.Oct_Act,
                            Nov_Plan = khhc.Nov_Plan,
                            Nov_Act = khhc.Nov_Act,
                            Dec_Plan = khhc.Dec_Plan,
                            Dec_Act = khhc.Dec_Act,
                            NguoiTao = khhc.NguoiTao,
                            NgayTao = khhc.NgayTao,
                            NgayTao_Pdf = khhcform.NgayTao,
                            Status = khhc.Status,
                        };
             
            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.MaThietBi.Contains(lowerName) ||
                                          m.TenThietBi.Contains(lowerName) ||
                                          m.HangSX.Contains(lowerName) ||
                                          m.Serial.Contains(lowerName));
            }
             
            var result = query.OrderByDescending(m => m.Id)
                              .ToPagedList(pageNumber, pageSize);

            return result;
        }
        public PagedList.IPagedList<Join_KeHoachHieuChuanKiemTra_ThietBi> getlistTrash(int? id, string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = from dmtb in dbContext.DanhMucThietBis.AsNoTracking()
                        join khhc in dbContext.KeHoachHieuChuans.AsNoTracking() on dmtb.Id equals khhc.Id_DanhMucThietBi
                        join khhcform in dbContext.KeHoachHieuChuan_Forms.AsNoTracking() on khhc.Id_KeHoachHieuChuan_Form equals khhcform.Id
                        where khhc.Status != 1 && khhcform.Id == id
                        select new Join_KeHoachHieuChuanKiemTra_ThietBi
                        {
                            Id = khhc.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_KeHoachHieuChuan_form = khhcform.Id,
                            TenThietBi = dmtb.TenThietBi,
                            MaThietBi = dmtb.MaThietBi,
                            Serial = dmtb.Serial,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            Jan_Plan = khhc.Jan_Plan,
                            Jan_Act = khhc.Jan_Act,
                            Feb_Plan = khhc.Feb_Plan,
                            Feb_Act = khhc.Feb_Act,
                            March_Plan = khhc.March_Plan,
                            March_Act = khhc.March_Act,
                            April_Plan = khhc.April_Plan,
                            April_Act = khhc.April_Act,
                            May_Plan = khhc.May_Plan,
                            May_Act = khhc.May_Act,
                            June_Plan = khhc.June_Plan,
                            June_Act = khhc.June_Act,
                            July_Plan = khhc.July_Plan,
                            July_Act = khhc.July_Act,
                            Aug_Plan = khhc.Aug_Plan,
                            Aug_Act = khhc.Aug_Act,
                            Sep_Plan = khhc.Sep_Plan,
                            Sep_Act = khhc.Sep_Act,
                            Oct_Plan = khhc.Oct_Plan,
                            Oct_Act = khhc.Oct_Act,
                            Nov_Plan = khhc.Nov_Plan,
                            Nov_Act = khhc.Nov_Act,
                            Dec_Plan = khhc.Dec_Plan,
                            Dec_Act = khhc.Dec_Act,
                            NguoiTao = khhc.NguoiTao,
                            NgayTao = khhc.NgayTao,
                            Status = khhc.Status,
                        };
             
            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.MaThietBi.Contains(lowerName) ||
                                          m.TenThietBi.Contains(lowerName) ||
                                          m.HangSX.Contains(lowerName) ||
                                          m.Serial.Contains(lowerName));
            }
             
            var result = query.OrderByDescending(m => m.Id)
                              .ToPagedList(pageNumber, pageSize);

            return result;
        }
        public IEnumerable<Join_KeHoachHieuChuanKiemTra_ThietBi> GetRowsByIds(IEnumerable<int> ids)
        { 
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<Join_KeHoachHieuChuanKiemTra_ThietBi>();
            }
             
            var query = from dmtb in dbContext.DanhMucThietBis.AsNoTracking()
                        join khhc in dbContext.KeHoachHieuChuans.AsNoTracking() on dmtb.Id equals khhc.Id_DanhMucThietBi
                        join khhcform in dbContext.KeHoachHieuChuan_Forms.AsNoTracking() on khhc.Id_KeHoachHieuChuan_Form equals khhcform.Id
                        where ids.Contains(khhc.Id) 
                        select new Join_KeHoachHieuChuanKiemTra_ThietBi
                        {
                            Id = khhc.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_KeHoachHieuChuan_form = khhcform.Id,
                            TenThietBi = dmtb.TenThietBi,
                            MaThietBi = dmtb.MaThietBi,
                            Serial = dmtb.Serial,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            Jan_Plan = khhc.Jan_Plan,
                            Jan_Act = khhc.Jan_Act,
                            Feb_Plan = khhc.Feb_Plan,
                            Feb_Act = khhc.Feb_Act,
                            March_Plan = khhc.March_Plan,
                            March_Act = khhc.March_Act,
                            April_Plan = khhc.April_Plan,
                            April_Act = khhc.April_Act,
                            May_Plan = khhc.May_Plan,
                            May_Act = khhc.May_Act,
                            June_Plan = khhc.June_Plan,
                            June_Act = khhc.June_Act,
                            July_Plan = khhc.July_Plan,
                            July_Act = khhc.July_Act,
                            Aug_Plan = khhc.Aug_Plan,
                            Aug_Act = khhc.Aug_Act,
                            Sep_Plan = khhc.Sep_Plan,
                            Sep_Act = khhc.Sep_Act,
                            Oct_Plan = khhc.Oct_Plan,
                            Oct_Act = khhc.Oct_Act,
                            Nov_Plan = khhc.Nov_Plan,
                            Nov_Act = khhc.Nov_Act,
                            Dec_Plan = khhc.Dec_Plan,
                            Dec_Act = khhc.Dec_Act,
                            NguoiTao = khhc.NguoiTao,
                            NgayTao = khhc.NgayTao,
                            Status = khhc.Status,
                        };
             
            return query.ToList();
        }
        public List<Join_KeHoachHieuChuanKiemTra_ThietBi> getlistId(int? id)
        {
            if (!id.HasValue)
            {
                return new List<Join_KeHoachHieuChuanKiemTra_ThietBi>();
            }

            var query = from dmtb in dbContext.DanhMucThietBis.AsNoTracking()  
                        join khhc in dbContext.KeHoachHieuChuans.AsNoTracking() on dmtb.Id equals khhc.Id_DanhMucThietBi
                        join khhcform in dbContext.KeHoachHieuChuan_Forms.AsNoTracking() on khhc.Id_KeHoachHieuChuan_Form equals khhcform.Id
                        where khhc.Id == id  
                        select new Join_KeHoachHieuChuanKiemTra_ThietBi
                        {
                            Id = khhc.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_KeHoachHieuChuan_form = khhcform.Id,
                            TenThietBi = dmtb.TenThietBi,
                            MaThietBi = dmtb.MaThietBi,
                            Serial = dmtb.Serial,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            Jan_Plan = khhc.Jan_Plan,
                            Jan_Act = khhc.Jan_Act,
                            Feb_Plan = khhc.Feb_Plan,
                            Feb_Act = khhc.Feb_Act,
                            March_Plan = khhc.March_Plan,
                            March_Act = khhc.March_Act,
                            April_Plan = khhc.April_Plan,
                            April_Act = khhc.April_Act,
                            May_Plan = khhc.May_Plan,
                            May_Act = khhc.May_Act,
                            June_Plan = khhc.June_Plan,
                            June_Act = khhc.June_Act,
                            July_Plan = khhc.July_Plan,
                            July_Act = khhc.July_Act,
                            Aug_Plan = khhc.Aug_Plan,
                            Aug_Act = khhc.Aug_Act,
                            Sep_Plan = khhc.Sep_Plan,
                            Sep_Act = khhc.Sep_Act,
                            Oct_Plan = khhc.Oct_Plan,
                            Oct_Act = khhc.Oct_Act,
                            Nov_Plan = khhc.Nov_Plan,
                            Nov_Act = khhc.Nov_Act,
                            Dec_Plan = khhc.Dec_Plan,
                            Dec_Act = khhc.Dec_Act,
                            NguoiTao = khhc.NguoiTao,
                            NgayTao = khhc.NgayTao,
                            Status = khhc.Status,
                        };
             
            return query.ToList();
        }
        public List<Join_KeHoachHieuChuanKiemTra_ThietBi> getlistPdf(int? id)
        { 
            if (!id.HasValue)
            {
                return new List<Join_KeHoachHieuChuanKiemTra_ThietBi>();
            }

            var query = from dmtb in dbContext.DanhMucThietBis.AsNoTracking() 
                        join khhc in dbContext.KeHoachHieuChuans.AsNoTracking() on dmtb.Id equals khhc.Id_DanhMucThietBi
                        join khhcform in dbContext.KeHoachHieuChuan_Forms.AsNoTracking() on khhc.Id_KeHoachHieuChuan_Form equals khhcform.Id
                        where khhc.Status == 1 && khhcform.Id == id 
                        select new Join_KeHoachHieuChuanKiemTra_ThietBi
                        {
                            Id = khhc.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_KeHoachHieuChuan_form = khhcform.Id,
                            TenThietBi = dmtb.TenThietBi,
                            MaThietBi = dmtb.MaThietBi,
                            Serial = dmtb.Serial,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            Jan_Plan = khhc.Jan_Plan,
                            Jan_Act = khhc.Jan_Act,
                            Feb_Plan = khhc.Feb_Plan,
                            Feb_Act = khhc.Feb_Act,
                            March_Plan = khhc.March_Plan,
                            March_Act = khhc.March_Act,
                            April_Plan = khhc.April_Plan,
                            April_Act = khhc.April_Act,
                            May_Plan = khhc.May_Plan,
                            May_Act = khhc.May_Act,
                            June_Plan = khhc.June_Plan,
                            June_Act = khhc.June_Act,
                            July_Plan = khhc.July_Plan,
                            July_Act = khhc.July_Act,
                            Aug_Plan = khhc.Aug_Plan,
                            Aug_Act = khhc.Aug_Act,
                            Sep_Plan = khhc.Sep_Plan,
                            Sep_Act = khhc.Sep_Act,
                            Oct_Plan = khhc.Oct_Plan,
                            Oct_Act = khhc.Oct_Act,
                            Nov_Plan = khhc.Nov_Plan,
                            Nov_Act = khhc.Nov_Act,
                            Dec_Plan = khhc.Dec_Plan,
                            Dec_Act = khhc.Dec_Act,
                            NguoiTao = khhc.NguoiTao,
                            NgayTao = khhc.NgayTao,
                            Status = khhc.Status,
                        };
             
            return query.ToList();
        }
    }
}
