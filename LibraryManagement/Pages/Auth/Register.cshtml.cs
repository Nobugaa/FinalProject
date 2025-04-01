using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly UserDAO _userDAO;
        public string ErrorMessage { get; set; }

        public RegisterModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string fullName, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            var existingUser = _userDAO.FindByEmail(email);
            if (existingUser != null)
            {
                ErrorMessage = "Email is already in use.";
                return Page();
            }

            var newUser = new BusinessObject.Models.User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                Role = "Student",  // Default role
                CreatedAt = DateTime.Now,
                Active = true
            };

            _userDAO.Add(newUser);

            return RedirectToPage("/Auth/Login");
        }

    }
}
