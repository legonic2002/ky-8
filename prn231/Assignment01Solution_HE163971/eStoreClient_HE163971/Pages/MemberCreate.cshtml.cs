using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eStoreClient_HE163971.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System;

namespace eStoreClient_HE163971.Pages
{
    public class MemberCreateModel : PageModel
    {
        [BindProperty]
        public Member Member { get; set; }
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Member> listProduct { get; set; }

        public MemberCreateModel(ILogger<MemberCreateModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/Member/Member";
        }
        public void OnGet()
        {
   
           
        }
        public ActionResult OnPost()
        {
            var product = Member;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
              
                client.BaseAddress = new Uri("https://localhost:7063/Member/Member");

                var json = JsonSerializer.Serialize(product);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var result = client.PostAsync("https://localhost:7063/Member/Member", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToPage("MemberList");

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
