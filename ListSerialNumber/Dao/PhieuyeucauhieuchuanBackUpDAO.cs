//using ListClass.Model;
//using PagedList;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace ListClass.Dao
//{
//    public class PhieudoiyeucauhieuchuanDAO
//    {
//        ListDBContext dbContext = new ListDBContext();
//        public int getCount()
//        {
//            return dbContext.Phieudoiyeucauhieuchuans.Where(m => m.Status == 1).Count();
//        }
//        public PagedList.IPagedList<PhieuyeucauhieuchuanBackUp> getList(string Name, int pageSize, int pageNumber, string Status = "All")
//        {
//            var list = dbContext.Phieudoiyeucauhieuchuans.Where(m => m.Status == 1).AsQueryable();
//            if (!string.IsNullOrEmpty(Name))
//            {
//                list = list.Where(m => m.Sophieu.ToLower().Contains(Name)
//                || m.Tendonviyeucau.ToLower().Contains(Name)
//                || m.Diachitendonviyeucau.ToLower().Contains(Name)
//                || m.Diachihieuchuan.ToLower().Contains(Name)
//                || m.Nguoilienhe.ToLower().Contains(Name)
//                || m.Tencognty.ToLower().Contains(Name)
//                || m.Diachicongty.ToLower().Contains(Name)
//                || m.Masothue.ToLower().Contains(Name)
//                || m.SerialNumber.ToLower().Contains(Name)
//                || m.Dichvuyeucau.ToLower().Contains(Name)
//                || m.Danhhuy.ToLower().Contains(Name));
//            }
//            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
//        }
//        public PagedList.IPagedList<PhieuyeucauhieuchuanBackUp> getListhistory(string Name, int pageSize, int pageNumber, string Status = "All")
//        {
//            var list = dbContext.Phieudoiyeucauhieuchuans.Where(m => m.Status != 1).AsQueryable();
//            if (!string.IsNullOrEmpty(Name))
//            {
//                list = list.Where(m => m.Sophieu.ToLower().Contains(Name)
//                || m.Tendonviyeucau.ToLower().Contains(Name)
//                || m.Diachitendonviyeucau.ToLower().Contains(Name)
//                || m.Diachihieuchuan.ToLower().Contains(Name)
//                || m.Nguoilienhe.ToLower().Contains(Name)
//                || m.Tencognty.ToLower().Contains(Name)
//                || m.Diachicongty.ToLower().Contains(Name)
//                || m.Masothue.ToLower().Contains(Name)
//                || m.SerialNumber.ToLower().Contains(Name)
//                || m.Dichvuyeucau.ToLower().Contains(Name)
//                || m.Danhhuy.ToLower().Contains(Name));
//            }
//            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
//        }
//        public PhieuyeucauhieuchuanBackUp getRow(int? id)
//        {
//            if (id == null)
//            {
//                return null;
//            }
//            else
//            {
//                return dbContext.Phieudoiyeucauhieuchuans.Where(m => m.Id == id).FirstOrDefault();
//            }
//        }

//        public int Insert(PhieuyeucauhieuchuanBackUp phieuyeucauhieuchuan)
//        {
//            dbContext.Phieudoiyeucauhieuchuans.Add(phieuyeucauhieuchuan);
//            return dbContext.SaveChanges();
//        }
//        public int Update(PhieuyeucauhieuchuanBackUp phieuyeucauhieuchuan)
//        {
//            dbContext.Entry(phieuyeucauhieuchuan).State = EntityState.Modified;
//            return dbContext.SaveChanges();
//        }
//        public int Delete(PhieuyeucauhieuchuanBackUp phieuyeucauhieuchuan)
//        {
//            dbContext.Phieudoiyeucauhieuchuans.Remove(phieuyeucauhieuchuan);
//            return dbContext.SaveChanges();
//        }
//    }
//}
