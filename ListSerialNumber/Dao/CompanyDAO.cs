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
    public class CompanyDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public bool CheckSerialNumberExists(string username)
        {
            return !string.IsNullOrWhiteSpace(username) && dbContext.Companys.Any(m => m.UserName == username);
        }

        public bool IsPasswordValid(string password)
        {
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }

        public PagedList.IPagedList<Company> getList(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.Companys.Where(m => m.Status == 1).AsQueryable(); 
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m => m.Name.ToLower().Contains(Name.ToLower()) ||
                                          m.UserName.ToLower().Contains(Name.ToLower()) ||
                                          m.Email.ToLower().Contains(Name.ToLower()));
            }

            return query.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }

        public PagedList.IPagedList<Company> getListTrash(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.Companys.Where(m => m.Status != 1).AsQueryable(); 

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m => m.Name.ToLower().Contains(Name.ToLower()) ||
                                          m.UserName.ToLower().Contains(Name.ToLower()) ||
                                          m.Email.ToLower().Contains(Name.ToLower()));
            }

            return query.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable<Company> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.Companys
                .AsNoTracking()
                .Where(m => ids.Contains(m.Id))
                .ToList();
        }

        public List<Company> getList(string status = "All")
        {
            IQueryable<Company> query = dbContext.Companys;

            if (status == "Index")
            {
                query = query.Where(m => m.Status != 0);
            }

            return query.OrderByDescending(m => m.Id).ToList();
        }

        public List<Company> getListAll()
        {
            return dbContext.Companys.Where(m => m.Status == 1).ToList();
        }

        public Company getRow(int? id)
        {
            return id == null ? null : dbContext.Companys.FirstOrDefault(m => m.Id == id);
        }

        public int Insert(Company company)
        {
            dbContext.Companys.Add(company);
            return dbContext.SaveChanges();
        }

        public int Update(Company company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }

        public int Delete(Company company)
        {
            dbContext.Companys.Remove(company);
            return dbContext.SaveChanges();
        }
    }
}
