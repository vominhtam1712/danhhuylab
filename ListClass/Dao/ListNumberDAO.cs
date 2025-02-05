using ListClass.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{

    public class ListNumberDAO
    {
        ListDBContext dbContext = new ListDBContext();  
        public ListNumber GetById(int id)
        {
            return dbContext.ListNumbers
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public int GetCountIndex()
        {
            return dbContext.ListNumbers.Count(m=>m.Status==1);
        } 
        public int GetCountTrash()
        {
            return dbContext.ListNumbers.Count(m => m.Status != 1);
        }
        public IEnumerable<ListNumber> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.ListNumbers
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public PagedList.IPagedList<ListNumber> getList(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.ListNumbers.Where(m => m.Status == 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Tenthietbi.ToLower().Contains(Name)
                || m.Hang.ToLower().Contains(Name)
                || m.Model.ToLower().Contains(Name)
                || m.SerialNumber.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public PagedList.IPagedList<ListNumber> getListTrash(string Name, int pageSize, int pageNumber)
        {
            var list = dbContext.ListNumbers.Where(m => m.Status != 1).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                list = list.Where(m => m.Tenthietbi.ToLower().Contains(Name)
                 || m.Hang.ToLower().Contains(Name)
                 || m.Model.ToLower().Contains(Name)
                 || m.SerialNumber.ToLower().Contains(Name));
            }
            return list.OrderByDescending(m => m.Id).ToPagedList(pageNumber, pageSize);
        }
        public List<ListNumber> getListId(int? id)
        {
            if (id == null)
            {
                return new List<ListNumber>();
            }

            return dbContext.ListNumbers
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }
        public bool CheckImageExists(string imageName)
        { 
            return dbContext.ListNumbers.Any(l => l.Image == imageName);
        }

        public bool CheckSerialNumberExists(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                return false;
            }
            return dbContext.ListNumbers.Any(m => m.SerialNumber == serialNumber && m.Status == 1);
        }
        
        public List<ListNumber> getListAll()
        {
            return dbContext.ListNumbers.Where(m => m.Status == 1).ToList();
        }
        public List<ListNumber> getListOne(int? id)
        {
            return dbContext.ListNumbers.Where(m => m.Id == id && m.Status == 1).ToList();
        }

        public List<ListNumber> getList(string status = "All")
        {
            List<ListNumber> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = dbContext.ListNumbers.Where(m => m.Status != 0).OrderByDescending(m => m.Id).ToList();
                        break;
                    }
                default:
                    {
                        list = dbContext.ListNumbers.ToList();
                        break;
                    }
            }
            return list;
        }
        public ListNumber getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return dbContext.ListNumbers.Where(m => m.Id == id).FirstOrDefault();
            }
        }

        public int Insert(ListNumber ListNumber)
        {
            dbContext.ListNumbers.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(ListNumber ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(ListNumber ListNumber)
        {
            dbContext.ListNumbers.Remove(ListNumber);
            return dbContext.SaveChanges();
        }


    }
}
