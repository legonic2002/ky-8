using DemoLab3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DemoLab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Product> GetAllProducts()
        {
           return  ProductList();
        }
        public List<Product> ProductList()
        {
            var prod = new List<Product>()
            {
                new Product{ ProductId = 1 , ProductName ="a", Price =1 },
                 new Product{ ProductId = 2 , ProductName ="B", Price =2 },
                  new Product{ ProductId = 3 , ProductName ="C", Price =3 },
                  new Product{ ProductId = 4 , ProductName ="D", Price =4 },

            };
            return prod;
        }

        [HttpGet]
        [Route("GetById/{prodId}")]
        public Product GetProductById( int prodId)
        {
            return ProductList().SingleOrDefault(p => p.ProductId == prodId);
        }

    }
}
