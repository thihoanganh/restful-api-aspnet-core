
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_API_1.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public virtual List<ProductInvoice> Products { get; set; }


    }
}
