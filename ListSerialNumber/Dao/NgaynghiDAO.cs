using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class NgaynghiDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public int Insert(Ngaynghi Ngaynghi)
        {
            dbContext.Ngaynghis.Add(Ngaynghi);
            return dbContext.SaveChanges();
        }

        public int Update(Ngaynghi Ngaynghi)
        {
            dbContext.Entry(Ngaynghi).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Delete(Ngaynghi Ngaynghi)
        {
            dbContext.Ngaynghis.Remove(Ngaynghi);
            return dbContext.SaveChanges();
        }
    }
}
