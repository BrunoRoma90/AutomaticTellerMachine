using AtmModels;
using AtmServices;
using AtmServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace FinalProjectAtm.Razor.Pages
{
    public class MenuModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;

        public MenuModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        private IUserServices _userServices = new UserServices();

        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
            if (_memoryCache.Get("Id") != null)
            {
                User = _userServices.GetUserById(Convert.ToInt32(_memoryCache.Get("Id")));
            }
        }

        public IActionResult OnPost(string userChoice)
        {
            if (userChoice == "bankTransfer")
            {
                return RedirectToPage("/Transactions/BankTransfer");
            }
            else if (userChoice == "withdrawalTransaction")
            {
                return RedirectToPage("/Transactions/Withdrawal");
            }
            else if (userChoice == "deposits")
            {
                return RedirectToPage("/Transactions/Deposits");
            }
            else if (userChoice == "paymentServices")
            {
                return RedirectToPage("/Transactions/PaymentServices");
            }
            else if (userChoice == "otherOptions")
            {
                return RedirectToPage("/Students/LoginStudent");
            }
            else
            {

                return Page();
            }
        }
    }
}
