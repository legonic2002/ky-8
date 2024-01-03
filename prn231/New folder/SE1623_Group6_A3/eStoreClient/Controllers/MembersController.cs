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
            ProductApiUrl = "https://localhost:7063/Member/Member";


        }
        public IActionResult Create()
        {
            return View();
        }
        // GET: Members
        public async Task<IActionResult> Index()
        {
            List<Member> items = new List<Member>();
            return View(items);
        }

        // GET: Members/Details/5

        public async Task<IActionResult> Edit(int? id)
        {
            return View("UpdateMember");
        }

    }
}
