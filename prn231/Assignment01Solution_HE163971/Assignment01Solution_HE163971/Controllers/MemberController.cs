using Assignment01Solution_HE163971.Models;
using Assignment01Solution_HE163971.Repository;
using Lab01_ASP.NETCoreWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment01Solution_HE163971.Controllers
{
    [Route("Member/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberRepository repository = new MemberRepository();
        // GET: api/<ValuesController>
        [HttpGet]
       public ActionResult<IEnumerable<Member>> GetMembers() => repository.GetMembers();

  /*      // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult PostMember(Member product)
       /* public IActionResult PostProduct(string ProductName, int CategoryId, int UnitsInStock, decimal UnitPrice)*/

        {
            /*var product = new Product();
            product.ProductName = ProductName;
            product.CategoryId = CategoryId;
            product.UnitPrice = UnitPrice;
            product.UnitsInStock = UnitsInStock;*/
            repository.SaveMember(product);
            return NoContent();
        }

        // PUT api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id )
        {
            var p = repository.GetMemberById(id);
            if (p == null) return NotFound(); 

            repository.DeleteMember(p);
            return NoContent();
        }

        // DELETE api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, Member p)
        {
            var pTmp = repository.GetMemberById(id);
            if (p == null) return NotFound();

            repository.UpdateMember(p);
            return NoContent();
        }
    }
}
