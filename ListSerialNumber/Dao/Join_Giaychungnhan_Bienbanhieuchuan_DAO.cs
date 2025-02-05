using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListClass.Dao
{
    public class Join_Giaychungnhan_Bienbanhieuchuan_DAO
    {
        ListDBContext context = new ListDBContext();
        public PagedList.IPagedList<Join_Giaychungnhan_Bienbanhieuchuan> GetListJoin(string Name, int pageSize, int pageNumber)
        { 
            var result = context.Giaychungnhans
                          .AsNoTracking()
                          .Join(context.BienBanHieuChuans.AsNoTracking(),
                                gcn => gcn.Id_BienBanHieuChuan,
                                bb => bb.Id,
                                (gcn, bb) => new { gcn, bb })
                          .Join(context.PhanCongs.AsNoTracking(),
                                gb => gb.bb.Id_PhanCong,
                                pc => pc.Id,
                                (gb, pc) => new { gb.gcn, gb.bb, pc })
                          .Join(context.Phieuyeucauhieuchuans.AsNoTracking(),
                                gpc => gpc.pc.Id_pychc,
                                pychc => pychc.Id,
                                (gpc, pychc) => new { gpc.gcn, gpc.bb, gpc.pc, pychc })
                          .Join(context.PhieuNhanThietBis.AsNoTracking(),
                                gpychc => gpychc.pychc.Id_phieunhanthietbi,
                                pntb => pntb.Id,
                                (gpychc, pntb) => new { gpychc.gcn, gpychc.bb, gpychc.pc, gpychc.pychc, pntb })
                          .Join(context.ListNumbers.AsNoTracking(),
                                gpntb => gpntb.pntb.Id_ListNumber,
                                lnb => lnb.Id,
                                (gpntb, lnb) => new Join_Giaychungnhan_Bienbanhieuchuan
                                {
                                    Id_GCN = gpntb.gcn.Id,
                                    Id_BienBanHieuChuan = gpntb.bb.Id,
                                    MaGCN = gpntb.gcn.MaGCN,
                                    SoNhanDang = gpntb.pntb.SoNhanDang,
                                    SerialNumber = lnb.SerialNumber,
                                    Tenthietbi = lnb.Tenthietbi,
                                    Model = lnb.Model,
                                    NguoiTao = gpntb.gcn.NguoiTao,
                                    NgayTao = gpntb.gcn.NgayTao,
                                    status = gpntb.gcn.status
                                });
             
            if (!string.IsNullOrEmpty(Name))
            {
                string lowerName = Name.ToLower();
                result = result.Where(m => (m.Tenthietbi ?? "").ToLower().Contains(lowerName)
                                        || (m.Model ?? "").ToLower().Contains(lowerName)
                                        || (m.SerialNumber ?? "").ToLower().Contains(lowerName)
                                        || (m.SoNhanDang ?? "").ToLower().Contains(lowerName)
                                        || (m.MaGCN ?? "").ToLower().Contains(lowerName));
            } 
            var List = result
               .GroupBy(m => m.Id_GCN)
               .Select(g => g.OrderByDescending(m => m.Id_GCN).FirstOrDefault())
               .OrderByDescending(m => m.Id_GCN)
               .ToPagedList(pageNumber, pageSize);

            return List;
        }
        public List<Join_Giaychungnhan_Bienbanhieuchuan> GetListJoin(int? ids)
        {
            if (ids == null) return new List<Join_Giaychungnhan_Bienbanhieuchuan>();

            var result = context.Giaychungnhans
                .AsNoTracking()
                .Where(gcn => gcn.Id == ids)
                .Join(context.BienBanHieuChuans.AsNoTracking(),
                      gcn => gcn.Id_BienBanHieuChuan,
                      bb => bb.Id,
                      (gcn, bb) => new Join_Giaychungnhan_Bienbanhieuchuan
                      {
                          Id_GCN = gcn.Id,
                          Id_BienBanHieuChuan = bb.Id,
                          MaGCN = gcn.MaGCN,
                          NguoiTao = gcn.NguoiTao,
                          NgayTao = gcn.NgayTao,
                          status = gcn.status
                      })
                .ToList();

            return result;
        }
    }
}
