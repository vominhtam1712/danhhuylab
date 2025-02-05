using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Model
{
    public class Join_company_ngaynghi
    {   
        public int Id_company { get; set; }
        public int? Id_ngaynghi { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public string Img { get; set; }
        public string SoCCCD { get; set; }
        public string QueQuan { get; set; }
        public DateTime? NamSinh { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public string BacDaoTao { get; set; }
        public string ChuyenNganh { get; set; }
        public string ChucVu { get; set; } 
        public string LyDoXinPhep { get; set; } 
        public DateTime? Ngaybatdau { get; set; }
        public DateTime? Ngayketthuc { get; set; }
        public int? TongNgay { get; set; }
        public int? Status_ngaynghi { get; set; } 
    }
}
