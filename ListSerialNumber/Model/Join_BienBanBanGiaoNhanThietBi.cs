using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_BienBanBanGiaoNhanThietBi
    {
        public int Id { get; set; }
        public string MaP { get; set; }
        public string TenKH { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayThucHien { get; set; }
        public string NguoiTao { get; set; }
    }
}
