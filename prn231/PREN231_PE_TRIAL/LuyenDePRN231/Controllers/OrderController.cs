using AutoMapper;
using LuyenDePRN231.DTO;
using LuyenDePRN231.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LuyenDePRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private  readonly LuyenPRN231Context _context;
        private readonly IMapper _mapper;

        public OrderController(LuyenPRN231Context context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<OrderController>
        [HttpGet("getallorder")]
        public IActionResult getallorder()
        {
            List<Order> data = _context.Orders
                .Include(x=> x.Employee)
                .Include(x=> x.Customer)
                .ToList();
            List<OrderDTO> orders = _mapper.Map<List<OrderDTO>>(data).ToList(); 

            return Ok(orders);
        }

        [HttpGet("getorderbydate/{From}/{To}")]
        public IActionResult getorderbydate(string From, string To)
        {
            if(!DateTime.TryParse(From,out DateTime fromDate) || !DateTime.TryParse(To, out DateTime toDate))
            {
                return BadRequest("invalid");
            }
            List<Order> data = _context.Orders
               .Include(x => x.Employee)
               .Include(x => x.Customer)
               .Where(x=> x.OrderDate >= fromDate && x.OrderDate <= toDate).ToList();
            List<OrderDTO> orders = _mapper.Map<List<OrderDTO>>(data).ToList();
            return Ok(orders);
        }

    }
}
