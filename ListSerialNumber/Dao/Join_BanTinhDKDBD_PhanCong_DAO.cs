using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_BanTinhDKDBD_PhanCong_DAO
    {
        ListDBContext context = new ListDBContext();
        public List<Join_BanTinhDKDBD_PhanCong> GetListJoin(int? id)
        { 
            var result = (from bt in context.BanTinhDKDBDs.AsNoTracking()
                          join bbhc in context.BienBanHieuChuans.AsNoTracking()
                              on bt.Id_BienBanHieuChuan equals bbhc.Id
                          join pc in context.PhanCongs.AsNoTracking()
                          on bbhc.Id_PhanCong equals pc.Id
                          where bbhc.Id == id 
                          select new Join_BanTinhDKDBD_PhanCong
                          {
                              Id = bt.Id,
                              Id_PhanCong = pc.Id,
                              Id_BienBanHieuChuan = bbhc.Id,
                              MucKiemTra = bt.MucKiemTra,
                              U_a = bt.U_a,
                              U_d = bt.U_d,
                              U_cer = bt.U_cer,
                              U_od_std = bt.U_od_std,
                              U_od = bt.U_od,
                              U_dd = bt.U_dd,
                              U_c = bt.U_c,
                              K = bt.K,
                              U_morong = bt.U_morong,
                              Saiso = bt.Saiso,
                              DoDKDB = bt.DoDKDB,
                              NguoiTao = bt.NguoiTao,
                              Status = bt.Status
                          })
                          .ToList();
            return result;
        }

    }
}
