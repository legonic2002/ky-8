using Lab01_ASP.NETCoreWebAPI.Models;
using Lab01_ASP.NETCoreWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab01_ASP.NETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();
        // GET: api/<ValuesController>
        [HttpGet]
       public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();

  /*      // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult PostProduct(Product product)
       /* public IActionResult PostProduct(string ProductName, int CategoryId, int UnitsInStock, decimal UnitPrice)*/

        {
            /*var product = new Product();
            product.ProductName = ProductName;
            product.CategoryId = CategoryId;
            product.UnitPrice = UnitPrice;
            product.UnitsInStock = UnitsInStock;*/
            repository.SaveProduct(product);
            return NoContent();
        }

        // PUT api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id )
        {
            var p = repository.GetProductById(id);
            if (p == null) return NotFound(); 

            repository.DeleteProduct(p);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id,Product p)
        {
            var pTmp = repository.GetProductById(id);
            if (p == null) return NotFound();

            repository.UpdateProduct(p);
            return NoContent();
        }
    }
}
