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
    public class MemberViewOrderListModel : PageModel
    {
       
       
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Order> listProduct { get; set; }
        public MemberViewOrderListModel(ILogger<MemberViewOrderListModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/order/Order";

        }

        public async Task OnGetAsync(string visitorId)
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
                    if (p.MemberId == Int32.Parse(visitorId))
                    {
                        temp.Add(p);
                    }
                }
                listProduct = temp;

            
        }
       
    }
}
