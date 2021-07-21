using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET_Core_API_1.Services;
using ASPNET_Core_API_1.Models;

namespace ASPNET_Core_API_1.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public IActionResult Create([FromBody] List<Product> products)
        {
            try
            {
                Invoice invoice = _invoiceService.Create(products);
                if (invoice == null)
                {
                    return NotFound(new { msg = "Product not found" });
                }
                else
                {
                    var listProduct = invoice.Products.Select(p => new
                    {
                        id = p.ProductId,
                        name = p.Product.Name,
                        price = p.Product.Price,
                        quantity = p.Product.Quantity
                    });
                    var returnObj = new
                    {
                        id = invoice.Id,
                        total = invoice.Total,
                        products = listProduct,
                        total_product = listProduct.Count()
                    };
                    return Ok(returnObj);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] List<Product> products, int id)
        {
            try
            {
                var updateInvoice = _invoiceService.Update(id, products);
                if (updateInvoice == null)
                {
                    return NotFound(new { status = false, msg = "Can not update" });
                }
                else
                {
                    return Ok(updateInvoice);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var rs = _invoiceService.Delete(id);
                if (rs) return Ok(new { result = true, del_id = id });
                else return NotFound(new { result = false, msg = "Product not found" });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public IActionResult Find()
        {
            try
            {

                return Ok(_invoiceService.FindAll());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                var invoice = await _invoiceService.Find(id);

                if (invoice == null)
                {
                    return NotFound(new { msg = "Invoice not found" });
                }
                else
                {
                    return Ok(invoice);
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
