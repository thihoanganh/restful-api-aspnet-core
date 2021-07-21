using ASPNET_Core_API_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_API_1.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _db;
        public InvoiceService(AppDbContext db)
        {
            _db = db;
        }
        public Invoice Create(List<Product> products)
        {
            try
            {
                var invoice = new Invoice(); // create invoice obj
                var listProducts = new List<ProductInvoice>();
                var productsInDb = new List<Product>();
                products.ForEach(p => productsInDb.Add(_db.Product.Find(p.Id)));

                productsInDb.ForEach(p => listProducts.Add(new ProductInvoice()
                {
                    Product = p,
                    Invoice = invoice
                }));

                invoice.Total = products.Sum(p => p.Quantity * p.Price); // set invoice total
                invoice.Products = listProducts;  //set invoice products
                _db.Invoice.Add(invoice);
                _db.SaveChanges();
                return invoice;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _db.Invoice.Remove(_db.Invoice.Find(id));
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Invoice> Find(int id)
        {
            try
            {
                var invoice = await _db.Invoice.AsNoTracking().Where(i => i.Id == id).Include(i => i.Products).ThenInclude(i => i.Product).FirstOrDefaultAsync();
                return invoice;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public List<Invoice> FindAll()
        {
            return _db.Invoice.AsNoTracking().Include(i => i.Products).ThenInclude(i => i.Product).ToList();
        }

        public Invoice Update(int invoiceId, List<Product> products)
        {
            try
            {
                var targetInvoice = _db.Invoice.Where(i => i.Id == invoiceId).Include(i => i.Products).ThenInclude(i => i.Product).FirstOrDefault();
                if (targetInvoice == null)
                {
                    return null;
                }
                else
                {
                    var dbProducts = new List<Product>(); // create list of Product to store Products that query from Db by Input Products 
                    products.ForEach(p => dbProducts.Add(_db.Product.Find(p.Id)));  // find it and add to list

                    var productInvoice = new List<ProductInvoice>(); // create list of ProductInvoice. This will asign for entity to update
                    dbProducts.ForEach(p => productInvoice.Add(new ProductInvoice()
                    {
                        Product = p,
                        Invoice = targetInvoice
                    }));

                    targetInvoice.Products = productInvoice; // update lists product of invoice
                    targetInvoice.Total = dbProducts.Sum(p => p.Quantity * p.Price); // update total 
                    _db.SaveChanges();
                    return targetInvoice;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
