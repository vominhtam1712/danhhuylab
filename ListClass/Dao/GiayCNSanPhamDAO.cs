using ListClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListClass.Dao
{
    public class GiayCNSanPhamDAO
    {
        ListDBContext dbContext = new ListDBContext();
        public List<GiayCNSanPham> getList(int id)
        {
            return dbContext.GiayCNSanPhams.Where(m => m.Id_ListNumber == id).ToList();
        }
        public GiayCNSanPham GetById(int? id)
        {
            return dbContext.GiayCNSanPhams
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id_ListNumber == id);
        }
        public GiayCNSanPham GetId(int? id)
        {
            return dbContext.GiayCNSanPhams
                .AsNoTracking()
                .FirstOrDefault(ln => ln.Id == id);
        }
        public bool CheckTonTai(string fileName)
        {
            return dbContext.GiayCNSanPhams.Any(l => l.FileName == fileName);
        }
        public int Insert(GiayCNSanPham ListNumber)
        {
            dbContext.GiayCNSanPhams.Add(ListNumber);
            return dbContext.SaveChanges();
        }
        public int Update(GiayCNSanPham ListNumber)
        {
            dbContext.Entry(ListNumber).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public int Delete(GiayCNSanPham ListNumber)
        {
            dbContext.GiayCNSanPhams.Remove(ListNumber);
            return dbContext.SaveChanges();
        }
    }
}
