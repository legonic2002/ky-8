using Assignment01Solution_HE163971.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment01Solution_HE163971.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // GET: api/<CategoriesController>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var list = new List<Category>();
            try
            {
                using (var context = new MyDbContext())
                {
                    list = context.Categories.ToList();

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return list;
        }

     
        
    }
}
