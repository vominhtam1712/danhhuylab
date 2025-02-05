using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class join_PhieuTheoDoiNhietDo_DanhSachPhieuTheoDoiNhietDo
    {
        public int Id_PhieuTheoDoiNhietDo { get; set; }
        public string MaPhieuTheoDoiNhietDo { get; set; }
        public string MSNhietKe_Model_No { get; set; }
        public string Phong { get; set; }
        public DateTime Thang_Nam { get; set; }
        public int? Ngay_kiem_tra { get; set; } 
        public float? NhietDo_Dau { get; set; }
        public float? NhietDo_Sau { get; set; }
        public float? DoAm { get; set; }
        public int ID_DanhSachPhieuTheoDoiNhietDo { get; set; }
        public float? NhietDo_Sang { get; set; }
        public float? DoAm_Sang { get; set; }
        public float? NhietDo_Chieu { get; set; }
        public float? DoAm_Chieu { get; set; }
        public string KetLuan { get; set; }
        public string NguoiKiemTra { get; set; }
    }
}
