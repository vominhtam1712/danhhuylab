using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_NhatKyBaoTriVaSuChua_ThietBi
    {
        public int Id { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public string TenThietBi { get; set; }
        public string Serial { get; set; }
        public string MaThietBi { get; set; }
        public string Img { get; set; }
        public string HangSX { get; set; }
        public DateTime NgayLapDat { get; set; }
        public string NhatKyBaoTri { get; set; }

        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public int Status { get; set; }
    }
}
