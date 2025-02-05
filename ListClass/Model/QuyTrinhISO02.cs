using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    [Table("QuyTrinhISO02s")]
    public class QuyTrinhISO02
    {
        [Key]
        public int id { get; set; }
        public int id_quytrinh { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public virtual QuyTrinhISO QuyTrinhISO { get; set; }

    }
}
