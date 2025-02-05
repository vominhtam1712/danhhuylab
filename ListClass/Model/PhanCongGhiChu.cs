using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("PhanCongGhiChus")]
    public class PhanCongGhiChu
    {
        [Key]
        public int Id { get; set; }
        public int Id_PhanCong { get; set; }
        public string GhiChu { get; set; }
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public virtual PhanCong PhanCong { get; set; }
    }
}
