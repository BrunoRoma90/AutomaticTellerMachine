using AtmModels;
using AtmServices;
using AtmServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class MyBalanceModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public MyBalanceModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
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
    }
}
