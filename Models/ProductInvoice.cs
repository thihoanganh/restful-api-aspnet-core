using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_API_1.Models
{
    public class ProductInvoice
    {
        public int ProductId { get; set; } // field in tbl
        public Product Product { get; set; } // reference to Primary key of this entity 

        public int InvoiceId { get; set; } // field in tbl
        public Invoice Invoice { get; set; } // reference to Primary key of this entity

    }
}
