using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class MyAccountModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public MyAccountModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        private static IUserServices _userServices = new UserServices();
        private static IBankAccountServices _bankAccountServices = new BankAccountServices();
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }
        public void OnGet()
        {
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);
            Account = _bankAccountServices.GetBankAccountByUserId(userId);

            if (Account == null)
            {
                TempData["ErrorMessage"] = "No bank account found for the current user.";
            }
        }

        public IActionResult OnPost(string userChoice)
        {
            if (userChoice == "updatePassword")
            {
                return RedirectToPage("/OtherOptions/UpdatePassword");
            }
            else
            {

                return Page();
            }
        }
    }
}
