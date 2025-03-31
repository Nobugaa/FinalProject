using DataAccessObject;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LibraryManagement.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly UserDAO _userDAO;
        public string ErrorMessage { get; set; }
        public LoginModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string email, string password)
        {
            var user = _userDAO.FindByEmail(email);


            if (user == null || user.Active != true)
            {
                ErrorMessage = "Your account has been deactivated. Please contact support.";
                return Page();
            }

            if (!_userDAO.ValidateUser(email, password))
            {
                ErrorMessage = "Invalid email or password";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, "Login");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToPage("/Books/Index");
        }

    }
}
