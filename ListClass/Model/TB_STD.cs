using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{

    [Table("TB_STDs")]
    public class TB_STD
    {
        [Key]
        public int ID { get; set; }
        public int ID_STD { get; set; }
        public int Id_BienBanHieuChuan { get; set; }
        public float? MucKiemTra { get; set; }
        public float? TrungBinh_STD { get; set; }
        public string NguoiTao { get; set; }
        public int? Status { get; set; }
        public virtual STD_DUT STD_DUT { get; set; }
        public virtual ICollection<BanTinhDKDBD> BanTinhDKDBDs { get; set; }
    }
}
