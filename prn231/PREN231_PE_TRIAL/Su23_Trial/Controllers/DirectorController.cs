using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Su23_Trial.Models;

namespace Su23_Trial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : Controller
    {
        private readonly PE_PRN_Fall22B1Context _context;
        public DirectorController(PE_PRN_Fall22B1Context context) {

            _context = context;
        }
        [HttpGet("GetDirectors/{nationality}/{gender}")]
        public IActionResult GetDirectors(string nationality, string gender) {
            var result = _context.Directors.Where(x => x.Nationality.ToLower() == nationality.ToLower()
            && x.Male == (gender.ToLower()=="male")).ToList();  
            if(result == null)
            {
                return NotFound();
            }

            var res = result.Select(x => new DirecroeDTO()
            {
                Id = x.Id,
                FullName = x.FullName,
                Description = x.Description,    
                Dob = x.Dob,
                DobString = x.Dob.ToString("MM/dd/yyy"),
                Nationality = x.Nationality,
                Gender = x.Male? "Male": "Female" 

            });
            return Ok(res);
        }

        [HttpGet("GetDirector/{id}")]
        public IActionResult GetDirector(int id)
        {
            var res = _context.Directors.Include(x => x.Movies)
                .ThenInclude(x => x.Producer)
                .FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            var result = new DirecroeDTO
            {
                Id = res.Id,
                FullName = res.FullName,
                Description = res.Description,
                Dob = res.Dob,
                DobString = res.Dob.ToString("MM/dd/yyy"),
                Nationality = res.Nationality,
                Gender = res.Male ? "Male" : "Female",
                Movies = res.Movies.Select(x => new MovieDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    DirectorId = x.DirectorId,
                    DirectorName = x.Director?.FullName,
                    ProducerId = x.Producer?.Id,
                    ProducerName = x.Producer?.Name,
                    Language = x.Language,
                    ReleaseDate = x.ReleaseDate,
                    ReleasYear  = x.ReleaseDate?.Year,
                    Title  = x.Title,
                    Genres = x.Genres,
                    Stars = x.Stars
                }).ToList()

            };

            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult Create(DirectorDTORequest request)
        {
            try
            {
                var director = new Director()
                {
                    Description = request.Description,
                    Dob = Convert.ToDateTime(request.Dob),
                    Nationality = request.Nationality,
                    FullName = request.FullName,
                    Male = request.Male

                };
               _context.Directors.Add(director);
                var rowff = _context.SaveChanges();
                return Ok(rowff);



            }
            catch (Exception ex) 
            {
                return Conflict("There was an error when add Director"); 
            }

            return Ok();
        }

    }
}
