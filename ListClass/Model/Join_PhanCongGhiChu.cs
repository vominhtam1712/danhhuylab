using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_PhanCongGhiChu
    {
        public int Id_PhanCongGhiChu {  get; set; }
        public int Id_PhanCong{  get; set; }
        public string GhiChu_PhanCongGhiChu { get; set; }
        public string TenKH { get; set; }
        public string SoNhanDang { get; set; }
        public string Model { get; set; }
        public string SN { get; set; }
        public string Hang { get; set; }
        public string TenTB { get; set; }
        public DateTime Ngaytao { get; set; }
    }
}
