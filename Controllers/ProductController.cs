using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET_Core_API_1.Models;
using ASPNET_Core_API_1.Services;

namespace ASPNET_Core_API_1.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public IActionResult Create([FromBody] Product p)
        {
            try
            {
                int id = _productService.Create(p);
                return Ok(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut]
        public IActionResult Update([FromBody] Product p)
        {
            try
            {
                var updateProduct = _productService.Update(p);
                return Ok(updateProduct);
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
                var rs = _productService.Delete(id);
                if (rs) return Ok(new { result = true, id = id });
                else return Ok(new { result = false });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {

                return Ok(_productService.FindAll());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            try
            {
                return Ok(_productService.Find(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
