using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("NhatKyBaoTriVaSuChuas")]
    public class NhatKyBaoTriVaSuChua
    {
        [Key]
        public int Id { get; set; }
        public int Id_DanhMucThietBi { get; set; }
        public DateTime NgayLapDat { get; set; }
        public string NhatKyBaoTri {  get; set; }

        public string NguoiTao {  get; set; }
        public DateTime NgayTao { get; set; }
        public int Status {  get; set; }
        public virtual DanhMucThietBi DanhMucThietBi { get; set; }
        public virtual ICollection<DuLieu_NhatKyBaoTriVaSuChua> DuLieu_NhatKyBaoTriVaSuChuas { get; set; }
    }
}
