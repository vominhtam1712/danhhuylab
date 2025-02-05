using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("Ngaynghis")]
    public class Ngaynghi
    {
        [Key]
        public int Id { get; set; }
        public int? Id_Company { get; set; }
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public string LyDoXinPhep { get; set; }
        public int? TongNgay { get; set; }
        public int? Status { get; set; }
        public virtual Company Company { get; set; }
    }
}
