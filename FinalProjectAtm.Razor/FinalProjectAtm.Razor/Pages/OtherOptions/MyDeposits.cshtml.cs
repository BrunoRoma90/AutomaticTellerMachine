using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;
using System.Security.Principal;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class MyDepositsModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public MyDepositsModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        private static IUserServices _userServices = new UserServices();
        private static IBankAccountServices _bankAccountServices = new BankAccountServices();
        private static IDepositTransactionServices _depositTransaction = new DepositTransactionServices();
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public List<DepositTransaction> DepositTransaction { get; set; }
        public void OnGet()
        {
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);
            Account = _bankAccountServices.GetBankAccountByUserId(userId);
            DepositTransaction = _depositTransaction.GetDepositTransactionsByBankAccountId(Account.Id);
            if (Account == null)
            {
                TempData["ErrorMessage"] = "No bank account found for the current user.";
            }

        }
    }
}
