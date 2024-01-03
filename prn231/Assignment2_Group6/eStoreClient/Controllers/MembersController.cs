using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eStoreClient.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class MembersController : Controller
    {
        private readonly HttpClient client = null;
        public string ProductApiUrl = "";

        public MembersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5111/odata/Members";


        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
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



            return View(items);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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


            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            if (ModelState.IsValid)
            {
                Member temp = new Member() {
                MemberId=member.MemberId,
                    Email = member.Email,
                    CompanyName = member.CompanyName,
                    City = member.City,
                    Country = member.Country,
                    Password = member.Password

                };
                var json = JsonSerializer.Serialize(temp);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PostAsync(ProductApiUrl, content).Result;
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
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
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
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
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
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

            if (member != null)
            {
                var result = client.DeleteAsync(ProductApiUrl+"/" + member.MemberId + "");
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var member = new Member();
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

           
            foreach (Member item in items)
            {
                if (item.MemberId == id)
                {
                    member = item;
                }
            }

            if (member != null)
            {
                var result = client.DeleteAsync(ProductApiUrl + "/" + member.MemberId + "");
            }
            
           
            return RedirectToAction(nameof(Index));
        }

       
    }
}
