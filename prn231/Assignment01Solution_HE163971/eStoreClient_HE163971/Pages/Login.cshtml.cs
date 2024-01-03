using eStoreClient_HE163971.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eStoreClient_HE163971.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string pass { get; set; }
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Member> listProduct { get; set; }
        public LoginModel(ILogger<LoginModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/Member/Member";

        }

        public async Task <IActionResult> OnPost()
        {


            HttpResponseMessage response = await client.GetAsync(ProductApiUri);
            string strData = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listProduct = JsonSerializer.Deserialize<List<Member>>(strData, options);

            foreach (Member p in listProduct)
            {
                if(p.Email.Equals(email.ToLower())&& p.Password.Equals(pass.ToLower()))
                {
                    if (email.ToLower().Equals("admin@gmail.com") && pass.ToLower().Equals("123456"))
                    {
                        return RedirectToPage("Index");

                    }
                    else
                    {
                        return RedirectToPage("MemberView", new { visitorId = p.MemberId.ToString() });
                    }
                }
                

            }
            

            return RedirectToPage("Login");

        }
    }
}
