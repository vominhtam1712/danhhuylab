using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListClass.Dao;
using ListClass.Model;

namespace ListSerialNumber.Controllers
{
    public class TB_STDController : AccountLoginController
    {
        private readonly Join_STD_DUT_Bienbanhieuchuan_DAO sp = new Join_STD_DUT_Bienbanhieuchuan_DAO();
        private readonly TB_STD_DAO TB_STD_DAO = new TB_STD_DAO();
        private readonly STD_DUT_DAO std_dut_DAO = new STD_DUT_DAO();
        private readonly BienBanHieuChuanDAO bbhcDAO = new BienBanHieuChuanDAO();
        private readonly PhanCongDAO pcDAO = new PhanCongDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        private readonly BanTinhDKDBD_DAO banTinhDKDBD_DAO = new BanTinhDKDBD_DAO();
        private readonly Join_STD_DUT_Bienbanhieuchuan_DAO join_STD_DUT_BIENBAN = new Join_STD_DUT_Bienbanhieuchuan_DAO(); 
        public ActionResult Tao_TB_STD(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien")) 
            {
                var idList = ParseIds(ids);
                var listNumbers = std_dut_DAO.GetRowsByIds(idList);
                var id = listNumbers.FirstOrDefault()?.Id_BienBanHieuChuan;
                var id_bb = bbhcDAO.getListIdbienban(id);
                var id_pc = id_bb.FirstOrDefault()?.Id_PhanCong;
                ViewBag.id = id_pc;
                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            }    
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tao_TB_STD(List<TB_STD> TB_STDs)
        {
            if (TB_STDs == null || !TB_STDs.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            }

            try
            {
                var creatorName = Session["Name"]?.ToString();
                if (creatorName == null)
                {
                    TempData["Message"] = "Tên người tạo không hợp lệ.";
                    return RedirectToAction("ThucHien", "PhanCong");
                }
                foreach (var std in TB_STDs)
                {
                    var id_tb = std.Id_BienBanHieuChuan;
                    var muckiemtra = std.MucKiemTra;
                    float slope = sp.CalculateSlope(id_tb);
                    float? trungBinhCong_STD = sp.TrungBinhCong_STD(id_tb);
                    float? trungBinhCong_DUT = sp.TrungBinhCong_DUT(id_tb);

                    if (!trungBinhCong_STD.HasValue || !trungBinhCong_DUT.HasValue)
                    {
                        TempData["Message"] = "Dữ liệu tính toán không hợp lệ";
                        return RedirectToAction("ThucHien", "PhanCong");
                    }
                     
                    float intercept = trungBinhCong_STD.Value - (slope * trungBinhCong_DUT.Value);
                    float? trungBinh = std.TrungBinh_STD * slope + intercept;
                    var oldlist = new TB_STD
                    {
                        ID_STD = std.ID_STD,
                        Id_BienBanHieuChuan = std.Id_BienBanHieuChuan,
                        MucKiemTra = std.MucKiemTra,
                        TrungBinh_STD = trungBinh,
                        NguoiTao = Session["Name"].ToString(),
                        Status = 1
                    };
                    TB_STD_DAO.Insert(oldlist);

                    var u_a = banTinhDKDBD_DAO.Create_Ua(oldlist.Id_BienBanHieuChuan, oldlist.MucKiemTra);
                    var U_d = banTinhDKDBD_DAO.Create_Ud(oldlist.Id_BienBanHieuChuan, oldlist.MucKiemTra);
                    var U_cer = banTinhDKDBD_DAO.Create_U_cer(oldlist.Id_BienBanHieuChuan, oldlist.MucKiemTra);
                    var U_od_std = banTinhDKDBD_DAO.Create_U_od_std(oldlist.Id_BienBanHieuChuan, oldlist.MucKiemTra);
                    var U_od = banTinhDKDBD_DAO.Create_U_od(oldlist.Id_BienBanHieuChuan, oldlist.MucKiemTra);

                    if (u_a == null || U_od == null || U_d == null)
                    {
                        TempData["Message"] = "Có lỗi trong quá trình tính toán.";
                        return RedirectToAction("ThucHien", "PhanCong");
                    }

                    float Dodongdeu = ((slope - intercept) / trungBinhCong_STD.Value) * 100;
                    float Sqrt3 = (float)Math.Sqrt(3);
                    float? U_dd = Dodongdeu / 2 / Sqrt3;
                    float? U_c = (float)Math.Sqrt((u_a * u_a)
                        + (U_d * U_d)
                        + (U_cer * U_cer)
                        + (U_od_std * U_od_std)
                        + (U_od * U_od));
                    float? k = 2;
                    float? morong = U_c * k;
                    float? saiso = banTinhDKDBD_DAO.Create_saiso(oldlist.Id_BienBanHieuChuan, oldlist.MucKiemTra);
                    float? dodkdb = morong.HasValue ? (float)Math.Ceiling(morong.Value) : 0;
                    var banTinhEntry = new BanTinhDKDBD
                    {
                        Id_TB_STD = oldlist.ID, 
                        Id_BienBanHieuChuan = std.Id_BienBanHieuChuan,
                        MucKiemTra = std.MucKiemTra,
                        U_a = u_a, 
                        U_d = U_d,
                        U_cer = U_cer,
                        U_od_std = U_od_std,
                        U_od = U_od,
                        U_dd = U_dd,
                        U_c = U_c,
                        K = k,
                        U_morong = morong,
                        Saiso = saiso,
                        DoDKDB = dodkdb,
                        NguoiTao = Session["Name"].ToString(),
                        Status = 1
                    };

                    banTinhDKDBD_DAO.Insert(banTinhEntry);
                }

                TempData["Message"] = "Tạo thành công";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Đã xảy ra lỗi: {ex.Message}";
            }
            var ids = TB_STDs.FirstOrDefault()?.Id_BienBanHieuChuan;
            var id_bb = bbhcDAO.getListIdbienban(ids);
            var id = id_bb.FirstOrDefault()?.Id_PhanCong;
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        }
        public ActionResult XoaNhieu(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Không có dữ liệu";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = TB_STD_DAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa";
                return RedirectToReferrer();
            }    
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaNhieu(List<TB_STD> ids)
        {
            var id = ids.FirstOrDefault()?.Id_BienBanHieuChuan;
            foreach (TB_STD TB_STD in ids)
            {
                var oldlist = dbContext.TB_STDs.Find(TB_STD.ID);
                dbContext.TB_STDs.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index", "TB_STD", new { id });
        } 
    }
}
