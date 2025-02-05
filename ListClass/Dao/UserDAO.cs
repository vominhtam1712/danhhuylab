using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ListClass.Model;
using PagedList;

namespace ListClass.Dao
{
    public class UserDAO
    {
        ListDBContext dbContext =new ListDBContext();
        public PagedList.IPagedList<User> getList(string Name, int? Search, int pageSize, int pageNumber)
        {
            var list = dbContext.Users.AsQueryable();
             
            if (Search.HasValue)
            {
                list = list.Where(m => m.Phone == Search.Value);
            }
             
            if (!string.IsNullOrEmpty(Name))
            {
                var lowerCaseName = Name.ToLower(); 
                list = list.Where(m => m.Name.ToLower().Contains(lowerCaseName) ||
                                       m.UserName.ToLower().Contains(lowerCaseName) ||
                                       m.Email.ToLower().Contains(lowerCaseName));
            }
             
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }

        public User getRow(int? id)
        { 
            return id == null ? null : dbContext.Users.FirstOrDefault(m => m.Id == id);
        }
        public IEnumerable<User> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.Users
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public int Insert(User user)
        {
            dbContext.Users.Add(user);
            return dbContext.SaveChanges();
        }
        public int Update(User user)
        {
            dbContext.Entry(user).State=EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(User user)
        {
            dbContext.Users.Remove(user);
            return dbContext.SaveChanges();
        }
    }
}
