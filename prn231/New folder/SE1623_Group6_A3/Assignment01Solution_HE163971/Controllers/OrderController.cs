using Assignment01Solution_HE163971.Models;
using Assignment01Solution_HE163971.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment01Solution_HE163971.Controllers
{
    [Route("Order/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();
        // GET: api/<ValuesController>
        [HttpGet]
       public ActionResult<IEnumerable<Order>> GetOrders() => repository.GetOrders();

  /*      // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult PostOrder(Order product)
       /* public IActionResult PostProduct(string ProductName, int CategoryId, int UnitsInStock, decimal UnitPrice)*/

        {
            /*var product = new Product();
            product.ProductName = ProductName;
            product.CategoryId = CategoryId;
            product.UnitPrice = UnitPrice;
            product.UnitsInStock = UnitsInStock;*/
            repository.SaveOrder(product);
            return NoContent();
        }

        // PUT api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id )
        {
            var p = repository.GetOrderById(id);
            if (p == null) return NotFound(); 

            repository.DeleteOrder(p);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Order p)
        {
            var pTmp = repository.GetOrderById(id);
            if (p == null) return NotFound();

            repository.UpdateOrder(p);
            return NoContent();
        }
    }
}
