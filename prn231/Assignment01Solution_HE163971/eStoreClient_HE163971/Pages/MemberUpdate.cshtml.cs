using eStoreClient_HE163971.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace eStoreClient_HE163971.Pages
{
    public class MemberUpdateModel : PageModel
    {

        [BindProperty]
        public Member Product { get; set; }
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Member> listProduct { get; set; }

        public MemberUpdateModel(ILogger<MemberUpdateModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/member/Member";
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

                client.BaseAddress = new Uri("https://localhost:7063/member/Member");

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PutAsync("https://localhost:7063/member/Member/" + product.MemberId + "", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage("MemberList");

                }
                else
                {
                    return RedirectToPage("Index");
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

            listProduct = JsonSerializer.Deserialize<List<Member>>(strData, options);
            List<Member> temp = new List<Member>();
            foreach (Member p in listProduct)
            {
                if (p.MemberId == id)
                {
                    Product = p;
                }
            }



        }
    }
}
