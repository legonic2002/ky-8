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
    public class OrderDetailListModel : PageModel
    {
       
       
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<OrderDetail> listProduct { get; set; }
        public OrderDetailListModel(ILogger<OrderDetailListModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/orderDetail/OrderDetail";

        }

        public async Task OnGetAsync()
        {

            ProductApiUri = "https://localhost:7063/orderDetail/OrderDetail";
            HttpResponseMessage response = await client.GetAsync(ProductApiUri);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listProduct = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);

        }
  
    }
}
