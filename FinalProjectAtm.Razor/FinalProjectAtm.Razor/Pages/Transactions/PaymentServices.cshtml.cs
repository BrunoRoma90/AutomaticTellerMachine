using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;

namespace FinalProjectAtm.Razor.Pages.Transactions
{
    public class PaymentServicesModel : PageModel
    {

        private readonly IMemoryCache _memoryCache;
        public PaymentServicesModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        private IUserServices _userServices = new UserServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();
        private IPaymentServicesTransactionServices _paymentServices = new PaymentServicesTransactionServices();

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public PaymentServicesTransaction PaymentServices { get; set; }
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

        public IActionResult OnPost(int amount, string nameService)
        {
            if (amount <= 0)
            {
                TempData["ErrorMessage"] = "The amount must be greater than zero.";
                return Page();
            }
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);

            Account = _bankAccountServices.GetBankAccountByUserId(userId);

            if (Account.Balance < amount)
            {
                TempData["ErrorMessage"] = "Insufficient balance for this Withdrawal.";
                return Page();
            }

            bool paymentServicesSuccessful = _paymentServices.PaymentServices(Account.Id, amount, nameService);

            if (paymentServicesSuccessful)
            {
                TempData["SuccessMessage"] = "withdrawal completed successfully!";
                return RedirectToPage("/Menu");
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred during the transfer. Please try again.";
                return Page();
            }

        }
    }
}
