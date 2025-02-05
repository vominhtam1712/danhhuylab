using ClosedXML.Excel;
using ListClass.Dao;
using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class AccountLoginController : Controller
    {
        ListDBContext dbContext = new ListDBContext();
        protected AccountLoginController()
        {
            if (System.Web.HttpContext.Current.Session["UserName"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Account");
            }
        }
        protected string RenderPartialTostring(string viewName, object model,ControllerContext controllerContext)
        {
            if(string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");
                ViewDataDictionary ViewData = new ViewDataDictionary();
                TempDataDictionary TempData = new TempDataDictionary();
                ViewData.Model= model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext,viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
        protected string RenderViewToString(string viewName, object model, ControllerContext context)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(context, viewName, null);
                var viewContext = new ViewContext(context, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
        
        protected List<int> ParseIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                throw new ArgumentException("No IDs provided");
            }

            var idList = ids.Split(',')
                            .Select(id => int.TryParse(id, out var result) ? result : (int?)null)
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList();

            if (!idList.Any())
            {
                throw new ArgumentException("Invalid IDs");
            }

            return idList;
        }
        protected ActionResult RedirectToReferrer()
        {
            var currentUrl = Request.UrlReferrer?.ToString();
            if (!string.IsNullOrEmpty(currentUrl))
            {
                return Redirect(currentUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected bool HasAccessAd()
        {
            if (Session["UserId"] != null)
            {
                int idUser = Convert.ToInt32(Session["UserId"]);

                var user = dbContext.Users.FirstOrDefault(u => u.Id == idUser);

                if (user != null && user.Lever == "admin")
                {
                    return true;
                }
            }
            return false;
        } 
        protected bool HasAccessUser(string Values)
        {
            if (Session["CompanyId"] != null)
            {
                int idCompany = Convert.ToInt32(Session["CompanyId"]);

                var companyRoles = dbContext.CompanyRoles
                                            .Where(c => c.Id_Company == idCompany)
                                            .Select(c => c.MaQuyen)
                                            .ToList();

                if (companyRoles.Contains(Values))
                {
                    return true;
                }
            }

            return false;
        }
        protected DataTable CreateDataTable(IEnumerable<ListNumber> data)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("STT", typeof(int)),
                new DataColumn("Tenthietbi", typeof(string)),
                new DataColumn("Hang", typeof(string)),
                new DataColumn("Model", typeof(string)),
                new DataColumn("SerialNumber", typeof(string)),
            });

            int rowIndex = 1;
            foreach (var product in data)
            {
                dt.Rows.Add(
                    rowIndex++,
                    product.Tenthietbi,
                    product.Hang,
                    product.Model,
                    product.SerialNumber);
            }

            return dt;
        }

        protected void AddImagesToWorksheet(IXLWorksheet worksheet, IEnumerable<ListNumber> data, int imageColumnIndex)
        {
            int rowIndex = 2;
            foreach (var product in data)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    string imagePath = Server.MapPath("~/Public/Products/" + product.Image);
                    if (System.IO.File.Exists(imagePath))
                    {
                        var image = worksheet.AddPicture(imagePath)
                            .MoveTo(worksheet.Cell(rowIndex, imageColumnIndex))
                            .Scale(0.5);
                    }
                }
                rowIndex++;
            }
        }
        protected bool IsValidImage(HttpPostedFileBase file)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = System.IO.Path.GetExtension(file.FileName);
            return validExtensions.Contains(extension.ToLower());
        }

        protected string SaveFile(string slug, HttpPostedFileBase file)
        {
            string fileName = slug + System.IO.Path.GetExtension(file.FileName);
            string path = System.IO.Path.Combine(Server.MapPath("~/Public/Products"), fileName);
            file.SaveAs(path);
            return fileName;
        }

        protected string ValidatePhieu(PhieuTheoDoiNhietDo phieu)
        {
            if (phieu.NhietDo_Dau < 0 || phieu.NhietDo_Dau > 100) return "Nhiệt độ đầu không hợp lệ";
            if (phieu.NhietDo_Sau < 0 || phieu.NhietDo_Sau > 100) return "Nhiệt độ sau không hợp lệ";
            if (phieu.NhietDo_Dau >= phieu.NhietDo_Sau) return "Nhiệt độ đầu phải nhỏ hơn nhiệt độ sau";
            if (phieu.DoAm < 0 || phieu.DoAm > 100) return "Độ ẩm không hợp lệ";
            return null;
        }

        protected ActionResult RedirectWithMessage(string message, int? id = null)
        {
            TempData["Message"] = message;
            return id.HasValue ? RedirectToAction("Details", "PhieuTheoDoiNhietDo", new { Id = id.Value }) : RedirectToAction("ThucHien", "PhanCong");
        }
        protected void UpdatePhieu(PhieuTheoDoiNhietDo existingRecord, PhieuTheoDoiNhietDo newRecord)
        {
            existingRecord.MaPhieuTheoDoiNhietDo = newRecord.MaPhieuTheoDoiNhietDo;
            existingRecord.MSNhietKe_Model_No = newRecord.MSNhietKe_Model_No;
            existingRecord.Phong = newRecord.Phong;
            existingRecord.Thang_Nam = newRecord.Thang_Nam;
            existingRecord.NhietDo_Dau = newRecord.NhietDo_Dau;
            existingRecord.NhietDo_Sau = newRecord.NhietDo_Sau;
            existingRecord.DoAm = newRecord.DoAm;
            existingRecord.NgayTao = DateTime.Now;
            existingRecord.NguoiTao = Session["Name"].ToString();
            existingRecord.status = 1;
        }
        protected bool IsImageValid(HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0 && IsValidImage(file);
        }
        protected void SaveUploadedFile(ListNumber listNumber, HttpPostedFileBase file)
        {
            string fileName = SaveFile(listNumber.SerialNumber, file);
            listNumber.Image = fileName;
        }
        protected void DeleteProductImage(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var oldImagePath = Path.Combine(Server.MapPath("~/Public/Products"), imageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
        } 

    }
}