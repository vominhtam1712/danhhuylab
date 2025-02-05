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
    public class Join_BaoCaoSuaChua_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public PagedList.IPagedList<Join_BaoCaoSuaChua> getlistjoinIndex(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = dbContext.BaoCaoSuaChuas
                        .AsNoTracking()
                        .Where(bcsc => bcsc.Status == 1)
                        .Join(dbContext.Giaychungnhans, bcsc => bcsc.Id_giaychungnhan, gcn => gcn.Id, (bcsc, gcn) => new { bcsc, gcn })
                        .Join(dbContext.BienBanHieuChuans, temp => temp.gcn.Id_BienBanHieuChuan, bbhc => bbhc.Id, (temp, bbhc) => new { temp.bcsc, temp.gcn, bbhc })
                        .Join(dbContext.PhanCongs, temp => temp.bbhc.Id_PhanCong, pc => pc.Id, (temp, pc) => new { temp.bcsc, temp.gcn, temp.bbhc, pc })
                        .Join(dbContext.Phieuyeucauhieuchuans, temp => temp.pc.Id_pychc, pychc => pychc.Id, (temp, pychc) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, pychc })
                        .Join(dbContext.PhieuNhanThietBis, temp => temp.pychc.Id_phieunhanthietbi, pntb => pntb.Id, (temp, pntb) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, temp.pychc, pntb })
                        .Join(dbContext.ListNumbers, temp => temp.pntb.Id_ListNumber, lnb => lnb.Id, (temp, lnb) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, temp.pychc, temp.pntb, lnb })
                        .Join(dbContext.KhachHangs, temp => temp.lnb.Id_KhachHang, kh => kh.Id, (temp, kh) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, temp.pychc, temp.pntb, temp.lnb, kh })
                        .Select(temp => new Join_BaoCaoSuaChua
                        {
                            MaBaoCao = temp.bcsc.MaBaoCao,
                            Id_baocaosuachua = temp.bcsc.Id,
                            Id_giaychungnhan = temp.gcn.Id,
                            Tenthietbi = temp.lnb.Tenthietbi,
                            SoNhanDang = temp.pntb.SoNhanDang,
                            Model = temp.lnb.Model,
                            SN = temp.lnb.SerialNumber,
                            TenKH = temp.kh.TenKH,
                            Hang = temp.lnb.Hang,
                            Dul_date = temp.bbhc.Due_Day,
                            CacHangSuDung = temp.bcsc.CacHangSuDung,
                            NgayTao = temp.bcsc.NgayTao,
                            NguoiTao = temp.bcsc.NguoiTao,
                            Status = temp.bcsc.Status,
                            MaGCN = temp.gcn.MaGCN
                        });

            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.Tenthietbi.Contains(lowerName) ||
                                          m.Hang.Contains(lowerName) ||
                                          m.MaBaoCao.Contains(lowerName) ||
                                          m.Model.Contains(lowerName) ||
                                          m.TenKH.Contains(lowerName) ||
                                          m.SN.Contains(lowerName));
            }

            return query.OrderByDescending(m => m.Id_baocaosuachua).ToPagedList(pageNumber, pageSize);
        }

        public PagedList.IPagedList<Join_BaoCaoSuaChua> getlistjoinTrash(string Name, int pageSize, int pageNumber)
        {
            var lowerName = Name?.ToLower();

            var query = dbContext.BaoCaoSuaChuas
                        .AsNoTracking()
                        .Where(bcsc => bcsc.Status != 1)
                        .Join(dbContext.Giaychungnhans, bcsc => bcsc.Id_giaychungnhan, gcn => gcn.Id, (bcsc, gcn) => new { bcsc, gcn })
                        .Join(dbContext.BienBanHieuChuans, temp => temp.gcn.Id_BienBanHieuChuan, bbhc => bbhc.Id, (temp, bbhc) => new { temp.bcsc, temp.gcn, bbhc })
                        .Join(dbContext.PhanCongs, temp => temp.bbhc.Id_PhanCong, pc => pc.Id, (temp, pc) => new { temp.bcsc, temp.gcn, temp.bbhc, pc })
                        .Join(dbContext.Phieuyeucauhieuchuans, temp => temp.pc.Id_pychc, pychc => pychc.Id, (temp, pychc) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, pychc })
                        .Join(dbContext.PhieuNhanThietBis, temp => temp.pychc.Id_phieunhanthietbi, pntb => pntb.Id, (temp, pntb) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, temp.pychc, pntb })
                        .Join(dbContext.ListNumbers, temp => temp.pntb.Id_ListNumber, lnb => lnb.Id, (temp, lnb) => new { temp.bcsc, temp.gcn, temp.bbhc, temp.pc, temp.pychc, temp.pntb, lnb })
                        .Select(temp => new Join_BaoCaoSuaChua
                        {
                            MaBaoCao = temp.bcsc.MaBaoCao,
                            Id_baocaosuachua = temp.bcsc.Id,
                            Id_giaychungnhan = temp.gcn.Id,
                            Tenthietbi = temp.lnb.Tenthietbi,
                            SoNhanDang = temp.pntb.SoNhanDang,
                            Model = temp.lnb.Model,
                            SN = temp.lnb.SerialNumber,
                            Hang = temp.lnb.Hang,
                            Dul_date = temp.bbhc.Due_Day,
                            CacHangSuDung = temp.bcsc.CacHangSuDung,
                            NgayTao = temp.bcsc.NgayTao,
                            NguoiTao = temp.bcsc.NguoiTao,
                            Status = temp.bcsc.Status,
                            MaGCN = temp.gcn.MaGCN
                        });

            if (!string.IsNullOrEmpty(lowerName))
            {
                query = query.Where(m => m.Tenthietbi.Contains(lowerName) ||
                                          m.Hang.Contains(lowerName) ||
                                          m.MaBaoCao.Contains(lowerName) ||
                                          m.Model.Contains(lowerName) ||
                                          m.SN.Contains(lowerName));
            }

            return query.OrderByDescending(m => m.Id_baocaosuachua).ToPagedList(pageNumber, pageSize);
        }

        public List<Join_BaoCaoSuaChua> getlistDetails(int? ids)
        {
            if (ids == null) return new List<Join_BaoCaoSuaChua>();

            var query = (from bcsc in dbContext.BaoCaoSuaChuas
                         join gcn in dbContext.Giaychungnhans on bcsc.Id_giaychungnhan equals gcn.Id
                         join bbhc in dbContext.BienBanHieuChuans on gcn.Id_BienBanHieuChuan equals bbhc.Id
                         join pc in dbContext.PhanCongs on bbhc.Id_PhanCong equals pc.Id
                         join pychc in dbContext.Phieuyeucauhieuchuans on pc.Id_pychc equals pychc.Id
                         join pntb in dbContext.PhieuNhanThietBis on pychc.Id_phieunhanthietbi equals pntb.Id
                         join lnb in dbContext.ListNumbers on pntb.Id_ListNumber equals lnb.Id
                         join kh in dbContext.KhachHangs on lnb.Id_KhachHang equals kh.Id
                         where bcsc.Id == ids
                         select new Join_BaoCaoSuaChua
                         {
                             MaBaoCao = bcsc.MaBaoCao,
                             Id_baocaosuachua = bcsc.Id,
                             Id_giaychungnhan = gcn.Id,
                             Tenthietbi = lnb.Tenthietbi,
                             Model = lnb.Model,
                             SN = lnb.SerialNumber,
                             TenKH = kh.TenKH,
                             SoNhanDang = pntb.SoNhanDang,
                             Hang = lnb.Hang,
                             Dul_date = bbhc.Due_Day,
                             cal_date = bbhc.Cal_Date,
                             CacHangSuDung = bcsc.CacHangSuDung,
                             NgayTao = bcsc.NgayTao,
                             NguoiTao = bcsc.NguoiTao,
                             Status = bcsc.Status
                         }).AsNoTracking()
                        .FirstOrDefault();

            return query != null ? new List<Join_BaoCaoSuaChua> { query } : new List<Join_BaoCaoSuaChua>();
        }
        public IEnumerable<BaoCaoSuaChua> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.BaoCaoSuaChuas
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public List<BaoCaoSuaChua> getList(string status = "All")
        {
            IQueryable<BaoCaoSuaChua> query = dbContext.BaoCaoSuaChuas.AsNoTracking();

            if (status == "Index")
            {
                query = query.Where(m => m.Status != 0)
                             .OrderByDescending(m => m.Id);
            }
            else if (status != "All")
            {
                query = query.Where(m => m.Status.ToString() == status);
            }

            return query.ToList();
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.BaoCaoSuaChuas
                .Where(m => m.MaBaoCao.StartsWith("BCSC"))
                .Select(m => m.MaBaoCao)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "BCSC0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(4);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"BCSC{number:D4}";
                }
                else
                {
                    nextcode = $"BCSC{number}";
                }
            }
            return nextcode;
        }
        public async Task<int> GetCountIndex()
        {
            return await dbContext.BaoCaoSuaChuas.CountAsync(m => m.Status == 1);
        }

        public async Task<int> GetCountTrash()
        {
            return await dbContext.BaoCaoSuaChuas.CountAsync(m => m.Status != 1);
        }

        public async Task<List<BaoCaoSuaChua>> getListAll()
        {
            return await dbContext.BaoCaoSuaChuas.Where(m => m.Status == 1).ToListAsync();
        }

        public async Task<BaoCaoSuaChua> getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await dbContext.BaoCaoSuaChuas
                                  .Where(m => m.Id == id)
                                  .FirstOrDefaultAsync();
        }

        public async Task<int> Insert(BaoCaoSuaChua company)
        {
            dbContext.BaoCaoSuaChuas.Add(company);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> Update(BaoCaoSuaChua company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(BaoCaoSuaChua company)
        {
            dbContext.BaoCaoSuaChuas.Remove(company);
            return await dbContext.SaveChangesAsync();
        }
    }
}
