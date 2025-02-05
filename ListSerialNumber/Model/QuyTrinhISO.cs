using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("QuyTrinhISOs")]
    public class QuyTrinhISO
    {
        [Key]
        public int id { get; set; }
        public string TenQuyTrinh { get; set; } 
        public DateTime NgayTao { get; set; } 
        public string NguoiTao { get; set; } 
        public int? Status { get; set; }
        public virtual ICollection<QuyTrinhISO02> QuyTrinhISO02s { get; set; }
    }
}
