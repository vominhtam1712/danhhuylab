using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("KhuVucs")]
    public class KhuVuc
    {
        [Key]
        public int Id { get; set; }
        public string Tenkhuvuc { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
}
