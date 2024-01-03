using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eStoreClient_HE163971.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System;

namespace eStoreClient_HE163971.Pages
{
    public class ProductModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Product> listProduct { get; set; }

        public ProductModel(ILogger<ProductModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/product/Product";
        }
        public void OnGet()
        {
   
           
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
              
                client.BaseAddress = new Uri("https://localhost:7063/product/Product");

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PostAsync("https://localhost:7063/product/Product", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage("ProductList");

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
