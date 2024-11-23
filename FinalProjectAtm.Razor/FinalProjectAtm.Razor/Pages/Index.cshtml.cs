using AtmModels;
using AtmServices;
using AtmServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;


namespace FinalProjectAtm.Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMemoryCache _memoryCache;
        public IndexModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        private IUserServices _userServices = new UserServices();

        [BindProperty]
        public User User { get; set; }

        public void OnGet(int userId)
        {
            if (_memoryCache.Get("Id") != null)
            {
                User = _userServices.GetUserById(Convert.ToInt32(_memoryCache.Get("Id")));
            }
        }

        public IActionResult OnPostLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessageNullOrEmpty"] = "Please fill in all fields before submitting the form.";
                return RedirectToPage("/Index");
            }
            User = _userServices.Login(username, password);
            if (User != null)
            {
                _memoryCache.Set("Id", User.Id);
                return RedirectToPage("/Menu");
            }
            else
            {
                TempData["ErrorMessage"] = "Incorrect login. Please try again.";
                return RedirectToPage("/Index");
            }

        }

    }
}
