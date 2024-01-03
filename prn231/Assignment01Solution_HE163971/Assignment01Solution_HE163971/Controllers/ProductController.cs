using Assignment01Solution_HE163971.Models;
using Assignment01Solution_HE163971.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment01Solution_HE163971.Controllers
{
    [Route("product/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository = new ProductRepository();
        // GET: api/<ValuesController>
        [HttpGet]
       public ActionResult<IEnumerable<Product>> GetProducts() => repository.GetProducts();


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            
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

      
        [HttpPut]
        public IActionResult UpdateProduct(Product p)
        {
           
            if (p == null) return NotFound();

            repository.UpdateProduct(p);
            return NoContent();
        }
    }
}
