using AtmServices.Interfaces;
using AtmServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using AtmModels;
using System.Security.Principal;

namespace FinalProjectAtm.Razor.Pages.OtherOptions
{
    public class UpdatePasswordModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public UpdatePasswordModel(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        private IUserServices _userServices = new UserServices();
        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);


            if (User == null)
            {
                TempData["ErrorMessage"] = "No user found";
            }
        }

        public IActionResult OnPost(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "Please enter a valid password";
                return Page();
            }
            int userId = Convert.ToInt32(_memoryCache.Get("Id"));
            User = _userServices.GetUserById(userId);
            User.Password = password;
            bool changePasswordSuccessful = _userServices.UpdateUserPassword(userId,User.Password);
            if (changePasswordSuccessful)
            {
                TempData["SuccessMessage"] = "Change password completed successfully!";
                return RedirectToPage("/OtherOptions/MyAccount");
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred during change password. Please try again.";
                return Page();
            }
        }
    }
}
