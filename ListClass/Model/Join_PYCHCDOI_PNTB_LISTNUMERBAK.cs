using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_PYCHCDOI_PNTB_LISTNUMERBAK
    {
        public int Id { get; set; }
        public string MaPhieu { get; set; }
        public int Id_phieunhanthietbi { get; set; }
        public string SerialNumber { get; set; }
        public string SoNhanDang { get; set; }
        public string TenThietBi { get; set; }
        public string Hang { get; set; }
        public string Model { get; set; }
        public DateTime? NgaytaoSanPham { get; set; }
        public int? Status { get; set; }
    }
}
