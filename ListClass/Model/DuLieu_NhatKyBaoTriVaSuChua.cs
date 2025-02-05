using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("DuLieu_NhatKyBaoTriVaSuChuas")]
    public class DuLieu_NhatKyBaoTriVaSuChua
    {
        [Key]
        public int Id { get; set; }
        public int Id_NhatKyBaoTriVaSuChua { get; set; }
        public DateTime Ngay {  get; set; }
        public string MoTaSuCo {  get; set; }
        public string HanhDongKhacPhuc {  get; set; }
        public string KetQua {  get; set; }
        public string NguoiSuaChua {  get; set; }
        public string NguoiKiemTra {  get; set; }
        public int Status {  get; set; }
        public virtual NhatKyBaoTriVaSuChua NhatKyBaoTriVaSuChua { get; set; }

    }
}
