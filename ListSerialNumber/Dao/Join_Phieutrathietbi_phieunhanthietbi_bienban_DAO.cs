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
using System.Xml.Linq;

namespace ListClass.Dao
{
    public class Join_Phieutrathietbi_phieunhanthietbi_bienban_DAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<Join_Phieutrathietbi_phieunhanthietbi_bienban> getlistjoin(int? ids)
        {
            var list = (from listnumber in dbContext.ListNumbers
                        join kh in dbContext.KhachHangs on listnumber.Id_KhachHang equals kh.Id
                        join phieunhan in dbContext.PhieuNhanThietBis on listnumber.Id equals phieunhan.Id_ListNumber
                        join bienbanbangiao in dbContext.BienBanBanGiaoNhanThietBis on phieunhan.Id_BienBanBanGiaoNhanThietBi equals bienbanbangiao.Id
                        join phieuyeucau in dbContext.Phieuyeucauhieuchuans on phieunhan.Id equals phieuyeucau.Id_phieunhanthietbi
                        join phancong in dbContext.PhanCongs on phieuyeucau.Id equals phancong.Id_pychc
                        join bienbanhieuchuan in dbContext.BienBanHieuChuans on phancong.Id equals bienbanhieuchuan.Id_PhanCong
                        join std_dut in dbContext.STD_DUTs on bienbanhieuchuan.Id equals std_dut.Id_BienBanHieuChuan
                        join tb_std in dbContext.TB_STDs on std_dut.ID equals tb_std.ID_STD
                        join dkdbd in dbContext.BanTinhDKDBDs on tb_std.ID equals dkdbd.Id_TB_STD
                        join gcn in dbContext.Giaychungnhans on bienbanhieuchuan.Id equals gcn.Id_BienBanHieuChuan
                        join phieutra in dbContext.Phieutrathietbis on gcn.Id equals phieutra.Id_Giaychungnhan
                        join phieutra_form in dbContext.PhieuTra_forms on phieutra.Id_PhieuTra_Form equals phieutra_form.Id
                        where phieutra_form.Id == ids
                        select new Join_Phieutrathietbi_phieunhanthietbi_bienban
                        {
                            Id_PhieuTra = phieutra.Id,
                            Id_PhieuTra_Form = phieutra_form.Id,
                            Id_GiayCN = gcn.Id,
                            MaGCN = gcn.MaGCN,
                            Id_bienbannhan = bienbanbangiao.Id,
                            MaPhieuTra = phieutra_form.MaPhieuTra,
                            SoYeuCau = bienbanbangiao.SoYeuCau,
                            NgayThucHien = phieutra.NgayThucHien,
                            KhachHang = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            TenThietBi = listnumber.Tenthietbi,
                            Hang = listnumber.Hang,
                            Model = listnumber.Model,
                            SerialNumber = listnumber.SerialNumber,
                            SoNhanDang = phieunhan.SoNhanDang,
                            PhuongThucGiaoTra = phieutra.PhuongThucGiaoTra,
                            GhiChu = phieutra.Ghichu,
                            NgayTao = phieutra.NgayTao,
                            TrangThaiThietBi = phieutra.TrangThaiThietbi,
                            NguoiTao = phieutra.NguoiTao,
                            Status = phieutra.status,
                        });
            var groupedList = list.GroupBy(m => m.Id_GiayCN)
                          .Select(g => g.FirstOrDefault());
            groupedList = groupedList.OrderByDescending(m => m.Id_PhieuTra);
            return groupedList.ToList();
        }
        public List<Join_Phieutrathietbi_phieunhanthietbi_bienban> GetRowsByIds(List<int> ids)
        {
            var list = (from listnumber in dbContext.ListNumbers
                        join phieunhan in dbContext.PhieuNhanThietBis on listnumber.Id equals phieunhan.Id_ListNumber
                        join kh in dbContext.KhachHangs on listnumber.Id_KhachHang equals kh.Id
                        join bienbanbangiao in dbContext.BienBanBanGiaoNhanThietBis on phieunhan.Id_BienBanBanGiaoNhanThietBi equals bienbanbangiao.Id
                        join phieuyeucau in dbContext.Phieuyeucauhieuchuans on phieunhan.Id equals phieuyeucau.Id_phieunhanthietbi
                        join phancong in dbContext.PhanCongs on phieuyeucau.Id equals phancong.Id_pychc
                        join bienbanhieuchuan in dbContext.BienBanHieuChuans on phancong.Id equals bienbanhieuchuan.Id_PhanCong
                        join std_dut in dbContext.STD_DUTs on bienbanhieuchuan.Id equals std_dut.Id_BienBanHieuChuan
                        join tb_std in dbContext.TB_STDs on std_dut.ID equals tb_std.ID_STD
                        join dkdbd in dbContext.BanTinhDKDBDs on tb_std.ID equals dkdbd.Id_TB_STD
                        join gcn in dbContext.Giaychungnhans on bienbanhieuchuan.Id equals gcn.Id_BienBanHieuChuan
                        join phieutra in dbContext.Phieutrathietbis on gcn.Id equals phieutra.Id_Giaychungnhan
                        join phieutra_form in dbContext.PhieuTra_forms on phieutra.Id_PhieuTra_Form equals phieutra_form.Id
                        select new Join_Phieutrathietbi_phieunhanthietbi_bienban
                        {
                            Id_PhieuTra = phieutra.Id,
                            Id_PhieuTra_Form = phieutra_form.Id,
                            Id_GiayCN = gcn.Id,
                            MaGCN = gcn.MaGCN,
                            Id_bienbannhan = bienbanbangiao.Id,
                            MaPhieuTra = phieutra_form.MaPhieuTra,
                            SoYeuCau = bienbanbangiao.SoYeuCau,
                            NgayThucHien = phieutra.NgayThucHien,
                            KhachHang = kh.TenKH,
                            DiaChi = kh.DiaChi,
                            LienHe = kh.LienHe,
                            TenThietBi = listnumber.Tenthietbi,
                            Hang = listnumber.Hang,
                            Model = listnumber.Model,
                            SerialNumber = listnumber.SerialNumber,
                            SoNhanDang = phieunhan.SoNhanDang,
                            PhuongThucGiaoTra = phieutra.PhuongThucGiaoTra,
                            GhiChu = phieutra.Ghichu,
                            NgayTao = phieutra.NgayTao,
                            TrangThaiThietBi = phieutra.TrangThaiThietbi,
                            NguoiTao = phieutra.NguoiTao,
                            Status = phieutra.status,
                        });
            var groupedList = list.GroupBy(m => m.Id_PhieuTra)
                        .Select(g => g.FirstOrDefault());
            groupedList = groupedList.OrderByDescending(m => m.Id_PhieuTra);
            return groupedList.AsNoTracking()
                .Where(ln => ids.Contains(ln.Id_PhieuTra))
                .ToList();
        }
    }
}
