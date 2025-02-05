using DocumentFormat.OpenXml.Wordprocessing;
using ListClass.Dao;
using ListClass.Model;
using ListSerialNumber.Library;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.PeerToPeer;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ListSerialNumber.Controllers
{
    public class ThanhVienController : AccountLoginController
    {
        private readonly ThanhVienDAO thanhvienDAO = new ThanhVienDAO();
        private readonly NgaynghiDAO ngaynghiDAO = new NgaynghiDAO();
        private readonly ListDBContext dBContext = new ListDBContext();
        private readonly Md5 md5Hasher = new Md5();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duyet(int? Id)
        {
            if (Id == null)
            {
                TempData["Message"] = "Chưa có thông tin yêu cầu nghỉ!";
                return RedirectToReferrer();
            }
             
            if (HasAccessAd() || HasAccessUser("DuyetYeuCauNghi"))
            { 
                var datas = thanhvienDAO.getRow(Id);

                if (datas == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy yêu cầu nghỉ!" });
                }
                 
                datas.Status = datas.Status == 1 ? 2 : 1;
                thanhvienDAO.Update(datas);

                TempData["Message"] = "Đã duyệt yêu cầu nghỉ!";
                return RedirectToReferrer();
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền duyệt!";
                return RedirectToReferrer();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Huy(int? Id)
        {
            if (Id == null)
            {
                TempData["Message"] = "Chưa có thông tin yêu cầu nghỉ!";
                return RedirectToReferrer();
            }
             
            if (HasAccessAd() || HasAccessUser("HuyYeuCauNghi"))
            { 
                var thietBi = dBContext.Ngaynghis.Find(Id);
                 
                if (thietBi == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy yêu cầu nghỉ!" });
                }
                 
                dBContext.Ngaynghis.Remove(thietBi);
                dBContext.SaveChanges();

                TempData["Message"] = "Đã hủy đơn yêu cầu nghỉ!";
                return RedirectToReferrer();

            }
            else
            {
                TempData["Message"] = "Bạn không có quyền hủy!";
                return RedirectToReferrer();

            }
        }
        private int GetCurrentCompanyId()
        {
            if (Session["CompanyId"] != null && int.TryParse(Session["CompanyId"].ToString(), out int companyId) && companyId > 0)
            {
                return companyId;
            }
            throw new Exception("CompanyId is missing or invalid.");
        }
        public ActionResult Index(int? companyId)
        {
            if (HasAccessAd())
            {
                TempData["Message"] = "Chức năng chỉ dành cho thành viên";
                return RedirectToAction("Error", "Home");
            }
            if (companyId == null)
            {
                return RedirectToAction("Error", "Home");
            }

            int currentCompanyId = GetCurrentCompanyId();

            if (companyId != currentCompanyId)
            {
                return RedirectToReferrer();
            }

            var list = thanhvienDAO.getlistjoin(companyId);
            ViewBag.ListThanhVien = thanhvienDAO.getlist();
            ViewBag.ListRoles = thanhvienDAO.getlistRoles(companyId);
            return View(list);
        }
        public ActionResult Details(int? companyId)
        {
            if (HasAccessAd() || HasAccessUser("ThanhVien"))
            {
                if (companyId == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                ViewBag.ListThanhVien = thanhvienDAO.getlist();
                var list = thanhvienDAO.getlistjoin(companyId);
               
                return View(list);
            }
            else
            {
                TempData["Message"] = "Bạn không có quyền truy cập";
                return RedirectToReferrer();
            }
        }
        [HttpPost]
        public async Task<ActionResult> UploadProfileImage(int companyId, HttpPostedFileBase imgFile)
        {
            if (imgFile != null && imgFile.ContentLength > 0)
            {
                if (imgFile.ContentType.StartsWith("image"))
                {
                    var company = await dBContext.Companys.FindAsync(companyId);
                    if (company == null)
                    {
                        return Json(new { success = false, message = "Công ty không tồn tại." });
                    }

                    var fileName = Path.GetFileName(imgFile.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Public/Img/"), fileName);

                    imgFile.SaveAs(filePath);

                    company.Img = fileName;
                    dBContext.Entry(company).State = EntityState.Modified;
                    await dBContext.SaveChangesAsync();

                    return Json(new { success = true, message = "Cập nhật ảnh thành công!" });
                }
                else
                {
                    return Json(new { success = false, message = "Vui lòng chọn một file ảnh hợp lệ." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Không có file ảnh được chọn." });
            }
        }
        [HttpPost]
        public ActionResult UpdateCompanyInfo(int companyId, string email, string name,string username, string phone, string quequan, DateTime? namsinh, string gioitinh, string socccd, DateTime? ngayvaolam, string trinhdo, string chuyennganh, string chucvu)
        {
            if (HasAccessAd() || HasAccessUser("CapNhatThongTin"))
            {
                var company = dBContext.Companys.Find(companyId);
                if (company == null)
                {
                    return Json(new { success = false, message = "Thành viên không tồn tại" });
                }
                company.Email = email ?? null;
                company.Name = name ?? null;
                company.UserName = username ?? null;
                company.Phone = phone ?? null;
                company.QueQuan = quequan ?? null;
                company.NamSinh = namsinh ?? null;
                company.GioiTinh = gioitinh ?? null;
                company.SoCCCD = socccd ?? null;
                company.NgayVaoLam = ngayvaolam ?? null;
                company.BacDaoTao = trinhdo ?? null;
                company.ChuyenNganh = chuyennganh ?? null;
                company.ChucVu = chucvu ?? null;

                dBContext.Entry(company).State = EntityState.Modified;
                dBContext.SaveChanges();

                return Json(new { success = true, message = "Cập nhật thông tin thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền cập nhật thông tin!" });
            }
        }
        [HttpPost]
        public ActionResult CreateCompanyDates(int companyId, DateTime? ngaybatdau, DateTime? ngayketthuc, string lydoxinphep)
        {
            if (HasAccessAd() || HasAccessUser("ThemNgayNghi"))
            {
                if (ngaybatdau == null || ngayketthuc == null)
                {
                    return Json(new { success = false, message = "Hãy nhập ngày nghỉ bắt đầu và ngày nghỉ kết thúc" });
                }
                double tongNgay;
                var tennhanvien = dBContext.Companys.Where(m => m.Id == companyId).Select(m => m.Name).FirstOrDefault();
                var email = dBContext.Companys.Where(m => m.Id == companyId).Select(m => m.Email).FirstOrDefault();
                if (ngaybatdau == ngayketthuc)
                {
                    tongNgay = 0.5;
                }
                else
                {
                    tongNgay = (ngayketthuc.Value - ngaybatdau.Value).Days;
                }
                var oldlist = new Ngaynghi
                {
                    Id_Company = companyId,
                    Ngaybatdau = ngaybatdau,
                    Ngayketthuc = ngayketthuc,
                    LyDoXinPhep = lydoxinphep,
                    TongNgay = tongNgay,
                    Status = 1
                };

                ngaynghiDAO.Insert(oldlist);
                return Json(new { success = true, message = "Thêm ngày nghỉ thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền thêm ngày nghỉ!" });
            }

        }
        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var fromEmail = "nhanviendanhhuy@gmail.com";
                var smtpServer = "smtp.gmail.com";
                var smtpPort = 587;
                var smtpUser = "nhanviendanhhuy@gmail.com";
                var smtpPass = "hmzwrkjxxlsqaqve";

                var client = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body
                };

                mailMessage.To.Add(toEmail);

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi gửi email: " + ex.Message);
            }
        }
        [HttpPost]
        public ActionResult XinNghiPhep(int companyId, DateTime? ngaybatdau, DateTime? ngayketthuc, string lydoxinphep)
        {
            if (ngaybatdau == null || ngayketthuc == null)
            {
                return Json(new { success = false, message = "Hãy nhập ngày nghỉ bắt đầu và ngày nghỉ kết thúc" });
            }
            double tongNgay; 
            var tennhanvien = dBContext.Companys.Where(m => m.Id == companyId).Select(m => m.Name).FirstOrDefault();
            var email = dBContext.Companys.Where(m => m.Id == companyId).Select(m => m.Email).FirstOrDefault();
            if (ngaybatdau == ngayketthuc)
            {
                tongNgay = 0.5;
            }
            else
            {
                tongNgay = (ngayketthuc.Value - ngaybatdau.Value).Days;
            }
            var oldlist = new Ngaynghi
            {
                Id_Company = companyId,
                Ngaybatdau = ngaybatdau,
                Ngayketthuc = ngayketthuc,
                LyDoXinPhep = lydoxinphep,
                TongNgay = tongNgay,
                Status = 2
            };

            ngaynghiDAO.Insert(oldlist);
            try
            {
                var companyEmail = "info@danhhuy.com.vn";

                var subject = "Đơn xin nghỉ phép";
                var body = $"Họ và tên: {tennhanvien}\n" +
                           $"Email: {email}\n" +
                           $"Ngày nghỉ bắt đầu: {ngaybatdau.Value.ToString("dd/MM/yyyy")}\n" +
                           $"Ngày nghỉ kết thúc: {ngayketthuc.Value.ToString("dd/MM/yyyy")}\n" +
                           $"Số ngày nghỉ: {tongNgay} ngày\n" +
                           $"Lý do xin nghỉ: {lydoxinphep}";

                SendEmail(companyEmail, subject, body);

                return Json(new { success = true, message = "Thêm ngày nghỉ thành công và email đã được gửi!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi khi gửi email: " + ex.Message });
            }

        }
        [HttpPost]
        public ActionResult ChangePassword(int companyId, string oldPassword, string newPassword)
        {
            if (newPassword.Length < 8)
            {
                return Json(new { success = false, message = "Mật khẩu mới phải có ít nhất 8 ký tự!" });
            }
            var OldPassword = md5Hasher.ComputeMd5Hash(oldPassword);
            var company = dBContext.Companys.Find(companyId);
            if (company.Password != OldPassword)
            {
                return Json(new { success = false, message = "Mật khẩu cũ không đúng!" });
            }
            company.Password = md5Hasher.ComputeMd5Hash(newPassword);
            dBContext.Entry(company).State = EntityState.Modified;
            dBContext.SaveChanges();

            return Json(new { success = true, message = "Cập nhật mật khẩu thành công!" });
        }
        [HttpPost]
        public ActionResult ResetPassword(int companyId, string newPassword)
        {
            if (HasAccessAd() || HasAccessUser("DatLaiMatKhau"))
            {
                var company = dBContext.Companys.Find(companyId);
                company.Password = md5Hasher.ComputeMd5Hash(newPassword);
                dBContext.Entry(company).State = EntityState.Modified;
                dBContext.SaveChanges();

                return Json(new { success = true, message = "Cập nhật mật khẩu thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền đặt lại mật khẩu!" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNgayNghi(int? id, int? id_company)
        {

            if (id == null)
            {
                TempData["Message"] = "Chưa có thông tin";
                return RedirectToAction("Index");
            }
            if (HasAccessAd() || HasAccessUser("XoaNgayNghi"))
            {
                var ngaynghi = dBContext.Ngaynghis.Find(id);
                if (ngaynghi != null)
                {
                    dBContext.Ngaynghis.Remove(ngaynghi);
                    dBContext.SaveChanges();
                    TempData["Message"] = "Đã xóa ngày nghỉ thành công!";
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy ngày nghỉ.";
                }
                return RedirectToAction("Details", "ThanhVien", new { companyId = id_company });
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền xóa ngày nghỉ!" });
            }
        }
    }
}