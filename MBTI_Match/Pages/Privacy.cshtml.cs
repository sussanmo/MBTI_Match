using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace MBTI_Match.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string dataTime = DateTime.Now.ToString("d", new CultureInfo("en-US"));
            ViewData["TImeStamp"] = dataTime;
        }
    }

}
