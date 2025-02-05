using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("LampSystems")]
    public class LampSystem
    {
        [Key]
        public int Id { get; set; }
        public string Lamp_Systems { get; set; }
        public int status { get; set; }
        public virtual ICollection<NgayTaoTechnicialBuldTruocHieuChinh> NgayTaoTechnicialBuldTruocHieuChinhs { get; set; }
        public virtual ICollection<NgayTaoTechnicialBuldKetQuaHieuChinh> NgayTaoTechnicialBuldKetQuaHieuChinhs { get; set; }
    }
}
