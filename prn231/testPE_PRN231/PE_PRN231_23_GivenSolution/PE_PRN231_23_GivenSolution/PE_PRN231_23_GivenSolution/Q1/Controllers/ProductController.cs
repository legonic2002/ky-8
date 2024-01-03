using AutoMapper;
using LuyenDePRN231.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Q1.Models;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly LuyenDePRN231S2Context _context;
        private readonly IMapper _mapper;
        public ProductController( IMapper mapper)
        {
            _mapper = mapper;
            _context = new LuyenDePRN231S2Context();
        }
        // GET: api/<ProductController>
        [HttpGet("index")]
        [EnableQuery]
        [Produces( "application/json", "text/csv")]
        public ActionResult<IEnumerable<ProductDTO>> Index()
        {
            List<Product> products = _context.Products.AsQueryable().Include(x=>x.Category).ToList();
            List<ProductDTO> listDTO = _mapper.Map<List<ProductDTO>>(products);
            if (Request.Headers.ContainsKey("Accept") && Request.Headers["Accept"].ToString().Contains("text/csv"))
            {
                var csvData = new StringBuilder();
                foreach (var item in listDTO)
                {
                    csvData.AppendLine($"{item.ProductId};{item.ProductName}");
                }
                var csv = new ContentResult
                {
                    Content = csvData.ToString(),
                    ContentType = "text/csv",
                    StatusCode = 200,
                };
                return csv;
            }
            else
            {
                return Ok(listDTO);
            }

               
        }
        [HttpGet("List/{minprice}/{maxprice}")]
        public IActionResult List(int minprice, int maxprice)
        {
            List<Product> products =new List<Product>();
            if(minprice < 0) minprice = 0;
            if(maxprice < 0) maxprice = 0;
            if(maxprice == 0)
            {

                 products = _context.Products.Include(x => x.Category)
                .Where(x => x.UnitPrice > minprice).ToList();
            }
            else
            {
                 products = _context.Products.Include(x => x.Category)
                .Where(x => x.UnitPrice > minprice && x.UnitPrice < maxprice).ToList();
            }


            List<ProductDTO> listDTO = _mapper.Map<List<ProductDTO>>(products);
            return Ok(listDTO);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Product p = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (p != null)
            {
                try
                {   
                            List<OrderDetail> details = _context.OrderDetails
                                .Where(x => x.ProductId == p.ProductId)
                                .ToList();
                            if (details.Count > 0)
                            {
                                _context.OrderDetails.RemoveRange(details);
                                _context.SaveChanges();
                            }
                    _context.Products.Remove(p);
                    _context.SaveChanges();
                    return Ok("Delete success");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Ok("Not found");
            }
           
        }

    }
}
