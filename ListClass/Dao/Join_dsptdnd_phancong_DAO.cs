using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_dsptdnd_phancong_DAO
    {
        ListDBContext context = new ListDBContext();
        public List<Join_dsptdnd_phancong> GetListJoin(int? id)
        {
            if (id == null) return new List<Join_dsptdnd_phancong>();

            var result = context.DanhSachPhieuTheoDoiNhietDos
                         .AsNoTracking()
                         .Where(ds => ds.ID_PhieuTheoDoiNhietDo == id)
                         .Join(
                             context.PhieuTheoDoiNhietDos.AsNoTracking(),
                             ds => ds.ID_PhieuTheoDoiNhietDo,
                             p => p.Id,
                             (ds, p) => new Join_dsptdnd_phancong
                             {
                                 ID_dsptdnd = ds.ID,
                                 ID_PhieuTheoDoiNhietDo = ds.ID_PhieuTheoDoiNhietDo,
                                 NhietDo_Sang = ds.NhietDo_Sang,
                                 NhietDo_Chieu = ds.NhietDo_Chieu,
                                 Ngay_theo_doi = ds.Ngay_theo_doi,
                                 DoAm_Chieu = ds.DoAm_Chieu,
                                 DoAm_Sang = ds.DoAm_Sang,
                                 KetLuan = ds.KetLuan,
                                 NguoiKiemTra = ds.NguoiKiemTra,
                                 Ngay = ds.Ngay,
                                 status = ds.status
                             })
                         .ToList();

            return result;
        }
    }
}
