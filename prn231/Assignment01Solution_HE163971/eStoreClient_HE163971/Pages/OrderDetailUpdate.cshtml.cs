using eStoreClient_HE163971.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient_HE163971.Pages
{
    public class OrderDetailUpdateModel : PageModel
    {

        [BindProperty]
        public OrderDetail Product { get; set; }
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<OrderDetail> listProduct { get; set; }

        public OrderDetailUpdateModel(ILogger<OrderDetailUpdateModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/OrderDetail/OrderDetail";
        }
        public ActionResult OnPost()
        {
            var product = Product;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {

                client.BaseAddress = new Uri("https://localhost:7063/OrderDetail/OrderDetail");

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PutAsync("https://localhost:7063/OrderDetail/OrderDetail/ ", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage("OrderList");

                }
                else
                {
                    return RedirectToPage("Index");
                }

            }

        }
        public async Task OnGetDelete(int? pid, int? oid)
        {

          

            HttpResponseMessage response = await client.GetAsync(ProductApiUri);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            listProduct = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);
            foreach (OrderDetail p in listProduct)
            {
                if (p.OrderId == oid && p.ProductId == pid)
                {
                    Product = p;
                }
            }



        }
    }
}
