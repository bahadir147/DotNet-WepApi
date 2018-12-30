using BahadirDemir.WebApiDemo.DataAccess;
using BahadirDemir.WebApiDemo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BahadirDemir.WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        IProductDal _productDal;

        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }


        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(x => x.ProductId == productId);

                if (product == null)
                {
                    return NotFound($"There is no product with Id ={productId}");
                }

                return Ok(product);
            }
            catch (Exception) { }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch (Exception)
            { }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody]Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch (Exception)
            { }

            return BadRequest();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {
                _productDal.Delete(new Product { ProductId = productId });
                return Ok();
            }
            catch (Exception)
            { }

            return BadRequest();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetProductDetails")]
        public IActionResult GetProductWithDetails()
        {
            try
            {
                var result = _productDal.GetProductWithDetails();
                return Ok(result);
            }
            catch (Exception)
            { }
            return BadRequest();
        }
    }
}
