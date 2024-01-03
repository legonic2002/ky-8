using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient client = null;
        public string ProductApiUrl = "";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5111/odata/Members";
        }

        public IActionResult Index()
        {
            return RedirectToAction("/Products");
        }

        public IActionResult ToMain()
        {
            return View("AdminView");
        }
        public IActionResult ToMemberView(int id)
        {
            ViewBag.OpenID = id;
            return View("MemberView");
        }
        public async Task<IActionResult> GetList(int? id)
        {
            return View("OrderList");
            
        }

        public async Task<IActionResult> Login(string email, string password)
        {
            if (email.ToLower().Equals("admin@fpt") && password.ToLower().Equals("12345"))
            {
                return View("AdminView");
            }
            else if(email !=null && password != null) {

                ProductApiUrl = "https://localhost:7063/Member/Member";
                List<Member> items = new List<Member>();
                HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                items = JsonSerializer.Deserialize<List<Member>>(strData, options);

                Member member = new Member();
            foreach (Member item in items)
            {
                    
                        if (item.Email.ToLower().Equals(email.ToLower()) && item.Password.ToLower().Equals(password.ToLower()))
                        {


                            ViewBag.OpenID = item.MemberId;
                            return View("MemberView");

                        }
                    }
                   
                    
            

            }
            else
            {
                return View("Index");
            }
            return View("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["OrderId"] = id;
            return View("IndexDetails");
        }
        public async Task<IActionResult> EditProfile(int? id)
        {
            
            return View("UpdateMember");
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