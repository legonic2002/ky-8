using eStoreClient_HE163971.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace eStoreClient_HE163971.Pages
{
    public class OrderListModel : PageModel
    {
       
       
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Order> listProduct { get; set; }
        public List<OrderDetail> listOrderDetail { get; set; }
        public OrderListModel(ILogger<OrderListModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/order/Order";

        }

        public async Task OnGetAsync(DateTime? StartDate, DateTime? EndDate)
        {
            if(StartDate!=null && EndDate != null)
            {
                HttpResponseMessage response = await client.GetAsync(ProductApiUri);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                listProduct = JsonSerializer.Deserialize<List<Order>>(strData, options);
                List<Order> temp = new List<Order>();
                foreach (Order p in listProduct)
                {
                    if (p.OrderDate.CompareTo(StartDate)>0 && p.OrderDate.CompareTo(EndDate) < 0)
                    {
                        temp.Add(p);
                    }
                }
                listProduct = temp;

            }
            else {

            HttpResponseMessage response = await client.GetAsync(ProductApiUri);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listProduct = JsonSerializer.Deserialize<List<Order>>(strData, options);

                ProductApiUri = "https://localhost:7063/orderDetail/OrderDetail";
                 response = await client.GetAsync(ProductApiUri);
                 strData = await response.Content.ReadAsStringAsync();
                 options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                listOrderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);


            }



        }
        public ActionResult OnPost(int? pid, int? oid)
        {


            var result = client.DeleteAsync("https://localhost:7063/OrderDetail/OrderDetail?pid="+pid+"&oid="+oid+"");
            return RedirectToPage("orderList");
        }
        public ActionResult OnGetDelete(int? id)
        {
           

            var result = client.DeleteAsync("https://localhost:7063/order/Order/"+id+"");
            return RedirectToPage("orderList");
        }
    }
}
