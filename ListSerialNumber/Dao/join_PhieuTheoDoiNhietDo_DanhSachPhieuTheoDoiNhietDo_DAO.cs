using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo_DAO
    {
        ListDBContext _context = new ListDBContext();
        public List<join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo> GetListJoin(int? ids)
        {
            var query = _context.PhieuTheoDoiNhietDos
                                .Join(_context.DanhSachPhieuTheoDoiNhietDos,
                                      phieu => phieu.Id,
                                      danhSach => danhSach.ID_PhieuTheoDoiNhietDo,
                                      (phieu, danhSach) => new { phieu, danhSach })
                                .Where(x => !ids.HasValue || x.phieu.Id == ids.Value)
                                .Select(x => new join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo
                                {
                                    Id_PhieuTheoDoiNhietDo = x.phieu.Id,
                                    MaPhieuTheoDoiNhietDo = x.phieu.MaPhieuTheoDoiNhietDo,
                                    MSNhietKe_Model_No = x.phieu.MSNhietKe_Model_No,
                                    Phong = x.phieu.Phong,
                                    Thang_Nam = x.phieu.Thang_Nam,
                                    Ngay_kiem_tra = x.danhSach.Ngay_theo_doi,
                                    NhietDo_Dau = x.phieu.NhietDo_Dau,
                                    NhietDo_Sau = x.phieu.NhietDo_Sau,
                                    DoAm = x.phieu.DoAm,
                                    ID_DanhSachPhieuTheoDoiNhietDo = x.danhSach.ID,
                                    NhietDo_Sang = x.danhSach.NhietDo_Sang,
                                    DoAm_Sang = x.danhSach.DoAm_Sang,
                                    NhietDo_Chieu = x.danhSach.NhietDo_Chieu,
                                    DoAm_Chieu = x.danhSach.DoAm_Chieu,
                                    KetLuan = x.danhSach.KetLuan,
                                    NguoiKiemTra = x.danhSach.NguoiKiemTra
                                })
                                .ToList();

            return query;
        }
    }
}
