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
    public class GiaychungnhanDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<Giaychungnhan> getList(string status = "All")
        {
            List<Giaychungnhan> list;
            switch (status)
            {
                case "Index":
                    list = dbContext.Giaychungnhans
                        .AsNoTracking()
                        .Where(m => m.status == 1)
                        .OrderByDescending(m => m.Id)
                        .ToList();
                    break;

                default:
                    list = dbContext.Giaychungnhans
                        .AsNoTracking()
                        .ToList();
                    break;
            }
            return list;
        } 
        public string Layphieutudong()
        {
            var lastCode = dbContext.Giaychungnhans
                .Where(m => m.MaGCN.StartsWith("GCN"))
                .Select(m => m.MaGCN)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "GCN0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(3);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"GCN{number:D4}";
                }
                else
                {
                    nextcode = $"GCN{number}";
                }
            }
            return nextcode;
        }
        public PagedList.IPagedList<Giaychungnhan> getList(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.Giaychungnhans
                .AsNoTracking()
                .Where(m => m.status == 1);

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m => m.MaGCN.Contains(Name));
            }

            return query.OrderByDescending(m => m.Id)
                        .ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable<Giaychungnhan> GetRowsByIds(IEnumerable<int> ids)
        {
            var idSet = new HashSet<int>(ids); 
            return dbContext.Giaychungnhans
                .AsNoTracking()
                .Where(ln => idSet.Contains(ln.Id))
                .ToList();
        }
        public int Insert(Giaychungnhan ListNumber)
        {
            dbContext.Giaychungnhans.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(Giaychungnhan company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
    }
}
