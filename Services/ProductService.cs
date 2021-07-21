using ASPNET_Core_API_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_API_1.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }
        public int Create(Product p)
        {
            try
            {
                _db.Product.Add(p);
                _db.SaveChanges();
                return p.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _db.Product.Remove(_db.Product.Find(id));
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Product Find(int id)
        {
            return _db.Product.AsNoTracking().Find(id);
        }

        public List<Product> FindAll()
        {
            return _db.Product.ToList();
        }

        public Product Update(Product p)
        {
            try
            {
                var exist = _db.Product.Find(p.Id); // get current Product from db by Id
                _db.Entry(exist).CurrentValues.SetValues(p);
                _db.SaveChanges();
                return p;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
