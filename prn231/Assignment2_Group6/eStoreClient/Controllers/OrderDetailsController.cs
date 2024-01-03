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
    public class OrderDetailsController : Controller
    {
        private readonly HttpClient client = null;
        public string ProductApiUrl = "";

        public OrderDetailsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";

        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<OrderDetail> items = ((JArray)temp.value).Select(
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



            return View(items);
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<OrderDetail> items = ((JArray)temp.value).Select(
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

            OrderDetail member = new OrderDetail();
            foreach (OrderDetail item in items)
            {
                if (item.OrderDetailId == id)
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

        // GET: OrderDetails/Create
        public async Task<IActionResult> Create()
        {
            ProductApiUrl = "http://localhost:5111/odata/Products";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> ListProduct = new List<Product>();

            ListProduct = ((JArray)temp.value).Select(
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

            ProductApiUrl = "http://localhost:5111/odata/Orders";
            response = await client.GetAsync(ProductApiUrl);
            strData = await response.Content.ReadAsStringAsync();

            temp = JObject.Parse(strData);
            lst = temp.value;

            List<Order> ListOrder = ((JArray)temp.value).Select(
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

            ViewData["OrderId"] = new SelectList(ListOrder, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(ListProduct, "ProductId", "ProductId");

            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,ProductId,OrderId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            

                var json = JsonSerializer.Serialize(orderDetail);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PostAsync(ProductApiUrl, content).Result;
                
            

            ProductApiUrl = "http://localhost:5111/odata/Products";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> ListProduct = new List<Product>();

            ListProduct = ((JArray)temp.value).Select(
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

            ProductApiUrl = "http://localhost:5111/odata/Orders";
             response = await client.GetAsync(ProductApiUrl);
             strData = await response.Content.ReadAsStringAsync();

             temp = JObject.Parse(strData);
             lst = temp.value;

            List<Order> ListOrder = ((JArray)temp.value).Select(
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

            ViewData["OrderId"] = new SelectList(ListOrder, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(ListProduct, "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<OrderDetail> items = ((JArray)temp.value).Select(
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
            OrderDetail member = new OrderDetail();
            foreach (OrderDetail item in items)
            {
                if (item.OrderDetailId == id)
                {
                    member = item;
                }
            }

            if (member == null)
            {
                return NotFound();
            }
            ProductApiUrl = "http://localhost:5111/odata/Products";
             response = await client.GetAsync(ProductApiUrl);
             strData = await response.Content.ReadAsStringAsync();

             temp = JObject.Parse(strData);
             lst = temp.value;

            List<Product> ListProduct = new List<Product>();

            ListProduct = ((JArray)temp.value).Select(
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

            ProductApiUrl = "http://localhost:5111/odata/Orders";
            response = await client.GetAsync(ProductApiUrl);
            strData = await response.Content.ReadAsStringAsync();

            temp = JObject.Parse(strData);
            lst = temp.value;

            List<Order> ListOrder = ((JArray)temp.value).Select(
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

            ViewData["OrderId"] = new SelectList(ListOrder, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(ListProduct, "ProductId", "ProductId");
            return View(member);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,ProductId,OrderId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

           
                try
                {
                    var json = JsonSerializer.Serialize(orderDetail);
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                    var result = client.PutAsync(ProductApiUrl + "/" + orderDetail.OrderDetailId + "", content).Result;
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
               
            
            ProductApiUrl = "http://localhost:5111/odata/Products";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<Product> ListProduct = new List<Product>();

            ListProduct = ((JArray)temp.value).Select(
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

            ProductApiUrl = "http://localhost:5111/odata/Orders";
            response = await client.GetAsync(ProductApiUrl);
            strData = await response.Content.ReadAsStringAsync();

            temp = JObject.Parse(strData);
            lst = temp.value;

            List<Order> ListOrder = ((JArray)temp.value).Select(
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

            ViewData["OrderId"] = new SelectList(ListOrder, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(ListProduct, "ProductId", "ProductId", orderDetail.ProductId);
            return RedirectToAction(nameof(Index));
           
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<OrderDetail> items = ((JArray)temp.value).Select(
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
            OrderDetail member = new OrderDetail();
            foreach (OrderDetail item in items)
            {
                if (item.OrderDetailId == id)
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

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;

            List<OrderDetail> items = ((JArray)temp.value).Select(
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
            OrderDetail member = new OrderDetail();
            foreach (OrderDetail item in items)
            {
                if (item.OrderDetailId == id)
                {
                    member = item;
                }
            }

            if (member != null)
            {
                var result = client.DeleteAsync(ProductApiUrl + "/" + member.OrderDetailId + "");
            }


            return RedirectToAction(nameof(Index));
        }


    }
}
