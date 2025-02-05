using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("GiayCNSanPhams")]
    public class GiayCNSanPham
    {
        [Key]
        public int Id { get; set; }
        public int? Id_ListNumber { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public virtual ListNumber ListNumber { get; set; }
    }
}
