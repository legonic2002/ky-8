using Q2.Models;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using GivenAPIs.Models;

namespace Q2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:5000/api/";
        }

        public async Task<IActionResult> Index(int id=0)
        {
            HttpResponseMessage responseMessage = await client.GetAsync("http://localhost:5000/api/Studio/List");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                string strData = await responseMessage.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Studio> listProduct = JsonSerializer.Deserialize<List<Studio>>(strData, option);
             
                if(id==0 && listProduct.Count>0)
                {
                    id = listProduct[0].StudioId;
                }
                HttpResponseMessage responseMovie = await client.GetAsync("http://localhost:5000/api/Movie/List/"+id);
                List<Movie> movies = new List<Movie>();
                if (responseMovie.IsSuccessStatusCode)
                {
                    string ContentMoovie = await responseMovie.Content.ReadAsStringAsync();
                    movies = JsonSerializer.Deserialize<List<Movie>>(ContentMoovie, option);
                   
                }
                ViewBag.listProduct = listProduct;
                ViewBag.movies = movies;
                return View();
            }          
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}