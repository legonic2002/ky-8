using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using eStoreClient_HE163971.Models;

namespace eStoreClient_HE163971.Pages
{
    public class ProductUpdateModel : PageModel
    {

        [BindProperty]
        public Product Product { get; set; }
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Product> listProduct { get; set; }

        public ProductUpdateModel(ILogger<ProductUpdateModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/product/Product";
        }
        public ActionResult OnPost()
        {
            var product = Product;
            if (!ModelState.IsValid)
            {
                return RedirectToPage("Index");
            }
            else
            {

                client.BaseAddress = new Uri("https://localhost:7063/product/Product");

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PutAsync("https://localhost:7063/product/Product", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage("ProductList");

                }
                else
                {
                    return Page();
                }

            }
            
        }
        public async Task OnGetDelete(int? id)
        {

            var product = Product;

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
                if (p.ProductId==id)
                {
                    Product = p;
                }
            }
           


        }
    }
}
