using DemoContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoContentNegotiation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        // GET: api/<BlogController>
        [HttpGet]
        public IActionResult Get()
        {
            var blogs = new List<Blog>();
            var blogPosts = new List<BlogPost>();
            blogPosts.Add(new BlogPost
            {
                Title = "Content Negotiation in Web API",
                MetaDescription = "Content negotiation is the process of selecting the best resource for a response when multiple resource representations are available.",
                Published = true
            });
            blogs.Add(new Blog()
            {
                Name = "Code Maze",
                Description = "C#, .NET and Web Development Tutorials",
                BlogPosts = blogPosts
            });
            return Ok(blogs);
        }

       
    }
}
