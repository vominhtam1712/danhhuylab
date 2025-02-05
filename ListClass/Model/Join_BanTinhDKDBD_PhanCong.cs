using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_BanTinhDKDBD_PhanCong
    {
        public int Id { get; set; }
        public int Id_BienBanHieuChuan { get; set; }
        public int Id_PhanCong { get; set; }
        public float? MucKiemTra { get; set; }
        public float? U_a { get; set; }
        public float? U_d { get; set; }
        public float? U_cer { get; set; }
        public float? U_od_std { get; set; }
        public float? U_od { get; set; }
        public float? U_dd { get; set; }
        public float? U_c { get; set; }
        public float? K { get; set; }
        public float? U_morong { get; set; }
        public float? Saiso { get; set; }
        public float? DoDKDB { get; set; }
        public string NguoiTao { get; set; }
        public int Status { get; set; }
    }
}
