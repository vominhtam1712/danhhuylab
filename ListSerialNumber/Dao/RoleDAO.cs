using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class RoleDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public PagedList.IPagedList<Role> getList (int pageSize, int pageNumber)
        {
            var query = dbContext.Roles.Where(m => m.Status == 1).OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
            return query;
        }
        public Role getRow(int? id)
        { 
            return id == null
                ? null
                : dbContext.Roles.FirstOrDefault(m => m.Id == id);  
        }
        public int Insert(Role company)
        {
            dbContext.Roles.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(Role company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
    }
}
