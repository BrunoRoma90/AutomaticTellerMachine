using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class MyBankTransfersModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public MyBankTransfersModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        private static IUserServices _userServices = new UserServices();
        private static IBankAccountServices _bankAccountServices = new BankAccountServices();
        private static IBankTransferTransactionServices _bankTransferTransaction = new BankTransferTransactionServices();
        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public List<BankTransferTransaction> BankTransfersTransaction { get; set; }

        public void OnGet()
        {
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);
            Account = _bankAccountServices.GetBankAccountByUserId(userId);
            BankTransfersTransaction = _bankTransferTransaction.GetBankTransferTransactionByBankAccountId(Account.Id);
            if (Account == null)
            {
                TempData["ErrorMessage"] = "No bank account found for the current user.";
            }
        }
    }
}
