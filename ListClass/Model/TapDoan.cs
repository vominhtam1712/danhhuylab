using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("TapDoans")]
    public class TapDoan
    {
        [Key]
        public int Id { get; set; }
        public string Tentapdoan { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
}
