using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ListSerialNumber
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Sitemap",
                url: "sitemap.xml",
                defaults: new { controller = "Sitemap", action = "Index" }
            );
            routes.MapRoute(
                name: "AccountIndex",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Index"}
            );
            routes.MapRoute(
               name: "HomeIndex",
               url: "danh-huy-lab",
               defaults: new { controller = "Home", action = "Index" }
           );
            routes.MapRoute(
              name: "GiayCNSanPham",
              url: "giay-chung-nhan-san-pham/{id}",
              defaults: new { controller = "GiayCNSanPham", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "ListNumberIndex",
               url: "danh-sach-san-pham",
               defaults: new { controller = "ListNumber", action = "Index" }
           );
            routes.MapRoute(
               name: "LampSystemIndex",
               url: "danh-sach-lamp-systems",
               defaults: new { controller = "LampSystem", action = "Index" }
           );
            routes.MapRoute(
              name: "BulbSpectrumIndex",
              url: "danh-sach-bulb-spectrum",
              defaults: new { controller = "BulbSpectrum", action = "Index" }
          );
            routes.MapRoute(
               name: "KhachHangIndex",
               url: "danh-sach-khach-hang",
               defaults: new { controller = "KhachHang", action = "Index" }
           );
            routes.MapRoute(
              name: "TapDoanIndex",
              url: "danh-sach-tap-doan",
              defaults: new { controller = "TapDoan", action = "Index" }
            );
            routes.MapRoute(
            name: "KhuVucIndex",
            url: "danh-sach-khu-vuc",
            defaults: new { controller = "KhuVuc", action = "Index" }
            );
            routes.MapRoute(
            name: "PhieuNhanThietBichitiet",
            url: "chi-tiet-phieu-nhan-thiet-bi/{id}",
            defaults: new { controller = "PhieuNhanThietBi", action = "chitiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "PhieuNhanThietBiIndex",
            url: "phieu-nhan-thiet-bi",
            defaults: new { controller = "PhieuNhanThietBi", action = "Index" }
           );
            routes.MapRoute(
           name: "Phieuyeucauhieuchuanchitiet",
           url: "chi-tiet-phieu-yeu-cau-hieu-chuan/{id}",
           defaults: new { controller = "Phieuyeucauhieuchuan", action = "chitiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "PhieuyeucauhieuchuanIndex",
            url: "phieu-yeu-cau-hieu-chuan",
            defaults: new { controller = "Phieuyeucauhieuchuan", action = "Index" }
             );
            routes.MapRoute(
            name: "PhieuChuyenMauHieuChuanchitiet",
            url: "chi-tiet-phieu-chuyen-mau-hieu-chuan/{id}",
            defaults: new { controller = "PhieuChuyenMauHieuChuan", action = "chitiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "PhieuChuyenMauHieuChuanIndex",
            url: "phieu-chuyen-mau-hieu-chuan",
            defaults: new { controller = "PhieuChuyenMauHieuChuan", action = "Index" }
            );
            routes.MapRoute(
            name: "Phieutrathietbichitiet",
            url: "chi-tiet-phieu-tra-thiet-bi/{id}",
            defaults: new { controller = "Phieutrathietbi", action = "chitiet", id = UrlParameter.Optional }
             );
            routes.MapRoute(
            name: "PhieutrathietbiIndex",
            url: "phieu-tra-thiet-bi",
            defaults: new { controller = "Phieutrathietbi", action = "Index" }
             );
            routes.MapRoute(
            name: "PhieuyeucauhieuchuanDoiIndex",
            url: "phieu-yeu-cau-hieu-chuan-doi",
            defaults: new { controller = "PhieuyeucauhieuchuanDoi", action = "Index" }
            );
            routes.MapRoute(
            name: "PhieuyeucauhieuchuanDoiTrash",
            url: "phieu-yeu-cau-hieu-chuan-da-duyet",
            defaults: new { controller = "PhieuyeucauhieuchuanDoi", action = "Trash" }
            );
            routes.MapRoute(
            name: "PhanCongIndex",
            url: "danh-sach-phan-cong",
            defaults: new { controller = "PhanCong", action = "Index" }
            );
            routes.MapRoute(
            name: "PhanCongTrash",
            url: "lich-su-phan-cong",
            defaults: new { controller = "PhanCong", action = "Trash" }
            );
            routes.MapRoute(
            name: "PhanCongThucHien",
            url: "phan-cong-thuc-hien",
            defaults: new { controller = "PhanCong", action = "ThucHien" }
            );
            routes.MapRoute(
            name: "BienBanHieuChuanDetails",
            url: "phan-cong-thuc-hien/hieu-chinh-hieu-chuan/{id}",
            defaults: new { controller = "BienBanHieuChuan", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Giaychungnhanchitiet",
            url: "chi-tiet-giay-chung-nhan/{id}",
            defaults: new { controller = "Giaychungnhan", action = "chitiet", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "GiaychungnhanIndex",
            url: "giay-chung-nhan",
            defaults: new { controller = "Giaychungnhan", action = "Index" }
            );
            routes.MapRoute(
            name: "BaoCaoSuaChuachitiet",
            url: "chi-tiet-bao-cao-sua-chua/{ids}",
            defaults: new { controller = "BaoCaoSuaChua", action = "chitiet", ids = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "BaoCaoSuaChuaIndex",
            url: "bao-cao-sua-chua",
            defaults: new { controller = "BaoCaoSuaChua", action = "Index" }
            );
            routes.MapRoute(
            name: "DanhMucThietBiIndex",
            url: "danh-muc-thiet-bi",
            defaults: new { controller = "DanhMucThietBi", action = "Index" }
            );
            routes.MapRoute(
           name: "KeHoachHieuChuanFormIndex",
           url: "ke-hoach-hieu-chuan",
           defaults: new { controller = "KeHoachHieuChuan_Form", action = "Index" }
           );
            routes.MapRoute(
           name: "KeHoachHieuChuanIndex",
           url: "ke-hoach-hieu-chuan/{id}",
           defaults: new { controller = "KeHoachHieuChuan", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
            name: "BaoCaoDanhGiaIndex",
            url: "bao-cao-danh-gia",
            defaults: new { controller = "BaoCaoDanhGia", action = "Index" }
            );
            routes.MapRoute(
            name: "NhatKyBaoTriVaSuChuaIndex",
            url: "nhat-ky-bao-tri-sua-chua",
            defaults: new { controller = "NhatKyBaoTriVaSuChua", action = "Index"}
            );
            routes.MapRoute(
            name: "NhatKyBaoTriVaSuChuaDetails",
            url: "nhat-ky-bao-tri-sua-chua/{id}",
            defaults: new { controller = "NhatKyBaoTriVaSuChua", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "PhieuTheoDoiNhietDoDetails",
            url: "phieu-theo-doi-thiet-do",
            defaults: new { controller = "PhieuTheoDoiNhietDo", action = "Details" }
            );
            routes.MapRoute(
            name: "DanhSachPhieuTheoDoiNhietDoIndex",
            url: "phieu-theo-doi-thiet-do/{id}",
            defaults: new { controller = "DanhSachPhieuTheoDoiNhietDo", action = "Index", id = UrlParameter.Optional }
            ); 
            routes.MapRoute(
            name: "LuuInHoSoIndex",
            url: "luu-in-ho-so",
            defaults: new { controller = "LuuInHoSo", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "LuuInHoSoDetails",
            url: "luu-in-ho-so/{id}",
            defaults: new { controller = "LuuInHoSo", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "QuyTrinhISOIndex",
            url: "quy-trinh-iso-17025",
            defaults: new { controller = "QuyTrinhISO", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "CompanysIndex",
            url: "danh-sach-tai-khoan",
            defaults: new { controller = "Companys", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "ThanhVienDetails",
            url: "thong-tin-thanh-vien/{companyId}",
            defaults: new { controller = "ThanhVien", action = "Details", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "RoleIndex",
            url: "danh-sach-quyen",
            defaults: new { controller = "Role", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "ThanhVienIndex",
            url: "thong-tin-tai-khoan/{companyId}",
            defaults: new { controller = "ThanhVien", action = "Index", id = UrlParameter.Optional }
            ); 
            routes.MapRoute(
                name: "Error404",
                url: "Error/404",
                defaults: new { controller = "Home", action = "Error" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Account", action = "Index" }
            );    
        }
    }
}
