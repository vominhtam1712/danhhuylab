using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_Giaychungnhan_Bienbanhieuchuan
    {
        public int Id_GCN { get; set; } 
        public int Id_BienBanHieuChuan { get; set; }
        public string MaGCN { get; set; }
        public string SoNhanDang { get; set; }
        public string SerialNumber { get; set; }
        public string Tenthietbi { get; set; }
        public string Model { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public int status { get; set; }
    }
}
