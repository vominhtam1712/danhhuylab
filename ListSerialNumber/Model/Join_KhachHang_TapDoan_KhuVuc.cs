using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_KhachHang_TapDoan_KhuVuc
    {
        public int  Id {  get; set; }
        public string MaKH {  get; set; }
        public string TenKH {  get; set; }
        public string DiaChi {  get; set; }
        public string Email {  get; set; }
        public string SDT {  get; set; }
        public string LienHe {  get; set; }
        public string NhomNganh {  get; set; }
        public string Ghichu {  get; set; }
        public string TenKhuVuc {  get; set; }
        public string TenTapDoan {  get; set; }
        public int Status {  get; set; }
    }
}
