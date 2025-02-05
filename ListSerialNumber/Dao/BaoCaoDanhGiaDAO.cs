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
    public class BaoCaoDanhGiaDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int getCountIndex()
        {
            return dbContext.BaoCaoDanhGias.Count(m => m.Status == 1);
        }
        public int getCountTrash()
        {
            return dbContext.BaoCaoDanhGias.Count(m => m.Status != 1);
        }
        public PagedList.IPagedList<Join_Baocaodanhgia_thietbi> getlistIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = dbContext.DanhMucThietBis
                .AsNoTracking()   
                .Join(
                    dbContext.BaoCaoDanhGias.AsNoTracking().Where(b => b.Status == 1),  
                    dmtb => dmtb.Id,
                    baocao => baocao.Id_DanhMucThietBi,
                    (dmtb, baocao) => new Join_Baocaodanhgia_thietbi
                    {
                        Id_Baocaodanhgia = baocao.Id,
                        Id_DanhMucThietBi = dmtb.Id,
                        Img = dmtb.Img,
                        SoSN = dmtb.Serial,
                        ThietBi_HoaChatHieuChuan = baocao.ThietBi_HoaChatHieuChuan,
                        TenThietBi = dmtb.TenThietBi,
                        MaThietBi = dmtb.MaThietBi,
                        NgayHieuChuan = baocao.NgayHieuChuan,
                        KetQuaHC = baocao.KetQuaHC,
                        KetLuan = baocao.KetLuan,
                        NgayHieuChuanKeTiep = baocao.NgayHieuChuanKeTiep,
                        NguoiTao = baocao.NguoiTao,
                        NgayTao = baocao.NgayTao,
                        Status = baocao.Status
                    }
                );

            if (!string.IsNullOrEmpty(lowerName))
            { 
                query = query.Where(m => m.MaThietBi.ToLower().Contains(lowerName) ||
                                          m.TenThietBi.ToLower().Contains(lowerName));
            }
             
            var groupedList = query
                .GroupBy(m => m.Id_Baocaodanhgia)
                .Select(g => g.FirstOrDefault())
                .OrderByDescending(m => m.Id_Baocaodanhgia);

            return groupedList.ToPagedList(pageNumber, pageSize);
        } 
        public IEnumerable<BaoCaoDanhGia> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.BaoCaoDanhGias
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public List<BaoCaoDanhGia> getList(string status = "All")
        {
            List<BaoCaoDanhGia> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.BaoCaoDanhGias.Where(m => m.Status != 0).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.BaoCaoDanhGias.ToList();
                        break;
                    }
            }
            return list;
        }
        public List<Join_Baocaodanhgia_thietbi> getlistDetails(int? id)
        {
            var query = dbContext.BaoCaoDanhGias
                .AsNoTracking()  
                .Where(baocao => baocao.Status == 1 && baocao.Id == id)
                .Select(baocao => new Join_Baocaodanhgia_thietbi
                {
                    Id_Baocaodanhgia = baocao.Id,
                    Id_DanhMucThietBi = baocao.DanhMucThietBi.Id,
                    Img = baocao.DanhMucThietBi.Img,
                    SoSN = baocao.DanhMucThietBi.Serial,
                    ThietBi_HoaChatHieuChuan = baocao.ThietBi_HoaChatHieuChuan,
                    TenThietBi = baocao.DanhMucThietBi.TenThietBi,
                    MaThietBi = baocao.DanhMucThietBi.MaThietBi,
                    NgayHieuChuan = baocao.NgayHieuChuan,
                    KetQuaHC = baocao.KetQuaHC,
                    KetLuan = baocao.KetLuan,
                    NgayHieuChuanKeTiep = baocao.NgayHieuChuanKeTiep,
                    NguoiTao = baocao.NguoiTao,
                    NgayTao = baocao.NgayTao,
                    Status = baocao.Status
                });

            return query.ToList();
        }

        public List<BaoCaoDanhGia> getListAll()
        {
            List<BaoCaoDanhGia> List = null;
            List = dbContext.BaoCaoDanhGias.Where(m => m.Status == 1).ToList();
            return List;

        }
        public BaoCaoDanhGia getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.BaoCaoDanhGias.Where(m => m.Id == id).FirstOrDefault();
            }
        }

        public int Insert(BaoCaoDanhGia company)
        {
            dbContext.BaoCaoDanhGias.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(BaoCaoDanhGia company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(BaoCaoDanhGia company)
        {
            dbContext.BaoCaoDanhGias.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
