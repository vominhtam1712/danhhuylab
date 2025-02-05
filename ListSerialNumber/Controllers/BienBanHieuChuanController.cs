using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Kernel.Geom;
using ListClass.Dao;
using ListClass.Model;
using SelectPdf;

namespace ListSerialNumber.Controllers
{
    public class BienBanHieuChuanController : AccountLoginController
    {
        private readonly PhanCongDAO phancongDAO = new PhanCongDAO();
        private readonly BienBanHieuChuanDAO bienbanhieuchuanDAO = new BienBanHieuChuanDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        private readonly ChuanSuDungDAO chuansudungDAO = new ChuanSuDungDAO();
        private readonly KetQuaHieuChinhDAO ketquahieuchinhDAO = new KetQuaHieuChinhDAO();
        private readonly GiaTriTruocHieuChinhDAO giatritruochieuchinhDAO = new GiaTriTruocHieuChinhDAO();
        private readonly NgayTaoTechnicialBuldTruocHieuChinhDAO ngaytaotruoc = new NgayTaoTechnicialBuldTruocHieuChinhDAO();
        private readonly NgayTaoTechnicialBuldKetQuaHieuChinhDAO ngaytaosau = new NgayTaoTechnicialBuldKetQuaHieuChinhDAO();
        private readonly PhieuyeucauhieuchuanDAO pychcDAO = new PhieuyeucauhieuchuanDAO();
        private readonly PhieuNhanThietBiDAO pctbDAO = new PhieuNhanThietBiDAO();
        private readonly ListNumberDAO lnbDAO = new ListNumberDAO();
        private readonly KhachHangDAO khDAO = new KhachHangDAO(); 
        private readonly STD_DUT_DAO std_dut_DAO = new STD_DUT_DAO(); 
        private readonly BanTinhDKDBD_DAO dkdbdDAO = new BanTinhDKDBD_DAO(); 
        private readonly PhanCongGhiChuDAO pcgchuDAO = new PhanCongGhiChuDAO();  
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = bienbanhieuchuanDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền xóa ";
                return RedirectToReferrer();
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(List<BienBanHieuChuan> ids)
        {
            var id = ids.FirstOrDefault()?.Id_PhanCong;
            foreach (BienBanHieuChuan BienBanHieuChuan in ids)
            {
                var oldlist = dbContext.BienBanHieuChuans.Find(BienBanHieuChuan.Id);
                dbContext.BienBanHieuChuans.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { id });
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThucHien"))
            {
                var phanCong = phancongDAO.getListId(id).FirstOrDefault();
                if (phanCong == null)
                {
                    TempData["Message"] = "Không tìm thấy thông tin ";
                    return RedirectToReferrer();
                } 
                var idbienban = bienbanhieuchuanDAO.getListId(phanCong?.Id).FirstOrDefault();
                var bienBanHieuChuanList = bienbanhieuchuanDAO.getListId(phanCong?.Id).ToList();
                var chuansudungList = chuansudungDAO.getListId(phanCong?.Id).ToList();
                var pcgc = pcgchuDAO.getListId(phanCong?.Id).ToList();
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
                    BanTinhDKDBDs = dkdbd,
                    PhanCongGhiChus = pcgc,

                };

                return View("Details", model);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        } 
        public ActionResult Taobienbanhieuchuan(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = phancongDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers); 
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm ";
                return RedirectToReferrer();
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Taobienbanhieuchuan(List<BienBanHieuChuan> bienbanhieuchuans)
        {
            var Id = bienbanhieuchuans.First().Id_PhanCong;
            if (bienbanhieuchuans == null || !bienbanhieuchuans.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            }
            foreach (var phancong in bienbanhieuchuans)
            {
                if(bienbanhieuchuanDAO.CheckSerialNumberExists(phancong.Id_PhanCong))
                {
                    TempData["Message"] = "Biên bản hiệu chuẩn đã tồn tại";
                    return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
                }    
                var oldlist = new BienBanHieuChuan
                {
                    Id_PhanCong = phancong.Id_PhanCong,
                    MaBienBan = bienbanhieuchuanDAO.Layphieutudong(),
                    NhietDo = phancong.NhietDo,
                    DoAm = phancong.DoAm,
                    TenHieuChuan = phancong.TenHieuChuan,
                    SN = phancong.SN,
                    Cal_Date = phancong.Cal_Date,
                    Due_Day = phancong.Due_Day,
                    NgayThucHien = phancong.NgayThucHien,
                    NgayTao = DateTime.Now,
                    NguoiTao = Session["Name"].ToString(),
                    Status = 1
                };
                bienbanhieuchuanDAO.Insert(oldlist);
            }
           
            TempData["Message"] = "Tạo thành công";
            return RedirectToAction("Details", "BienBanHieuChuan", new { Id });
        }
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaThucHien"))
            {
                var idList = ParseIds(ids);
                var listNumbers = bienbanhieuchuanDAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                return View(listNumbers);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền sửa";
                return RedirectToReferrer();
            }
           

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<BienBanHieuChuan> bienbanhieuchuans)
        {
            if (bienbanhieuchuans == null || !bienbanhieuchuans.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = bienbanhieuchuans.Select(b => b.Id).ToList();

                var existingRecords = dbContext.BienBanHieuChuans
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var bienBanHieuChuan in bienbanhieuchuans)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == bienBanHieuChuan.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_PhanCong = bienBanHieuChuan.Id_PhanCong;
                        existingRecord.MaBienBan = bienBanHieuChuan.MaBienBan;
                        existingRecord.NhietDo = bienBanHieuChuan.NhietDo;
                        existingRecord.DoAm = bienBanHieuChuan.DoAm;
                        existingRecord.TenHieuChuan = bienBanHieuChuan.TenHieuChuan;
                        existingRecord.SN = bienBanHieuChuan.SN;
                        existingRecord.NgayThucHien = bienBanHieuChuan.NgayThucHien;
                        existingRecord.Cal_Date = bienBanHieuChuan.Cal_Date;
                        existingRecord.Due_Day = bienBanHieuChuan.Due_Day;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                var id = bienbanhieuchuans.First().Id_PhanCong;
                TempData["Message"] = "Cập nhật thành công";
                return RedirectToAction("Details", "BienBanHieuChuan", new { Id = id });
            }
        } 
    }
}
