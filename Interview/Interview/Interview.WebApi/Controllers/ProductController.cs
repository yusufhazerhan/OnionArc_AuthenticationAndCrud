using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Entities;
using Interview.Service.Interface;
using Interview.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Interview.WebApi.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        [Route("create")]
        public ActionResult CreateProduct([FromBody] CreateProductModel model)
        {
            var result = _productService.CreateProduct(
                new Product
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    Amount= model.Amount
                });

            return Ok(result);
        }
        [HttpPost]
        [Route("update")]
        public ActionResult UpdateProduct([FromBody] UpdateProductModel model)
        {
            var result = _productService.UpdateProduct(
                new Product
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    Amount = model.Amount
                });

            return Ok(result);
        }
        [HttpDelete("(id)")]
        public ActionResult DeleteProduct(int id)
        {
            if(_productService.ProductById(id) != null)
            {
                _productService.DeleteProduct(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("products")]
        public ActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }
    }
}
