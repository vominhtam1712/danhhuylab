using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class Join_Bienbanhieuchuan_DKDBD_DAO
    {
        ListDBContext _context = new ListDBContext();
        public List<Join_Bienbanhieuchuan_DKDBD> GetListJoin(int? id)
        {
            if (id == null) return new List<Join_Bienbanhieuchuan_DKDBD>();

            var result = _context.BienBanHieuChuans
                         .AsNoTracking()
                         .Where(bbhc => bbhc.Id == id)
                         .Join(
                             _context.BanTinhDKDBDs.AsNoTracking(),
                             bbhc => bbhc.Id,
                             dkdbd => dkdbd.Id_BienBanHieuChuan,
                             (bbhc, dkdbd) => new Join_Bienbanhieuchuan_DKDBD
                             {
                                 Id_DKDBD = dkdbd.Id,
                                 Id_BienBanHieuChuan = dkdbd.Id_BienBanHieuChuan,
                                 MaBienBan = bbhc.MaBienBan,
                                 U_a = dkdbd.U_a,
                                 U_d = dkdbd.U_d,
                                 U_cer = dkdbd.U_cer,
                                 U_od_std = dkdbd.U_od_std,
                                 U_od = dkdbd.U_od,
                                 U_dd = dkdbd.U_dd,
                                 U_c = dkdbd.U_c,
                                 K = dkdbd.K,
                                 U_morong = dkdbd.U_morong,
                                 Saiso = dkdbd.Saiso,
                                 DoDKDB = dkdbd.DoDKDB
                             })
                         .ToList();

            return result;
        } 
    }
}
