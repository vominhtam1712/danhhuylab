using ListClass.Dao;
using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class LuuInHoSoController : AccountLoginController
    {
        private readonly Join_PhanCong_NhanVien_DAO joinphancongnhanvienDAO = new Join_PhanCong_NhanVien_DAO();
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly BienBanHieuChuanDAO bienbanhieuchuanDAO = new BienBanHieuChuanDAO();
        private readonly Join_STD_DUT_PhanCong_DAO join = new Join_STD_DUT_PhanCong_DAO();
        private readonly STD_DUT_DAO std_dut_DAO = new STD_DUT_DAO();
        private readonly Join_STD_DUT_Bienbanhieuchuan_DAO sp = new Join_STD_DUT_Bienbanhieuchuan_DAO();
        private readonly Join_BanTinhDKDBD_PhanCong_DAO join_bt = new Join_BanTinhDKDBD_PhanCong_DAO();
        private readonly Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD_DAO Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD_DAO = new Join_SDT_DUT_BBHC_PC_PYCHC_PNTB_LISTNUMBER_DKDBD_DAO();
        private readonly TB_STD_DAO TB_STD_DAO = new TB_STD_DAO();
        private readonly ChuanSuDungDAO chuansudungDAO = new ChuanSuDungDAO();
        private readonly GiaTriTruocHieuChinhDAO giatritruochieuchinhDAO = new GiaTriTruocHieuChinhDAO();
        private readonly KetQuaHieuChinhDAO ketquahieuchinhDAO = new KetQuaHieuChinhDAO();
        private readonly NgayTaoTechnicialBuldTruocHieuChinhDAO ngaytaotruoc = new NgayTaoTechnicialBuldTruocHieuChinhDAO();
        private readonly NgayTaoTechnicialBuldKetQuaHieuChinhDAO ngaytaosau = new NgayTaoTechnicialBuldKetQuaHieuChinhDAO();
        private readonly PhieuyeucauhieuchuanDAO pychcDAO = new PhieuyeucauhieuchuanDAO();
        private readonly PhieuNhanThietBiDAO pctbDAO = new PhieuNhanThietBiDAO();
        private readonly ListNumberDAO lnbDAO = new ListNumberDAO();
        private readonly KhachHangDAO khDAO = new KhachHangDAO();
        private readonly BanTinhDKDBD_DAO dkdbdDAO = new BanTinhDKDBD_DAO();
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("LuuInHoSo"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1; 
                var count = joinphancongnhanvienDAO.GetCountIndex();
                var countIndex = joinphancongnhanvienDAO.GetCountTrash();
                var countTrash = joinphancongnhanvienDAO.GetCountIndex1();
                int totalPages = (int)Math.Ceiling((double)count / pageSize); 
                ViewBag.Count = count;
                ViewBag.CountIndex = countIndex;
                ViewBag.CountTrash = countTrash;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber; 
                var list = joinphancongnhanvienDAO.getlistLuuInHoSo(Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập ";
                return RedirectToReferrer();
            }    
            
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("LuuInHoSo")) 
            {
                var phanCong = phancongDAO.getListId(id).FirstOrDefault();
                if (phanCong == null)
                {
                    TempData["Message"] = "Không tìm thấy thông tin phân công";
                    return RedirectToReferrer();
                } 
                var idbienban = bienbanhieuchuanDAO.getListId(phanCong?.Id).FirstOrDefault();
                var bienBanHieuChuanList = bienbanhieuchuanDAO.getListId(phanCong?.Id).ToList();
                var chuansudungList = chuansudungDAO.getListId(phanCong?.Id).ToList();
                var giatritruochieuchinhList = giatritruochieuchinhDAO.getListId(phanCong?.Id).ToList();
                var ketquahieuchinhList = ketquahieuchinhDAO.getListId(phanCong?.Id).ToList();
                var ngaytaotechnicialbuldtruochieuchinh = ngaytaotruoc.getListId(phanCong?.Id).ToList();
                var ngaytaotechnicialbuldketquahieuchinh = ngaytaosau.getListId(phanCong?.Id).ToList();
                var yeucauhieuchuan = pychcDAO.getListId(phanCong?.Id_pychc).FirstOrDefault();
                var phieuyeucauhieuchuan = pychcDAO.getListId(yeucauhieuchuan?.Id).ToList();
                var nhanthietbi = pctbDAO.getListId(yeucauhieuchuan?.Id_phieunhanthietbi).FirstOrDefault();
                var phieuNhanThietBi = pctbDAO.getListId(nhanthietbi?.Id).ToList();
                var listnumber = lnbDAO.getListId(nhanthietbi?.Id_ListNumber).FirstOrDefault();
                var listNumbers = lnbDAO.getListId(listnumber?.Id).ToList();
                var kh = khDAO.getListId(listnumber?.Id_KhachHang).ToList();
                var std_dut = std_dut_DAO.getListId(bienBanHieuChuanList.FirstOrDefault()?.Id).ToList();
                var dkdbd = dkdbdDAO.getList(idbienban?.Id).ToList(); 
                var model = new PhanCong_HieuChinh_HieuChuan
                {
                    PhanCong = phanCong,
                    BienBanHieuChuans = bienBanHieuChuanList,
                    ChuanSuDungs = chuansudungList,
                    GiaTriTruocHieuChinhs = giatritruochieuchinhList,
                    KetQuaHieuChinhs = ketquahieuchinhList,
                    NgayTaoTechnicialBuldTruocHieuChinhs = ngaytaotechnicialbuldtruochieuchinh,
                    NgayTaoTechnicialBuldKetQuaHieuChinhs = ngaytaotechnicialbuldketquahieuchinh,
                    Phieuyeucauhieuchuans = phieuyeucauhieuchuan,
                    PhieuNhanThietBis = phieuNhanThietBi,
                    ListNumbers = listNumbers,
                    KhachHangs = kh,
                    STD_DUTs = std_dut,
                    BanTinhDKDBDs = dkdbd 
                };
                float slope = sp.CalculateSlope(idbienban.Id);
                float? trungBinhCong_STD = sp.TrungBinhCong_STD(idbienban.Id);
                float? trungBinhCong_DUT = sp.TrungBinhCong_DUT(idbienban.Id); 
                if (!trungBinhCong_STD.HasValue || !trungBinhCong_DUT.HasValue)
                {
                    TempData["Message"] = "Dữ liệu tính toán không hợp lệ";
                    return RedirectToReferrer();
                } 
                float intercept = trungBinhCong_STD.Value - (slope * trungBinhCong_DUT.Value); 
                if (trungBinhCong_STD.Value == 0)
                {
                    TempData["Message"] = "Giá trị trung bình không hợp lệ để tính toán độ đồng đều.";
                    return RedirectToReferrer();
                } 
                float Dodongdeu = ((slope - intercept) / trungBinhCong_STD.Value) * 100; 
                ViewBag.Slope = slope.ToString("F3");
                ViewBag.Intercept = intercept.ToString("F3");
                ViewBag.DoDongDeu = Dodongdeu.ToString("F3"); 
                return View("Details", model);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập ";
                return RedirectToReferrer();
            }     
        } 
    }
}