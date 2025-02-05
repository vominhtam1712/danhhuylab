using ClosedXML.Excel;
using ListClass.Dao;
using ListClass.Model;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class GiaychungnhanController : AccountLoginController
    {
        private readonly GiaychungnhanDAO giaychungnhanDAO = new GiaychungnhanDAO();
        private readonly BanTinhDKDBD_DAO dkdbdoDAO = new BanTinhDKDBD_DAO();
        private readonly Join_Giaychungnhan_Bienbanhieuchuan_DAO join = new Join_Giaychungnhan_Bienbanhieuchuan_DAO();
        private readonly Join_Giaychungnhan_DAO join_Giaychungnhan_DAO = new Join_Giaychungnhan_DAO();
        private readonly BienBanHieuChuanDAO bienbanhieuchuanDAO = new BienBanHieuChuanDAO();
        private readonly ChuanSuDungDAO csdDAO = new ChuanSuDungDAO();
        private readonly NgayTaoTechnicialBuldTruocHieuChinhDAO ngaytaotruocDAO = new NgayTaoTechnicialBuldTruocHieuChinhDAO();
        private readonly GiaTriTruocHieuChinhDAO giatritruocDAO = new GiaTriTruocHieuChinhDAO();
        private readonly NgayTaoTechnicialBuldKetQuaHieuChinhDAO ngaytaoqkhcDAO = new NgayTaoTechnicialBuldKetQuaHieuChinhDAO();
        private readonly KetQuaHieuChinhDAO kqhcDAO = new KetQuaHieuChinhDAO();
        private readonly STD_DUT_DAO std_dut_DAO = new STD_DUT_DAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaGiayChungNhan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = giaychungnhanDAO.GetRowsByIds(idList);

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
        public ActionResult Edit(List<Giaychungnhan> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.Giaychungnhans
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var id in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == id.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id = id.Id;
                        existingRecord.Id_BienBanHieuChuan = id.Id_BienBanHieuChuan;
                        existingRecord.NgayHieuChuan = id.NgayHieuChuan;
                        existingRecord.NgayHieuChuanLai = id.NgayHieuChuanLai;
                        existingRecord.Type = id.Type; 
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"]?.ToString();
                        existingRecord.status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                } 
                dbContext.SaveChanges();
                TempData["Message"] = "Chỉnh sửa thành công";
                return RedirectToAction("Index");
            }
        } 
        public ActionResult Tao_giaychungnhan(string id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ThemGiayChungNhan"))
            {
                var idList = ParseIds(id);
                var listNumbers = bienbanhieuchuanDAO.GetRowsByIds(idList);

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
        public ActionResult Tao_giaychungnhan(List<Giaychungnhan> giayChungNhans)
        {
            if (giayChungNhans == null || !giayChungNhans.Any())
            {
                TempData["Message"] = "Danh sách phân công không hợp lệ.";
                return RedirectToAction("ThucHien", "PhanCong");
            }

            foreach (var giayChungNhan in giayChungNhans)
            {
                var bienBanHieuChuan = dbContext.BienBanHieuChuans.Find(giayChungNhan.Id_BienBanHieuChuan);
                if (bienBanHieuChuan == null) continue;

                var phanCongId = bienBanHieuChuan.Id_PhanCong;
                var bienbanId = bienbanhieuchuanDAO.getListId(phanCongId).FirstOrDefault()?.Id; 
                var messages = new[]
                {
                    (csdDAO.CheckSerialNumberExists(phanCongId), "Chưa có dữ liệu chuẩn sử dụng"),
                    (ngaytaotruocDAO.CheckSerialNumberExists(phanCongId), "Chưa có dữ liệu ngày tạo trước hiệu chỉnh"),
                    (giatritruocDAO.CheckSerialNumberExists(phanCongId), "Chưa có dữ liệu giá trị trước hiệu chỉnh"),
                    (ngaytaoqkhcDAO.CheckSerialNumberExists(phanCongId), "Chưa có dữ liệu ngày tạo kết quả hiệu chỉnh"),
                    (kqhcDAO.CheckSerialNumberExists(phanCongId), "Chưa có dữ liệu kết quả hiệu chỉnh"),
                    (std_dut_DAO.CheckSerialNumberExists(bienbanId), "Chưa có dữ liệu STD_DUT"),
                    (dkdbdoDAO.CheckSerialNumberExists(bienbanId), "Chưa có dữ liệu độ không đảm bảo đo")
                };

                foreach (var (exists, message) in messages)
                {
                    if (!exists)
                    {
                        TempData["Message"] = message;
                        return RedirectToAction("Details", "BienBanHieuChuan", new { id = phanCongId });
                    }
                }

                var newGiayChungNhan = new Giaychungnhan
                {
                    Id_BienBanHieuChuan = giayChungNhan.Id_BienBanHieuChuan,
                    MaGCN = giaychungnhanDAO.Layphieutudong(),
                    NgayTao = DateTime.Now,
                    Type = giayChungNhan.Type,
                    NgayHieuChuan = giayChungNhan.NgayHieuChuan,
                    NgayHieuChuanLai = giayChungNhan.NgayHieuChuanLai,
                    NguoiTao = Session["Name"]?.ToString() ?? "Unknown",
                    status = 1
                };

                giaychungnhanDAO.Insert(newGiayChungNhan);

                var phanCong = dbContext.PhanCongs.Find(phanCongId);
                if (phanCong != null)
                {
                    phanCong.Status = 2;
                }
            }

            dbContext.SaveChanges();
            TempData["Message"] = "Đã tạo giấy chứng nhận";
            return RedirectToAction("ThucHien", "PhanCong");
        } 
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaGiayChungNhan"))
            {
                var idList = ParseIds(ids);
                var listNumbers = giaychungnhanDAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<Giaychungnhan> ids)
        {
            foreach (Giaychungnhan listNumber in ids)
            {
                var oldlist = dbContext.Giaychungnhans.Find(listNumber.Id);
                dbContext.Giaychungnhans.Remove(oldlist);

            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("GiayChungNhan"))
            {
                var list = join_Giaychungnhan_DAO.GetListJoin(id);
                var Id_PhanCong = list.FirstOrDefault()?.Id_PhanCong;
                string buocsongA = "UV-A";
                string buocsongB = "UV-B";
                string buocsongC = "UV-C";
                string buocsongV = "UV-V";
                if (kqhcDAO.Check(Id_PhanCong, buocsongA) && kqhcDAO.Check(Id_PhanCong, buocsongB) && kqhcDAO.Check(Id_PhanCong, buocsongC) && kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongA))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongB))
                {
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongC))
                {
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                return View("Details", list); 
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }  
        }
        public ActionResult chitiet(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("GiayChungNhan"))
            {
                var list = join_Giaychungnhan_DAO.GetListJoin(id);
                var Id_PhanCong = list.FirstOrDefault()?.Id_PhanCong;
                string buocsongA = "UV-A";
                string buocsongB = "UV-B";
                string buocsongC = "UV-C";
                string buocsongV = "UV-V";
                if (kqhcDAO.Check(Id_PhanCong, buocsongA) && kqhcDAO.Check(Id_PhanCong, buocsongB) && kqhcDAO.Check(Id_PhanCong, buocsongC) && kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongA))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongB))
                {
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongC))
                {
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                return View("chitiet", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult InPDF_3(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfGiayChungNhan"))
            {
                var list = join_Giaychungnhan_DAO.GetListJoin(id);
                return View("InPDF_3", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult InPDF(int? id )
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfGiayChungNhan"))
            {
                var list = join_Giaychungnhan_DAO.GetListJoin(id);
                var Id_PhanCong = list.FirstOrDefault()?.Id_PhanCong;
                string buocsongA = "UV-A";
                string buocsongB = "UV-B";
                string buocsongC = "UV-C";
                string buocsongV = "UV-V";
                if (kqhcDAO.Check(Id_PhanCong, buocsongA) && kqhcDAO.Check(Id_PhanCong, buocsongB) && kqhcDAO.Check(Id_PhanCong, buocsongC) && kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongA))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongB))
                {
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongC))
                {
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ExportPdf(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfGiayChungNhan"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                            {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Portrait,
                                MarginLeft = 25,
                                MarginRight = 25,
                                MarginTop = 15,
                                MarginBottom = 5,
                            }
                    };

                    var list = join_Giaychungnhan_DAO.GetListJoin(id);
                    var Id_PhanCong = list.FirstOrDefault()?.Id_PhanCong;
                    string buocsongA = "UV-A";
                    string buocsongB = "UV-B";
                    string buocsongC = "UV-C";
                    string buocsongV = "UV-V";
                    if (kqhcDAO.Check(Id_PhanCong, buocsongA) && kqhcDAO.Check(Id_PhanCong, buocsongB) && kqhcDAO.Check(Id_PhanCong, buocsongC) && kqhcDAO.Check(Id_PhanCong, buocsongV))
                    {
                        var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                        var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                        var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                        var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                        var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                        var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                        var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                        var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                        var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                        var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                        var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                        var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                        var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                        var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                        var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                        var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongA))
                    {
                        var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                        var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                        var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                        var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongB))
                    {
                        var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                        var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                        var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                        var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongC))
                    {
                        var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                        var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                        var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                        var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongV))
                    {
                        var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                        var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                        var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                        var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                    }
                    if (list == null || !list.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderViewToString("~/Views/Giaychungnhan/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Giaychungnhan.pdf";

                    using (var stream = new MemoryStream())
                    {
                        doc.Save(stream);
                        doc.Close();

                        stream.Position = 0;

                        return File(stream.ToArray(), "application/pdf", fileName);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPDF";
                return RedirectToReferrer();
            }    
        }
        public ActionResult InPDF_2(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfGiayChungNhan"))
            {
                var list = join_Giaychungnhan_DAO.GetListJoin(id);
                var Id_PhanCong = list.FirstOrDefault()?.Id_PhanCong;
                string buocsongA = "UV-A";
                string buocsongB = "UV-B";
                string buocsongC = "UV-C";
                string buocsongV = "UV-V";
                if (kqhcDAO.Check(Id_PhanCong, buocsongA) && kqhcDAO.Check(Id_PhanCong, buocsongB) && kqhcDAO.Check(Id_PhanCong, buocsongC) && kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongA))
                {
                    var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                    var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                    ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                    var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                    var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                    ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongB))
                {
                    var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                    var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                    ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                    var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                    var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                    ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongC))
                {
                    var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                    var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                    ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                    var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                    var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                    ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                }
                else if (kqhcDAO.Check(Id_PhanCong, buocsongV))
                {
                    var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                    var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                    ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                    var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                    var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                    ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                }
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }    
            
        }
        public ActionResult ExportPdf_2(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfGiayChungNhan"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                            {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Portrait,
                                MarginLeft = 25,
                                MarginRight = 25,
                                MarginTop = 35,
                                MarginBottom = 5,
                            }
                    };

                    var list = join_Giaychungnhan_DAO.GetListJoin(id);
                    var Id_PhanCong = list.FirstOrDefault()?.Id_PhanCong;
                    string buocsongA = "UV-A";
                    string buocsongB = "UV-B";
                    string buocsongC = "UV-C";
                    string buocsongV = "UV-V";
                    if (kqhcDAO.Check(Id_PhanCong, buocsongA) && kqhcDAO.Check(Id_PhanCong, buocsongB) && kqhcDAO.Check(Id_PhanCong, buocsongC) && kqhcDAO.Check(Id_PhanCong, buocsongV))
                    {
                        var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                        var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                        var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                        var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                        var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                        var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                        var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                        var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                        var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                        var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                        var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                        var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                        var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                        var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                        var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                        var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongA))
                    {
                        var AverageA_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_1 = AverageA_1.ToString("F2");
                        var AverageA_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongA);
                        ViewBag.AverageA_2 = AverageA_2.ToString("F2");
                        var StandardA_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_1 = StandardA_1.ToString("F2");
                        var StandardA_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongA);
                        ViewBag.StandardA_2 = StandardA_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongB))
                    {
                        var AverageB_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_1 = AverageB_1.ToString("F2");
                        var AverageB_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongB);
                        ViewBag.AverageB_2 = AverageB_2.ToString("F2");
                        var StandardB_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_1 = StandardB_1.ToString("F2");
                        var StandardB_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongB);
                        ViewBag.StandardB_2 = StandardB_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongC))
                    {
                        var AverageC_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_1 = AverageC_1.ToString("F2");
                        var AverageC_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongC);
                        ViewBag.AverageC_2 = AverageC_2.ToString("F2");
                        var StandardC_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_1 = StandardC_1.ToString("F2");
                        var StandardC_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongC);
                        ViewBag.StandardC_2 = StandardC_2.ToString("F2");
                    }
                    else if (kqhcDAO.Check(Id_PhanCong, buocsongV))
                    {
                        var AverageV_1 = kqhcDAO.Average_1(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_1 = AverageV_1.ToString("F2");
                        var AverageV_2 = kqhcDAO.Average_2(Id_PhanCong, buocsongV);
                        ViewBag.AverageV_2 = AverageV_2.ToString("F2");
                        var StandardV_1 = kqhcDAO.Stand_1(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_1 = StandardV_1.ToString("F2");
                        var StandardV_2 = kqhcDAO.Stand_2(Id_PhanCong, buocsongV);
                        ViewBag.StandardV_2 = StandardV_2.ToString("F2");
                    }
                    if (list == null || !list.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderViewToString("~/Views/Giaychungnhan/InPDF_2.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Giaychungnhan.pdf";

                    using (var stream = new MemoryStream())
                    {
                        doc.Save(stream);
                        doc.Close();

                        stream.Position = 0;

                        return File(stream.ToArray(), "application/pdf", fileName);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPDF";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ExportPdf_3(int? id)
        {
            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfGiayChungNhan"))
            {
                try
                {
                    HtmlToPdf convert = new HtmlToPdf
                    {
                        Options =
                            {
                                PdfPageSize = PdfPageSize.A4,
                                PdfPageOrientation = PdfPageOrientation.Portrait,
                                MarginLeft = 25,
                                MarginRight = 25,
                                MarginTop = 25,
                                MarginBottom = 5,
                            }
                    };

                    var list = join_Giaychungnhan_DAO.GetListJoin(id); 
                    if (list == null || !list.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderViewToString("~/Views/Giaychungnhan/InPDF_3.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "Giaychungnhan.pdf";

                    using (var stream = new MemoryStream())
                    {
                        doc.Save(stream);
                        doc.Close();

                        stream.Position = 0;

                        return File(stream.ToArray(), "application/pdf", fileName);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền InPDF";
                return RedirectToReferrer();
            }
        }
        public ActionResult Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("GiayChungNhan"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;
                var list = join.GetListJoin(Name, pageSize, pageNumber);
                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ExportToExcel(int? ids)
        {

            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("ExcelGiayChungNhan"))
            {
                try
                {
                    var data = join.GetListJoin(ids);

                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.AddRange(new DataColumn[]
                    {

                            new DataColumn("STT", typeof(int)),
                            new DataColumn("Mã GCN", typeof(string)),
                            new DataColumn("Ngày tạo", typeof(DateTime)),
                            new DataColumn("Người tạo", typeof(string)),
                            new DataColumn("Trạng thái", typeof(int))
                    });

                    int rowIndex = 1;

                    foreach (var product in data)
                    {
                        dt.Rows.Add(
                            rowIndex++,
                            product.MaGCN,
                            product.NgayTao,
                            product.NguoiTao,
                            product.status);
                    }

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Giaychungnhan");
                        worksheet.Cell(1, 1).InsertTable(dt);

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Giaychungnhans.xlsx");
                        }
                    }
                }
                catch (Exception ex)
                { 
                    return new HttpStatusCodeResult(500, "Internal server error: " + ex.Message);
                }
            }
            else
            {
                TempData["Message"] = "Bạn không thể truy cập";
                return RedirectToReferrer();
            }     
        } 
    }
}