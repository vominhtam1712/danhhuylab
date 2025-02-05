using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_NhatKyBaoTriVaSuChua_ThietBi_DuLieu
    {
        public int Id { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public int Id_DuLieu { get; set; }
        public string TenThietBi { get; set; }
        public string Serial { get; set; }
        public string MaThietBi { get; set; }
        public string Img { get; set; }
        public string HangSX { get; set; }
        public DateTime NgayLapDat { get; set; }
        public string NhatKyBaoTri { get; set; }
        public string NguoiTao { get; set; }
        public DateTime Ngay { get; set; }
        public string MoTaSuCo { get; set; }
        public string HanhDongKhacPhuc { get; set; }
        public string KetQua { get; set; }
        public string NguoiSuaChua { get; set; }
        public string NguoiKiemTra { get; set; }
        public DateTime NgayTao { get; set; }
        public int Status { get; set; }
    }
}
