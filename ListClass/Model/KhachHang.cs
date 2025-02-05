using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("KhachHangs")]
    public class KhachHang
    {
        public int Id { get; set; } 
        public string MaKH { get; set; }
        public string TenKH { get; set; }  
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public string LienHe { get; set; }
        public string Ghichu { get; set; }
        public string NhomNganh { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public int? Status { get; set; }
        public int? Id_TapDoan { get; set; }
        public int? Id_KhuVuc { get; set; }
        public virtual TapDoan TapDoan { get; set; }
        public virtual KhuVuc KhuVuc { get; set; }
        public virtual ICollection<ListNumber> ListNumbers { get; set; }
    }
}
