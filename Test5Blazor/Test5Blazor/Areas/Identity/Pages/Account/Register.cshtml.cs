using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Test5Blazor.Data.Models;
namespace Test5Blazor.Areas.Identity.Pages.Account
{
    
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public InputModel input { get; set; }
        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signinManager = signInManager;
            _userManager = userManager;

        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var Identity = new ApplicationUser() { UserName = input.Email.Split("@")[0], Email = input.Email  };
                var resault=await _userManager.CreateAsync(Identity,input.Password);
                if (resault.Succeeded)
                {
                    await _signinManager.SignInAsync(Identity, false);
                    return LocalRedirect("~/");
                }
            }
            return LocalRedirect("~/Identity/Account/Login");
        }
       
    }
    
}
