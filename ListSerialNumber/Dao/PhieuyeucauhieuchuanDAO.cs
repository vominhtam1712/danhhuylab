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
    public class PhieuyeucauhieuchuanDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public IEnumerable<Phieuyeucauhieuchuan> GetRowsByIds(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Enumerable.Empty<Phieuyeucauhieuchuan>();  
            }

            return dbContext.Phieuyeucauhieuchuans
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.Id))
                .ToList();
        }
        public string Layphieutudong()
        {
            var lastCode = dbContext.Phieuyeucauhieuchuans
                .Where(m => m.MaPhieu.StartsWith("MP"))
                .Select(m => m.MaPhieu)
                .Max();

            string nextcode;
            if (string.IsNullOrEmpty(lastCode))
            {
                nextcode = "MP0001";
            }
            else
            {
                var currentCode = lastCode;
                var numberPart = currentCode.Substring(2);
                var number = int.Parse(numberPart);
                number++;

                if (number < 10000)
                {
                    nextcode = $"MP{number:D4}";
                }
                else
                {
                    nextcode = $"MP{number}";
                }
            }
            return nextcode;
        }
        public PagedList.IPagedList<Phieuyeucauhieuchuan> getList(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.Phieuyeucauhieuchuans.Where(m => m.Status == 1);
             
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m => m.Diachihieuchuan.ToLower().Contains(Name) ||
                                          m.Tencognty.ToLower().Contains(Name) ||
                                          m.Diachicongty.ToLower().Contains(Name) ||
                                          m.Masothue.ToLower().Contains(Name) ||
                                          m.Dichvuyeucau.ToLower().Contains(Name) ||
                                          m.Createby.ToLower().Contains(Name) ||
                                          m.UpdateBy.ToLower().Contains(Name) ||
                                          m.Danhhuy.ToLower().Contains(Name));
            }

            return query.OrderByDescending(m => m.Id)
                        .AsNoTracking()
                        .ToPagedList(pageNumber, pageSize);
        }

        public PagedList.IPagedList<Phieuyeucauhieuchuan> getListTrash(string Name, int pageSize, int pageNumber)
        {
            var query = dbContext.Phieuyeucauhieuchuans.Where(m => m.Status != 1);
             
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(m => m.Diachihieuchuan.ToLower().Contains(Name) ||
                                          m.Tencognty.ToLower().Contains(Name) ||
                                          m.Diachicongty.ToLower().Contains(Name) ||
                                          m.Masothue.ToLower().Contains(Name) ||
                                          m.Dichvuyeucau.ToLower().Contains(Name) ||
                                          m.Createby.ToLower().Contains(Name) ||
                                          m.UpdateBy.ToLower().Contains(Name) ||
                                          m.Danhhuy.ToLower().Contains(Name));
            }

            return query.OrderByDescending(m => m.Id)
                        .AsNoTracking()
                        .ToPagedList(pageNumber, pageSize);
        }
        public List<Phieuyeucauhieuchuan> getListId(int? id)
        {
            if (id == null)
            {
                return new List<Phieuyeucauhieuchuan>();
            }

            return dbContext.Phieuyeucauhieuchuans
                .Where(m => m.Id == id.Value)
                .AsNoTracking()
                .ToList();
        }

        public Phieuyeucauhieuchuan getRow(int? id)
        {
            if (!id.HasValue)  
            {
                return null;
            }
             
            return dbContext.Phieuyeucauhieuchuans.Find(id);
        }
        public int Insert(Phieuyeucauhieuchuan phieuyeucauhieuchuan)
        {
            dbContext.Phieuyeucauhieuchuans.Add(phieuyeucauhieuchuan);
            return dbContext.SaveChanges();
        }
        public int Update(Phieuyeucauhieuchuan phieuyeucauhieuchuan)
        {
            dbContext.Entry(phieuyeucauhieuchuan).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(Phieuyeucauhieuchuan phieuyeucauhieuchuan)
        {
            dbContext.Phieuyeucauhieuchuans.Remove(phieuyeucauhieuchuan);
            return dbContext.SaveChanges();
        }
    }
}
