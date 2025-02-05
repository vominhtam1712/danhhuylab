using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("NgayTaoTechnicialBuldKetQuaHieuChinhs")]
    public class NgayTaoTechnicialBuldKetQuaHieuChinh
    {
        [Key]
        public int Id { get; set; }
        public int Id_PhanCong { get; set; }
        public string Technicial { get; set; }
        public int? Id_BulbSpectrum { get; set; } 
        public int? Id_LampSystems { get; set; } 
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public int Status { get; set; }
        public virtual PhanCong PhanCong { get; set; }
        public virtual BulbSpectrum BulbSpectrum { get; set; }
        public virtual LampSystem LampSystem { get; set; }
        
    }
}
