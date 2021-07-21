using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET_Core_API_1.Models;

namespace ASPNET_Core_API_1.Services
{
    public interface IProductService
    {
        public int Create(Product p);
        public Product Update(Product p);
        public bool Delete(int id);
        public Product Find(int id);
        public List<Product> FindAll();
    }
}
