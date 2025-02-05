using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_MaSoMau_PhieuChuyenMauHieuChuan_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<Join_MaSoMau_PhieuChuyenMauHieuChuan> getlistjoin(int? id)
        {
            var result = (from mspcm in dbContext.MaSoPhieuChuyenMaus
                          join pcmhc in dbContext.PhieuChuyenMauHieuChuans
                              on mspcm.Id equals pcmhc.Id_MaSoPhieuChuyenMau
                          join pntb in dbContext.PhieuNhanThietBis
                          on pcmhc.Id_PhieuNhanThietBi equals pntb.Id
                          select new Join_MaSoMau_PhieuChuyenMauHieuChuan
                          {
                              Id_MSM = mspcm.Id,
                              Id_PCM = pcmhc.Id,
                              MaSoMau = mspcm.MaSoMau,
                              NgayNhan = pcmhc.NgayNhan,
                              NguoiNhan = pcmhc.NguoiNhan,
                              SoNhanDang = pntb.SoNhanDang,
                              ThongSoHieuChuan = pcmhc.ThongSoHieuChuan,
                              NguoiDuocPhanCong = pcmhc.NguoiDuocPhanCong,
                              PhuongPhamHieuChuan = pcmhc.PhuongPhamHieuChuan,
                              NgayTraBaoCao = pcmhc.NgayTraBaoCao,
                              NgayTaoPhieu_PCM = pcmhc.NgayTaoPhieu,
                              NguoiTao = pcmhc.NguoiTao,
                              Status = pcmhc.Status

                          })
                          .Where(m => m.Id_MSM == id && m.Status==1)
                .GroupBy(m => m.SoNhanDang)
                .Select(g => g.OrderByDescending(m => m.NgayTraBaoCao).FirstOrDefault())
                .ToList();
            return result;
        }
        public List<Join_MaSoMau_PhieuChuyenMauHieuChuan> getlistTrash(int? id)
        {
            var result = (from mspcm in dbContext.MaSoPhieuChuyenMaus
                          join pcmhc in dbContext.PhieuChuyenMauHieuChuans
                              on mspcm.Id equals pcmhc.Id_MaSoPhieuChuyenMau
                          join pntb in dbContext.PhieuNhanThietBis
                          on pcmhc.Id_PhieuNhanThietBi equals pntb.Id
                          select new Join_MaSoMau_PhieuChuyenMauHieuChuan
                          {
                              Id_MSM = mspcm.Id,
                              Id_PCM = pcmhc.Id,
                              MaSoMau = mspcm.MaSoMau,
                              NgayNhan = pcmhc.NgayNhan,
                              NguoiNhan = pcmhc.NguoiNhan,
                              SoNhanDang = pntb.SoNhanDang,
                              ThongSoHieuChuan = pcmhc.ThongSoHieuChuan,
                              NguoiDuocPhanCong = pcmhc.NguoiDuocPhanCong,
                              PhuongPhamHieuChuan = pcmhc.PhuongPhamHieuChuan,
                              NgayTraBaoCao = pcmhc.NgayTraBaoCao,
                              NgayTaoPhieu_PCM = pcmhc.NgayTaoPhieu,
                              NguoiTao = pcmhc.NguoiTao,
                              Status = pcmhc.Status

                          })
                          .Where(m => m.Id_MSM == id && m.Status != 1)
                .GroupBy(m => m.SoNhanDang)
                .Select(g => g.OrderByDescending(m => m.NgayTraBaoCao).FirstOrDefault())
                .ToList();
            return result;
        }
    }
}
