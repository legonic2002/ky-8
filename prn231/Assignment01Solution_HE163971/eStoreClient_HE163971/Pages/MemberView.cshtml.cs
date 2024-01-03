using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient_HE163971.Pages
{
    public class MemberViewModel : PageModel
    {
        private readonly ILogger<MemberViewModel> _logger;
        
        public MemberViewModel(ILogger<MemberViewModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public string Visitor { get; set; }
        public void OnGet(string visitorId)
        {
            Visitor = visitorId;
        }
        public ActionResult OnPost()
        {
            return RedirectToPage("MemberViewOrderList", new { visitorId = Visitor.ToString() });
        }

    }
}