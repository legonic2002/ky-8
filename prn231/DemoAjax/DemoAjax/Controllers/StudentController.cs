using DemoAjax.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DemoAjax.Controllers
{
    [Route("api/[controllers]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        List<Reservation> _oRe = new List<Reservation>()
            {
                new Reservation { Id = 1, Name = "Steven", StartLocation = "New York", EndLocation = "Beijing" },
                new Reservation { Id = 2, Name = "John", StartLocation = "New Jersey", EndLocation = "Boston" },
                new Reservation { Id = 3, Name = "Martin", StartLocation = "London", EndLocation = "Paris" }
            };

        // GET: StudentController
        [HttpGet]
        public IActionResult Gets()
        {
            if (_oRe.Count == 0)
            {
                return NotFound("No list found");
            }

            return Ok(_oRe);
        }

    }
}
