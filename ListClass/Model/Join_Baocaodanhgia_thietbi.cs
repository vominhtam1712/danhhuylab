using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_Baocaodanhgia_thietbi
    {
        public int Id_Baocaodanhgia { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public string ThietBi_HoaChatHieuChuan { get; set; }
        public string TenThietBi { get; set; }
        public string SoSN { get; set; }
        public string Img { get; set; }
        public string MaThietBi { get; set; }
        public DateTime NgayHieuChuan { get; set; }
        public string KetQuaHC { get; set; }
        public string KetLuan { get; set; }
        public DateTime NgayHieuChuanKeTiep { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public int Status { get; set; }
    }
}
