using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("Phieutrathietbis")]
    public class Phieutrathietbi
    {
        public int Id { get; set; }
        public int Id_Giaychungnhan { get; set; } 
        public string MaPT { get; set; }
        public string PhuongThucGiaoTra { get; set; }
        public string TrangThaiThietbi { get; set; }
        public string NguoiTao { get; set; }
        public string Ghichu { get; set; } 
        public DateTime NgayThucHien { get; set; }
        public DateTime NgayTao { get; set; }
        public int status { get; set; }
        public virtual Giaychungnhan Giaychungnhan { get; set; } 
    }
}
