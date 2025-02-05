using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class RoleCheckboxViewModel
    {
        public int Id { get; set; }
        public string NameRoles { get; set; }
        public string MaQuyen { get; set; }
        public int? Nhom { get; set; }
        public bool IsSelected { get; set; }
    }
}
