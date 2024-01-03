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
    public class EmployeesController : ControllerBase
    {
        private readonly LuyenPRN231Context _context;
        private readonly IMapper _mapper;

        public EmployeesController(LuyenPRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IActionResult getallorder()
        {
            List<Employee> data = _context.Employees
                .Include(x => x.Department)
                .ToList();
            List<EmployeeDTO> orders = _mapper.Map<List<EmployeeDTO>>(data).ToList();

            return Ok(orders);
        }

        [HttpGet("getemployeebyid/{id}")]
        public IActionResult getemployeebyid(string id)
        {
            if(!int.TryParse(id, out int result))
            {
                return BadRequest("id  khong dung dinh dang");
            }
            Employee data = _context.Employees
                .Include(x => x.Department)
                .FirstOrDefault(x => x.EmployeeId == int.Parse(id));
            if (data == null) {
                return BadRequest("Ko co employee");
            }
            var orders = _mapper.Map<EmployeeDTO>(data);

            return Ok(orders);
        }

        [HttpDelete("delete/{Employeeid}")]
        public IActionResult delete(string Employeeid)
        {
            if (!int.TryParse(Employeeid, out int result))
            {
                return BadRequest("id  khong dung dinh dang");
            }
            Employee data = _context.Employees
                .Include(x => x.Department)
                .FirstOrDefault(x => x.EmployeeId == int.Parse(Employeeid));
            if (data == null)
            {
                return BadRequest("Ko co employee");
            }
            else
            {
                try
                {
                    List<Order> orders = _context.Orders.Where(x => x.EmployeeId == result).ToList();
                    if (orders.Count > 0)
                    {
                        foreach(var order in orders)
                        {
                            List<OrderDetail> details = _context.OrderDetails
                                .Where(x => x.OrderId == order.OrderId)
                                .ToList();
                            if (details.Count > 0)
                            {
                                _context.OrderDetails.RemoveRange(details);
                                _context.SaveChanges();
                            }
                        }
                        _context.Orders.RemoveRange(orders);
                        _context.SaveChanges();
                    }
                    _context.Employees.RemoveRange(data);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


        }
    }
}
