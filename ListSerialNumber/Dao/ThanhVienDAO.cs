using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class ThanhVienDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int Update(Ngaynghi ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public Ngaynghi getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.Ngaynghis.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        public List<Join_company_ngaynghi> getlistjoin(int? companyId = null)
        {
            var query = from c in dbContext.Companys
                        join n in dbContext.Ngaynghis on c.Id equals n.Id_Company into nJoin
                        from n in nJoin.DefaultIfEmpty()
                        select new Join_company_ngaynghi
                        {
                            Id_company = c.Id,
                            Id_ngaynghi = n.Id,
                            Name = c.Name,
                            UserName = c.UserName,
                            Password = c.Password,
                            Email = c.Email,
                            Phone = c.Phone,
                            Status = c.Status,
                            Img = c.Img,
                            SoCCCD = c.SoCCCD,
                            QueQuan = c.QueQuan,
                            NamSinh = c.NamSinh,
                            GioiTinh = c.GioiTinh,
                            NgayVaoLam = c.NgayVaoLam,
                            BacDaoTao = c.BacDaoTao,
                            ChuyenNganh = c.ChuyenNganh,
                            ChucVu = c.ChucVu, 
                            TongNgay = n.TongNgay, 
                            LyDoXinPhep = n.LyDoXinPhep, 
                            Status_ngaynghi = n.Status, 
                            Ngaybatdau = n.Ngaybatdau ?? DateTime.MinValue,
                            Ngayketthuc = n.Ngayketthuc ?? DateTime.MinValue
                        };
             
            if (companyId.HasValue)
            {
                query = query.Where(m => m.Id_company == companyId.Value);
            }

            return query.ToList();
        }
        public List<CompanyRoles> getlistRoles(int? companyId = null)
        {
            var query = dbContext.CompanyRoles.Where(m=>m.Id_Company == companyId.Value); 
            return query.ToList();
        }
        public List<Join_company_ngaynghi> getlist()
        {
            var query = from c in dbContext.Companys
                        join n in dbContext.Ngaynghis on c.Id equals n.Id_Company into nJoin
                        from n in nJoin.DefaultIfEmpty()
                        select new Join_company_ngaynghi
                        {
                            Id_company = c.Id,
                            Id_ngaynghi = n.Id,
                            Name = c.Name,
                            UserName = c.UserName,
                            Password = c.Password,
                            Email = c.Email,
                            Phone = c.Phone,
                            Status = c.Status,
                            Img = c.Img,
                            SoCCCD = c.SoCCCD,
                            QueQuan = c.QueQuan,
                            NamSinh = c.NamSinh,
                            GioiTinh = c.GioiTinh,
                            NgayVaoLam = c.NgayVaoLam,
                            BacDaoTao = c.BacDaoTao,
                            ChuyenNganh = c.ChuyenNganh,
                            ChucVu = c.ChucVu,
                            TongNgay = n.TongNgay,
                            LyDoXinPhep = n.LyDoXinPhep,
                            Status_ngaynghi = n.Status,
                            Ngaybatdau = n.Ngaybatdau ?? DateTime.MinValue,
                            Ngayketthuc = n.Ngayketthuc ?? DateTime.MinValue
                        }; 
            return query.ToList();
        }

    }
}
