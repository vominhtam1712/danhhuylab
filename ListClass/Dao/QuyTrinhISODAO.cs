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
    public class QuyTrinhISODAO
    {
        ListDBContext dbContext = new ListDBContext(); 
        public List<QuyTrinhISO> getListQuyTrinhISO()
        {
            return dbContext.QuyTrinhISOs.ToList();
        }
        public QuyTrinhISO GetById(int? id)
        {
            return dbContext.QuyTrinhISOs
                .AsNoTracking()
                .FirstOrDefault(ln => ln.id == id);
        }
        public QuyTrinhISO02 GetById2(int? id)
        {
            return dbContext.QuyTrinhISO02s
                .AsNoTracking()
                .FirstOrDefault(ln => ln.id == id);
        }
        public bool CheckDelete(int? id)
        {
            return dbContext.QuyTrinhISO02s.Any(l => l.id_quytrinh == id);
        }
        public bool CheckTonTai(string fileName)
        {
            return dbContext.QuyTrinhISO02s.Any(l => l.FileName == fileName);
        }
        public IEnumerable<QuyTrinhISO> GetRowsByIds(IEnumerable<int> ids)
        {
            return dbContext.QuyTrinhISOs
                .AsNoTracking()
                .Where(ln => ids.Contains(ln.id))
                .ToList();
        }
        public List<QuyTrinhISO02> getListQuyTrinhISO02()
        {
            return dbContext.QuyTrinhISO02s.ToList();
        }
        public int Insert(QuyTrinhISO company)
        {
            dbContext.QuyTrinhISOs.Add(company);
            return dbContext.SaveChanges();
        }
        public int Insert2(QuyTrinhISO02 company)
        {
            dbContext.QuyTrinhISO02s.Add(company);
            return dbContext.SaveChanges();
        }
        public int Update(QuyTrinhISO company)
        {
            dbContext.Entry(company).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
    }
}
