using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class MenuOtherOptionsModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;

        public MenuOtherOptionsModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
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
            if (userChoice == "balance")
            {
                return RedirectToPage("/OtherOptions/MyBalance");
            }
            else if (userChoice == "transations")
            {
                return RedirectToPage("/OtherOptions/MyTransactions");
            }
            else if (userChoice == "informations")
            {
                return RedirectToPage("/OtherOptions/MyAccount");
            }
            else if (userChoice == "bankTransfer")
            {
                return RedirectToPage("/OtherOptions/MyBankTransfers");
            }
            else if (userChoice == "withdrawals")
            {
                return RedirectToPage("/OtherOptions/MyWithdrawls");
            }
            else if (userChoice == "deposits")
            {
                return RedirectToPage("/OtherOptions/MyDeposits");
            }
            else if (userChoice == "paymentServices")
            {
                return RedirectToPage("/OtherOptions/MyPaymentsServices");
            }
            else
            {

                return Page();
            }
        }
    }
}
