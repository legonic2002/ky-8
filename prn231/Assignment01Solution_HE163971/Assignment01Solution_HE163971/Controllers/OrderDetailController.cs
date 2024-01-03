using Assignment01Solution_HE163971.Models;
using Assignment01Solution_HE163971.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment01Solution_HE163971.Controllers
{
    [Route("OrderDetail/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailRepository repository = new OrderDetailRepository();
        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetails() => repository.GetOrderDetails();

        /*      // GET api/<ValuesController>/5
              [HttpGet("{id}")]
              public string Get(int id)
              {
                  return "value";
              }*/

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult PostOrderDetail(OrderDetail product)
        /* public IActionResult PostProduct(string ProductName, int CategoryId, int UnitsInStock, decimal UnitPrice)*/

        {
            /*var product = new Product();
            product.ProductName = ProductName;
            product.CategoryId = CategoryId;
            product.UnitPrice = UnitPrice;
            product.UnitsInStock = UnitsInStock;*/
            repository.SaveOrderDetail(product);
            return NoContent();
        }

        // PUT api/<ValuesController>/5
        [HttpDelete("id")]
        public IActionResult DeleteOrderDetail(int pid , int oid)
        {
            
            var p = repository.GetOrderDetailById(pid, oid);
            if (p == null) return NotFound(); 

            repository.DeleteOrderDetail(p);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpPut]
        public IActionResult UpdateProduct( OrderDetail p)
        {
            int productid = p.ProductId;
            int orderid = p.OrderId;
            var pTmp = repository.GetOrderDetailById(productid, orderid);
            if (pTmp == null) return NotFound();

            repository.UpdateOrderDetail(p);
            return NoContent();
        }
    }
}
