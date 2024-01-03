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
    public class MemberListModel : PageModel
    {
       
       
        private readonly HttpClient client = null;
        private string ProductApiUri = "";
        public List<Member> listProduct { get; set; }
        public MemberListModel(ILogger<MemberListModel> logger)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUri = "https://localhost:7063/Member/Member";

        }

        public async Task OnGetAsync()
        {
            
            HttpResponseMessage response = await client.GetAsync(ProductApiUri);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            listProduct = JsonSerializer.Deserialize<List<Member>>(strData, options);

        }
        public ActionResult OnGetDelete(int? id)
        {
           

            var result = client.DeleteAsync("https://localhost:7063/Member/Member/" + id+"");
            return RedirectToPage("Index");
        }
    }
}
