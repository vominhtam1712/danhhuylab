using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_KeHoachHieuChuanKiemTra_ThietBi
    {
        public int Id { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public int Id_KeHoachHieuChuan_form { get; set; }
        public string TenThietBi { get; set; }
        public string Serial { get; set; }
        public string MaThietBi { get; set; }
        public string Img { get; set; }
        public string HangSX { get; set; }
        public string Jan_Plan { get; set; }
        public string Jan_Act { get; set; }

        public string Feb_Plan { get; set; }
        public string Feb_Act { get; set; }

        public string March_Plan { get; set; }
        public string March_Act { get; set; }

        public string April_Plan { get; set; }
        public string April_Act { get; set; }

        public string May_Plan { get; set; }
        public string May_Act { get; set; }

        public string June_Plan { get; set; }
        public string June_Act { get; set; }

        public string July_Plan { get; set; }
        public string July_Act { get; set; }

        public string Aug_Plan { get; set; }
        public string Aug_Act { get; set; }

        public string Sep_Plan { get; set; }
        public string Sep_Act { get; set; }

        public string Oct_Plan { get; set; }
        public string Oct_Act { get; set; }

        public string Nov_Plan { get; set; }
        public string Nov_Act { get; set; }

        public string Dec_Plan { get; set; }
        public string Dec_Act { get; set; }

        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayTao_Pdf { get; set; }
        public int Status { get; set; }
    }
}
