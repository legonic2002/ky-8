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
using System.Diagnostics.Metrics;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eStoreClient.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HttpClient client = null;
        public string ProductApiUrl = "";

        public OrdersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "http://localhost:5111/odata/Orders";
        }

        // GET: Orders
   
        public async Task<IActionResult> Index()
        {
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

            ViewBag.listODetails= listODetails;

            MainPageModel mymodel = new MainPageModel();
            mymodel.Order = items;
            mymodel.OrderDetail = listODetails;

            return View(mymodel);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            Order member = new Order();
            foreach (Order item in items)
            {
                if (item.OrderId == id)
                {
                    member = item;
                }
            }


            return View(member);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] Order order)
        {
            if (ModelState.IsValid)
            {
                Order temp = new Order()
                {
                    MemberId = order.MemberId,
                    OrderDate = order.OrderDate,
                    RequiredDate = order.RequiredDate,
                    ShippedDate = order.ShippedDate,
                    Freight = order.Freight

                };

                string json = JsonConvert.SerializeObject(temp,
                new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzzz" });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.PostAsync(ProductApiUrl, content);
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            Order member = new Order();
            foreach (Order item in items)
            {
                if (item.OrderId == id)
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

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,OrderDate,RequiredDate,ShippedDate,Freight")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    string json = JsonConvert.SerializeObject(order,
                 new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzzz" });
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                    var result = client.PutAsync(ProductApiUrl + "/" + order.OrderId + "", content).Result;
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            Order member = new Order();
            foreach (Order item in items)
            {
                if (item.OrderId == id)
                {
                    member = item;
                }
            }

            if (member != null)
            {
                var result = client.DeleteAsync(ProductApiUrl + "/" + member.OrderId + "");
            }

            return View(member);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = new Order();
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


            foreach (Order item in items)
            {
                if (item.OrderId == id)
                {
                    member = item;
                }
            }

            if (member != null)
            {
                var result = client.DeleteAsync(ProductApiUrl + "/" + member.OrderId + "");
            }


            return RedirectToAction(nameof(Index));
        }

        //====================================================================================================



        // GET: OrderDetails/Details/5
       

        // GET: OrderDetails/Create
        public async Task<IActionResult> CreateDetails()
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

            return View("CreateDetails");
        }


        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDetails([Bind("OrderDetailId,ProductId,OrderId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {

            ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
            var json = System.Text.Json.JsonSerializer.Serialize(orderDetail);
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
            mymodel.Order = ListOrder;
            mymodel.OrderDetail = listODetails;

            return View("Index",mymodel);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> EditDetails(int? id)
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
          
            return View("EditDetails", member);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetails(int id, [Bind("OrderDetailId,ProductId,OrderId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }


            try
            {
                ProductApiUrl = "http://localhost:5111/odata/OrderDetails";
                var json = System.Text.Json.JsonSerializer.Serialize(orderDetail);
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
        public async Task<IActionResult> DeleteDetails(int? id)
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

            return View("DeleteDetails",member);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("DeleteDetails")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDetailsConfirmed(int id)
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
