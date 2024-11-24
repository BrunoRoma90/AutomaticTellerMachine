using AtmModels;
using AtmServices;
using AtmServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace FinalProjectAtm.Razor.Pages.Transactions
{
    public class BankTransferModel : PageModel
    {

        private readonly IMemoryCache _memoryCache;
        public BankTransferModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;
        
        private IBankTransferTransactionServices _bankTransfer = new BankTransferTransactionServices();
        private IUserServices _userServices = new UserServices();
        private IBankAccountServices _bankAccountServices = new BankAccountServices();

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public BankTransferTransaction bankTransferTransaction { get; set; }
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

        public IActionResult OnPost(string destinationAccountNumber, int amount)
        {

            if (amount <= 0)
            {
                TempData["ErrorMessage"] = "The amount must be greater than zero.";
                return Page();
            }
            if (string.IsNullOrEmpty(destinationAccountNumber))
            {
                TempData["ErrorMessage"] = "Please enter a valid destination account number.";
                return Page();
            }

            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);

            Account = _bankAccountServices.GetBankAccountByUserId(userId);

            if (Account == null)
            {
                TempData["ErrorMessage"] = "No account found for the current user.";
                return Page();
            }

            // Verificar saldo suficiente
            if (Account.Balance < amount)
            {
                TempData["ErrorMessage"] = "Insufficient balance for this transfer.";
                return Page();
            }


            BankAccount destinationAccount = _bankAccountServices.GetBankAccountIdByNumber(destinationAccountNumber);


            if (destinationAccount == null)
            {
                TempData["ErrorMessage"] = "Destination account not found. Please check the account number.";
                return Page();
            }

            bool transferSuccessful = _bankTransfer.BankTransfer(Account.Id, amount, destinationAccount.Id);

            if (transferSuccessful)
            {
                TempData["SuccessMessage"] = "Transfer completed successfully!";
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
