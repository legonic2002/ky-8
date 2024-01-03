using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eStoreClient_HE163971.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System;

namespace eStoreClient_HE163971.Pages
{
    public class OrderDetailCreateModel : PageModel
    {
        [BindProperty]
        public OrderDetail OrderDetail { get; set; }
        [BindProperty]
        public int OrdderId { get; set; }

        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<OrderDetail> listProduct { get; set; }

        public OrderDetailCreateModel(ILogger<OrderDetailCreateModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/OrderDetail/OrderDetail";
        }
        public void OnGet()
        {
   
           
        }
        public void OnGetDelete(int id)
        {
            OrdderId = id;
            if (id != null)
            {
                OrdderId= id;
            }
            else
            {
                OrdderId = 0;
            }
          

            
        }
            public ActionResult OnPost()
        {
            OrderDetail.OrderId = OrdderId;
            var product = OrderDetail;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
              
                client.BaseAddress = new Uri("https://localhost:7063/OrderDetail/OrderDetail");

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PostAsync("https://localhost:7063/OrderDetail/OrderDetail", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage("OrderList");

                }
                else
                {
                    return RedirectToPage("Index");
                }

            }
            return RedirectToPage("Index");
        }
    }
}
