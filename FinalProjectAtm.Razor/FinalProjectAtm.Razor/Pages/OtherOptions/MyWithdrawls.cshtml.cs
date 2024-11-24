using AtmModels;
using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class MyWithdrawlsModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public MyWithdrawlsModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        private static IUserServices _userServices = new UserServices();
        private static IBankAccountServices _bankAccountServices = new BankAccountServices();
        private static IWithdrawalTransactionServices _withdrawalTransaction = new WithdrawalTransactionServices();
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public List<WithdrawalTransaction> WithdrawalTransaction { get; set; }

        public void OnGet()
        {
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);
            Account = _bankAccountServices.GetBankAccountByUserId(userId);
            WithdrawalTransaction = _withdrawalTransaction.GetWithdrawalTransactionsByBankAccountId(Account.Id);
            if (Account == null)
            {
                TempData["ErrorMessage"] = "No bank account found for the current user.";
            }
        }
    }
}
