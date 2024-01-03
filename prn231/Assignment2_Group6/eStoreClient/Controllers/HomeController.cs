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
            return View();
        }

        public IActionResult ToMain()
        {
            return View("AdminView");
        }
        public async Task<IActionResult> GetList(int? id)
        {
            ViewBag.OpenID = id;
            ProductApiUrl = "http://localhost:5111/odata/Orders";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Order> items = ((JArray)temp.value).Select(
            x => new Order
            {
                OrderId = (int)x["OrderId"],
                MemberId = (int)x["MemberId"],
                OrderDate = (DateTime)x["OrderDate"],
                RequiredDate = (DateTime)x["RequiredDate"],
                ShippedDate = (DateTime)x["ShippedDate"],
                Freight = (int)x["Freight"]


            }

            ).ToList();


            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
            response = await client.GetAsync(ProductApiUrl);
            strData = await response.Content.ReadAsStringAsync();

            temp = JObject.Parse(strData);
            lst = temp.value;

            List<OrderDetail> listODetails = ((JArray)temp.value).Select(
            x => new OrderDetail
            {
                OrderDetailId = (int)x["OrderDetailId"],
                ProductId = (int)x["ProductId"],
                OrderId = (int)x["OrderId"],
                UnitPrice = (int)x["UnitPrice"],
                Quantity = (int)x["Quantity"],
                Discount = (int)x["Discount"],
            }

            ).ToList();

            ViewBag.listODetails = listODetails;

            MainPageModel mymodel = new MainPageModel();
            mymodel.Order = items;
            mymodel.OrderDetail = listODetails;

            return View("OrderList",mymodel);
        }
        public async Task<IActionResult> Login(string email, string password)
        {
            if (email.ToLower().Equals("admin@fpt") && password.ToLower().Equals("12345"))
            {
                return View("AdminView");
            }
            else { 


            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Member> items = ((JArray)temp.value).Select(
            x => new Member
            {
                MemberId = (int)x["MemberId"],
                Email = (string)x["Email"],
                CompanyName = (string)x["CompanyName"],
                City = (string)x["City"],
                Country = (string)x["Country"],
                Password = (string)x["Password"]


            }

            ).ToList();
            Member member = new Member();
            foreach (Member item in items)
            {
               
                 if (item.Email.ToLower().Equals(email.ToLower()) && item.Password.ToLower().Equals(password.ToLower()))
                {
                    
                   
                        ViewBag.OpenID = item.MemberId;
                        return View("MemberView");
                    
                }
            }
            return View("Index");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.OpenID = id;
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Member> items = ((JArray)temp.value).Select(
            x => new Member
            {
                MemberId = (int)x["MemberId"],
                Email = (string)x["Email"],
                CompanyName = (string)x["CompanyName"],
                City = (string)x["City"],
                Country = (string)x["Country"],
                Password = (string)x["Password"]


            }

            ).ToList();

            Member member = new Member();
            foreach (Member item in items)
            {
                if (item.MemberId == id)
                {
                    member = item;
                }
            }

            if (member == null)
            {
                return NotFound();
            }
            
            return View("~/Views/Members/Edit.cshtml", member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            ViewBag.OpenID = member.MemberId;
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    var json = JsonSerializer.Serialize(member);
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                    var result = client.PutAsync(ProductApiUrl + "/" + member.MemberId + "", content).Result;
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return View("MemberView");
            }
            return View(member);
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