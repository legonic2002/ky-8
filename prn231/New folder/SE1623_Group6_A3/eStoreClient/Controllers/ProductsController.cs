using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eStoreClient.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        public string ProductApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5111/odata/Products";

        }

        public async Task<List<Product>>GetList()
        {

            ProductApiUrl = "http://localhost:5111/odata/Products";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> items = new List<Product>();

            items = ((JArray)temp.value).Select(
            x => new Product
            {
               ProductId = (int)x["ProductId"],
                CategoryId = (int)x["CategoryId"],
                ProductName = (string)x["ProductName"],
                Weight = (float)x["Weight"],
                UnitPrice = (int)x["UnitPrice"],
                UnitInStock = (int)x["UnitInStock"]
            }

            ).ToList();

            return items;
        }

        public async Task<Product> GetProduct(int? id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> items = new List<Product>();

            items = ((JArray)temp.value).Select(
            x => new Product
            {
                ProductId = (int)x["ProductId"],
                CategoryId = (int)x["CategoryId"],
                ProductName = (string)x["ProductName"],
                Weight = (float)x["Weight"],
                UnitPrice = (int)x["UnitPrice"],
                UnitInStock = (int)x["UnitInStock"]
            }

            ).ToList();

            Product member = new Product();
            foreach (Product item in items)
            {
                if (item.ProductId == id)
                {
                    member = item;
                }
            }

            return member;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? search)
        {
            List<Product> items = new List<Product>();
            return View(items);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> items = new List<Product>();

            items = ((JArray)temp.value).Select(
            x => new Product
            {
                ProductId = (int)x["ProductId"],
                CategoryId = (int)x["CategoryId"],
                ProductName = (string)x["ProductName"],
                Weight = (float)x["Weight"],
                UnitPrice = (int)x["UnitPrice"],
                UnitInStock = (int)x["UnitInStock"]
            }

            ).ToList();

            Product member = new Product();
            foreach (Product item in items)
            {
                if (item.ProductId == id)
                {
                    member = item;
                }
            }

            return View(member);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ProductApiUrl = "https://localhost:7063/api/Categories";
            List<Category> items = new List<Category>();
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            items = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(strData, options);


            ViewData["CategoryId"] = new SelectList(items, "CategoryId", "CategoryId");

            
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PostAsync(ProductApiUrl, content).Result;
            }
        
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            return View("Edit");
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var json = JsonSerializer.Serialize(product);
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                    var result = client.PutAsync(ProductApiUrl + "/" + product.ProductId + "", content).Result;
                }
                catch (DbUpdateConcurrencyException)
                {
                
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
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

            List<Product> items = new List<Product>();

            items = ((JArray)temp.value).Select(
            x => new Product
            {
                ProductId = (int)x["ProductId"],
                CategoryId = (int)x["CategoryId"],
                ProductName = (string)x["ProductName"],
                Weight = (float)x["Weight"],
                UnitPrice = (int)x["UnitPrice"],
                UnitInStock = (int)x["UnitInStock"]
            }

            ).ToList();

            Product member = new Product();
            foreach (Product item in items)
            {
                if (item.ProductId == id)
                {
                    member = item;
                }
            }

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                var result = client.DeleteAsync(ProductApiUrl + "/" + member.ProductId + "");
            }

            return View(member);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> items = new List<Product>();

            items = ((JArray)temp.value).Select(
            x => new Product
            {
                ProductId = (int)x["ProductId"],
                CategoryId = (int)x["CategoryId"],
                ProductName = (string)x["ProductName"],
                Weight = (float)x["Weight"],
                UnitPrice = (int)x["UnitPrice"],
                UnitInStock = (int)x["UnitInStock"]
            }

            ).ToList();

            Product member = new Product();
            foreach (Product item in items)
            {
                if (item.ProductId == id)
                {
                    member = item;
                }
            }



            if (member != null)
            {
                var result = client.DeleteAsync(ProductApiUrl + "/" + member.ProductId + "");
            }
            
        
            return RedirectToAction(nameof(Index));
        }

      
    }
}
