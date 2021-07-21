using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_API_1.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(e =>
            {
                e.Property("Name").HasMaxLength(200);
            });

            // set Product-Invoice n-n relationship

            // set composite key
            builder.Entity<ProductInvoice>().HasKey(e => new { e.ProductId, e.InvoiceId });

            //product tbl
            builder.Entity<ProductInvoice>(entity =>
            {
                entity.HasOne(e => e.Product)
                .WithMany(e => e.Invoices)
                .HasForeignKey(e => e.ProductId);
            });
            // invoice tbl
            builder.Entity<ProductInvoice>(entity =>
            {
                entity.HasOne(e => e.Invoice)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.InvoiceId);
            });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
