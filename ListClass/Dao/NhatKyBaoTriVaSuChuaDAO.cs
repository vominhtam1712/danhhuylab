using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListClass.Model;
using PagedList;

namespace ListClass.Dao
{
    public class NhatKyBaoTriVaSuChuaDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public PagedList.IPagedList<Join_NhatKyBaoTriVaSuChua_ThietBi> getlistIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();
             
            var query = from dmtb in dbContext.DanhMucThietBis
                        join nkbt in dbContext.NhatKyBaoTriVaSuChuas on dmtb.Id equals nkbt.Id_DanhMucThietBi
                        where nkbt.Status == 1
                        select new Join_NhatKyBaoTriVaSuChua_ThietBi
                        {
                            Id = nkbt.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            TenThietBi = dmtb.TenThietBi,
                            Serial = dmtb.Serial,
                            MaThietBi = dmtb.MaThietBi,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            NgayLapDat = nkbt.NgayLapDat,
                            NhatKyBaoTri = nkbt.NhatKyBaoTri,
                            NguoiTao = nkbt.NguoiTao,
                            NgayTao = nkbt.NgayTao,
                            Status = nkbt.Status
                        };
             
            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.MaThietBi.ToLower().Contains(lowerName) ||
                                          m.TenThietBi.ToLower().Contains(lowerName) ||
                                          m.HangSX.ToLower().Contains(lowerName) ||
                                          m.Serial.ToLower().Contains(lowerName));
            }
             
            var distinctList = query.Distinct().OrderByDescending(m => m.Id);

            return distinctList.ToPagedList(pageNumber, pageSize);
        } 
        public List<Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu> getlistId(int? id)
        {
            if (!id.HasValue)
            {
                return new List<Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu>(); 
            }

            var query = from dmtb in dbContext.DanhMucThietBis
                        join nkbt in dbContext.NhatKyBaoTriVaSuChuas on dmtb.Id equals nkbt.Id_DanhMucThietBi
                        join dulieu in dbContext.DuLieu_NhatKyBaoTriVaSuChua on nkbt.Id equals dulieu.Id_NhatKyBaoTriVaSuChua
                        where nkbt.Status == 1 && nkbt.Id == id
                        select new Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu
                        {
                            Id = nkbt.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_DuLieu = dulieu.Id,
                            TenThietBi = dmtb.TenThietBi,
                            Serial = dmtb.Serial,
                            MaThietBi = dmtb.MaThietBi,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            NgayLapDat = nkbt.NgayLapDat,
                            NhatKyBaoTri = nkbt.NhatKyBaoTri,
                            NguoiTao = nkbt.NguoiTao,
                            NgayTao = nkbt.NgayTao,
                            Ngay = dulieu.Ngay,
                            MoTaSuCo = dulieu.MoTaSuCo,
                            HanhDongKhacPhuc = dulieu.HanhDongKhacPhuc,
                            KetQua = dulieu.KetQua,
                            NguoiSuaChua = dulieu.NguoiSuaChua,
                            NguoiKiemTra = dulieu.NguoiKiemTra,
                            Status = nkbt.Status
                        };
             
            return query.AsNoTracking().ToList();
        }
        public List<Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu> getlistInPdf(int? id)
        {
            if (!id.HasValue)
            {
                return new List<Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu>();   
            }

            var query = from dmtb in dbContext.DanhMucThietBis
                        join nkbt in dbContext.NhatKyBaoTriVaSuChuas on dmtb.Id equals nkbt.Id_DanhMucThietBi
                        join dulieu in dbContext.DuLieu_NhatKyBaoTriVaSuChua on nkbt.Id equals dulieu.Id_NhatKyBaoTriVaSuChua
                        where nkbt.Status == 1 && nkbt.Id == id
                        select new Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu
                        {
                            Id = nkbt.Id,
                            Id_DanhMucThietBi = dmtb.Id,
                            Id_DuLieu = dulieu.Id,
                            TenThietBi = dmtb.TenThietBi,
                            Serial = dmtb.Serial,
                            MaThietBi = dmtb.MaThietBi,
                            Img = dmtb.Img,
                            HangSX = dmtb.HangSX,
                            NgayLapDat = nkbt.NgayLapDat,
                            NhatKyBaoTri = nkbt.NhatKyBaoTri,
                            NguoiTao = nkbt.NguoiTao,
                            NgayTao = nkbt.NgayTao,
                            Ngay = dulieu.Ngay,
                            MoTaSuCo = dulieu.MoTaSuCo,
                            HanhDongKhacPhuc = dulieu.HanhDongKhacPhuc,
                            KetQua = dulieu.KetQua,
                            NguoiSuaChua = dulieu.NguoiSuaChua,
                            NguoiKiemTra = dulieu.NguoiKiemTra,
                            Status = nkbt.Status
                        };
             
            return query.AsNoTracking().ToList();
        }
        public List<NhatKyBaoTriVaSuChua> getListId(int? id)
        { 
            if (!id.HasValue)
            {
                return new List<NhatKyBaoTriVaSuChua>();  
            }

            return dbContext.NhatKyBaoTriVaSuChuas
                            .Where(m => m.Id == id)
                            .AsNoTracking()  
                            .ToList();
        }

        public NhatKyBaoTriVaSuChua GetById(int id)
        {
            return dbContext.NhatKyBaoTriVaSuChuas
                            .AsNoTracking()  
                            .FirstOrDefault(m => m.Id == id);  
        }
        public int getCountIndex()
        {
            return dbContext.NhatKyBaoTriVaSuChuas.Count(m => m.Status == 1);
        }
        public int getCountTrash()
        {
            return dbContext.NhatKyBaoTriVaSuChuas.Count(m => m.Status != 1);
        }
        public IEnumerable<NhatKyBaoTriVaSuChua> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.NhatKyBaoTriVaSuChuas
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public List<NhatKyBaoTriVaSuChua> getList(string status = "All")
        { 
            IQueryable<NhatKyBaoTriVaSuChua> query = dbContext.NhatKyBaoTriVaSuChuas.AsNoTracking();
             
            if (status == "Index")
            {
                query = query.Where(m => m.Status != 0);
            }
             
            return query.OrderByDescending(m => m.Id).ToList();
        }
        public NhatKyBaoTriVaSuChua getRow(int? id)
        {
            if (!id.HasValue)
            {
                return null;
            }

            return dbContext.NhatKyBaoTriVaSuChuas
                            .AsNoTracking()  
                            .FirstOrDefault(m => m.Id == id);  
        }

        public int Insert(NhatKyBaoTriVaSuChua company)
        {
            dbContext.NhatKyBaoTriVaSuChuas.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(NhatKyBaoTriVaSuChua company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(NhatKyBaoTriVaSuChua company)
        {
            dbContext.NhatKyBaoTriVaSuChuas.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
