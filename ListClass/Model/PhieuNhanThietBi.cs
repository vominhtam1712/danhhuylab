using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhieuNhanThietBis")]
    public class PhieuNhanThietBi
    {

        [Key]
        public int Id { get; set; }
        public int Id_ListNumber { get; set; } 
        public string SoNhanDang { get; set; }
        public string SoYeuCau { get; set; } 
        public string NguoiThucHien { get; set; }
        public string HienTrang { get; set; }
        public string GhiChu { get; set; } 
        public DateTime NgayThucHien { get; set; }
        public DateTime Ngaytao { get; set; }
        public string Nguoitao { get; set; }
        public int? Status { get; set; }
        public virtual ListNumber ListNumber { get; set; } 
        public virtual ICollection<Phieuyeucauhieuchuan> Phieuyeucauhieuchuans { get; set; }
        public virtual ICollection<PhieuChuyenMauHieuChuan> PhieuChuyenMauHieuChuans { get; set; }
    }
}
