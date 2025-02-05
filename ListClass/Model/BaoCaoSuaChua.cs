using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ListClass.Model
{
    [Table("BaoCaoSuaChuas")]
    public class BaoCaoSuaChua
    {
        public int Id { get; set; }
        public int Id_giaychungnhan { get; set; }
        public string MaBaoCao { get; set; }
        public string CacHangSuDung { get; set; }

        public string NguoiTao { get; set; }

        public DateTime NgayTao { get; set; }

        public int Status { get; set; }

        public virtual Giaychungnhan Giaychungnhan { get; set; }

    }
}
