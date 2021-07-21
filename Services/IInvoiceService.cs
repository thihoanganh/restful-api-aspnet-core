using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET_Core_API_1.Models;


namespace ASPNET_Core_API_1.Services
{
    public interface IInvoiceService
    {
        public Invoice Create(List<Product> products);
        public Invoice Update(int invoiceId, List<Product> products);
        public bool Delete(int id);
        public Task<Invoice> Find(int id);
        public List<Invoice> FindAll();
    }
}
