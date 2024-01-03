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
    public class ProductListModel : PageModel
    {
       
       
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Product> listProduct { get; set; }
        public ProductListModel(ILogger<ProductListModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/product/Product";

        }

        public async Task OnGetAsync(string? search)
        {
            if (search != null)
            {
                HttpResponseMessage response = await client.GetAsync(ProductApiUri);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
               
                listProduct = JsonSerializer.Deserialize<List<Product>>(strData, options);
                List<Product> temp = new List<Product>();
                foreach (Product p in listProduct)
                {
                    if (p.ProductName.Contains(search.ToLower()))
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
            listProduct = JsonSerializer.Deserialize<List<Product>>(strData, options);
            }
        }
        public ActionResult OnGetDelete(int? id)
        {
           

            var result = client.DeleteAsync("https://localhost:7063/product/Product/"+id+"");
            return RedirectToPage("Index");
        }
    }
}
