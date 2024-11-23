using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;

namespace FinalProjectAtm.Razor.Pages.Transactions
{
    public class DepositModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public DepositModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        private IUserServices _userServices = new UserServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();
        private IDepositTransactionServices _depositTransaction = new DepositTransactionServices();

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public DepositTransaction DepositTransaction { get; set; }
        public void OnGet()
        {
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);
            Account = _bankAccountServices.GetBankAccountById(userId);

            if (Account == null)
            {
                TempData["ErrorMessage"] = "No bank account found for the current user.";
            }
        }


        public IActionResult OnPost(int amount)
        {
            if (amount <= 0)
            {
                TempData["ErrorMessage"] = "The amount must be greater than zero.";
                return Page();
            }
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);

            Account = _bankAccountServices.GetBankAccountByUserId(userId);

            bool depositlSuccessful = _depositTransaction.Deposit(Account.Id, amount);

            if (depositlSuccessful)
            {
                TempData["SuccessMessage"] = "Deposit completed successfully!";
                return RedirectToPage("/Menu");
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred during the Deposit. Please try again.";
                return Page();
            }
        }
    }
}
