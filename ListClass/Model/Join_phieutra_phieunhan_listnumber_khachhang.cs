using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_phieutra_phieunhan_listnumber_khachhang
    {
        public int Id_phieutra { get; set; }
        public string MaPT { get; set; }
        public string SoGCN { get; set; }
        public string SoYeuCau { get; set; }
        public string SoNhanDang { get; set; } 
        public string TenTB { get; set; }
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
        public string LienHe { get; set; }
        public string Img { get; set; }
        public string SoSN { get; set; }
        public string Model { get; set; }
        public string Hang { get; set; }
        public string PhuongThucGiaoTra { get; set; }
        public string TrangThaiThietBi { get; set; }
        public DateTime NgayThucHien { get; set; }
        public DateTime NgayTao { get; set; }
        public string GhiChu { get; set; }
        public string NguoiTao { get; set; }
    }
}
