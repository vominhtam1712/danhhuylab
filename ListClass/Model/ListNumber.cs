using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("ListNumbers")]
    public class ListNumber
    {
        [Key]
        public int Id { get; set; }
        public int? Id_KhachHang { get; set; } 
        public string Tenthietbi { get; set; }
        public string Image { get; set; } 
        public string Hang { get; set; } 
        public string Model { get; set; }  
        public string SerialNumber { get; set; }
        public string PhamViDo { get; set; }
        public string DoPhanGiai { get; set; }
        public string NguoiTao { get; set; }
        public string GhiChu { get; set; } 
        public DateTime NgaytaoSanPham { get; set; }
        public int? Status { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<PhieuNhanThietBi> PhieuNhanThietBis { get; set; }
        public virtual ICollection<GiayCNSanPham> GiayCNSanPhams { get; set; }

    }

}
