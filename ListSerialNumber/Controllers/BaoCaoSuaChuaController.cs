using ListClass.Dao;
using ListClass.Model;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class BaoCaoSuaChuaController : AccountLoginController
    {
        private readonly Join_BaoCaoSuaChua_DAO join_baocaosuachua_DAO = new Join_BaoCaoSuaChua_DAO();
        private readonly GiaychungnhanDAO giaychungnhanDAO = new GiaychungnhanDAO();
        private readonly GiaychungnhanDAO gcnDAO = new GiaychungnhanDAO();
        private readonly ListDBContext dbContext = new ListDBContext();
        public JsonResult GetCustomerInfo(int id)
        {
            var gcn = dbContext.Giaychungnhans.Find(id);
            var bbhc = dbContext.BienBanHieuChuans.FirstOrDefault(m => m.Id == gcn.Id_BienBanHieuChuan);
            var pc = dbContext.PhanCongs.FirstOrDefault(m => m.Id == bbhc.Id_PhanCong);
            var pychc = dbContext.Phieuyeucauhieuchuans.FirstOrDefault(m => m.Id == pc.Id_pychc);
            var pntb = dbContext.PhieuNhanThietBis.FirstOrDefault(m => m.Id == pychc.Id_phieunhanthietbi);
            var lms = dbContext.ListNumbers.FirstOrDefault(m => m.Id == pntb.Id_ListNumber);
            var kh = dbContext.KhachHangs.FirstOrDefault(m => m.Id == lms.Id_KhachHang);
            if (gcn == null || bbhc == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            var result = new
            {
                SerialNumber = lms.SerialNumber,
                Tenthietbi = lms.Tenthietbi,
                Hang = lms.Hang,
                Model = lms.Model,
                MaKH = kh.MaKH,
                TenKH = kh.TenKH,
                DiaChi = kh.DiaChi,
                NhomNganh = kh.NhomNganh
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        } 
        public ActionResult InPDF(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("BaoCaoSuaChua")) 
            {
                var list = join_baocaosuachua_DAO.getlistDetails(ids);
                return View("InPDF", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }     
        }
        public ActionResult ExportPdf(int? ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("InPdfBaoCaoSuaChua"))
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
                                MarginBottom = 25
                            }
                    };

                    var list = join_baocaosuachua_DAO.getlistDetails(ids);
                    if (list == null || !list.Any())
                    {
                        return HttpNotFound("Dữ liệu không được tìm thấy");
                    }
                    var htmlPdf = base.RenderViewToString("~/Views/BaoCaoSuaChua/InPDF.cshtml", list, ControllerContext);

                    htmlPdf = htmlPdf.Replace("src=\"~/Public/Img/logo.png", "src=\"/Public/Img/logo.png");

                    PdfDocument doc = convert.ConvertHtmlString(htmlPdf);

                    string fileName = "BaoCaoSuaChua.pdf";

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
        public async Task<ActionResult> Index(string Name, int? page)
        {
            if (HasAccessAd() || HasAccessUser("BaoCaoSuaChua"))
            {
                int pageSize = string.IsNullOrEmpty(Name) ? 10 : 150;
                int pageNumber = page ?? 1;

                var count = await join_baocaosuachua_DAO.GetCountIndex();
                int totalPages = (int)Math.Ceiling((double)count / pageSize);

                ViewBag.Count = count;
                ViewBag.TotalPages = totalPages;
                ViewBag.CurrentPage = pageNumber;

                var list = join_baocaosuachua_DAO.getlistjoinIndex(Name, pageSize, pageNumber);
                if (list == null)
                {
                    TempData["Message"] = "Không có dữ liệu để hiển thị.";
                    return View("Index", new List<Join_BaoCaoSuaChua>());
                }

                return View("Index", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }  
        }
        public ActionResult chitiet(int? ids)
        {
            if (HasAccessAd() || HasAccessUser("BaoCaoSuaChua"))
            {
                var list = join_baocaosuachua_DAO.getlistDetails(ids);
                return View("chitiet", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        public ActionResult Details(int? ids)
        {
            if (HasAccessAd() || HasAccessUser("BaoCaoSuaChua"))
            {
                var list = join_baocaosuachua_DAO.getlistDetails(ids);
                return View("Details", list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            } 
        } 
        public ActionResult Create()
        {
            if (HasAccessAd() || HasAccessUser("ThemBaoCaoSuaChua")) 
            {
                ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
                return View();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền thêm";
                return RedirectToReferrer();
            } 
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BaoCaoSuaChua baocaosuachua)
        {
            if (!ModelState.IsValid)
            {
                return View(baocaosuachua);
            }
            baocaosuachua.MaBaoCao = join_baocaosuachua_DAO.Layphieutudong();
            baocaosuachua.NgayTao = DateTime.Now;
            baocaosuachua.Status = 1;
            baocaosuachua.NguoiTao = Session["Name"]?.ToString();
            join_baocaosuachua_DAO.Insert(baocaosuachua);
            TempData["Message"] = "Tạo thành công";
            ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
            return RedirectToAction("Index", "BaoCaoSuaChua");
        }  
        public ActionResult Edit(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("SuaBaoCaoSuaChua")) 
            {
                var idList = ParseIds(ids);
                var listNumbers = join_baocaosuachua_DAO.GetRowsByIds(idList);

                if (listNumbers == null || !listNumbers.Any())
                {
                    return HttpNotFound("No data found for the provided IDs");
                }
                ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
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
        public ActionResult Edit(List<BaoCaoSuaChua> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid data.");
            }

            using (var dbContext = new ListDBContext())
            {
                var idsToUpdate = ids.Select(b => b.Id).ToList();

                var existingRecords = dbContext.BaoCaoSuaChuas
                    .Where(b => idsToUpdate.Contains(b.Id))
                    .ToList();

                foreach (var idss in ids)
                {
                    var existingRecord = existingRecords.FirstOrDefault(b => b.Id == idss.Id);
                    if (existingRecord != null)
                    {
                        existingRecord.Id_giaychungnhan = idss.Id_giaychungnhan;
                        existingRecord.MaBaoCao = idss.MaBaoCao;
                        existingRecord.CacHangSuDung = idss.CacHangSuDung;
                        existingRecord.NgayTao = DateTime.Now;
                        existingRecord.NguoiTao = Session["Name"].ToString();
                        existingRecord.Status = 1;
                        dbContext.Entry(existingRecord).State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges(); 
                TempData["Message"] = "Cập nhật thành công";
                ViewBag.Phieunhan = new SelectList(gcnDAO.getList("Index"), "Id", "MaGCN");
                return RedirectToAction("Index", "BaoCaoSuaChua");
            }
        }
        public ActionResult Delete(string ids)
        {
            if (ids == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToReferrer();
            }
            if (HasAccessAd() || HasAccessUser("XoaBaoCaoSuaChua"))
            {
                var idList = ParseIds(ids);
                var listNumbers = join_baocaosuachua_DAO.GetRowsByIds(idList);

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
        public ActionResult Delete(List<BaoCaoSuaChua> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["Message"] = "Không có sản phẩm nào để xóa.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var listNumber in ids)
            {
                var oldList = dbContext.BaoCaoSuaChuas.Find(listNumber.Id); 
                dbContext.BaoCaoSuaChuas.Remove(oldList); 
            }
            dbContext.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("Index");
        }
    }
}