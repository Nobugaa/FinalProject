using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserDAO _userDAO;
        private readonly BookDAO _bookDAO; // Add BookDAO to get books added by the user

        public ProfileModel(UserDAO userDAO, BookDAO bookDAO)
        {
            _userDAO = userDAO;
            _bookDAO = bookDAO;
        }

        public User User { get; set; } = default!;
        public List<Book> BooksAddedByAdmin { get; set; } = new List<Book>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Ensure id is valid
            if (id <= 0)
            {
                return NotFound();
            }

            // Retrieve user with borrowing history
            var user = _userDAO.GetByIdWithBorrowRecords(id);

            if (user == null)
            {
                return NotFound();
            }

            User = user;

            // Check if the logged-in user is an admin (role "1")
            var role = HttpContext.Session.GetString("UserRole"); // Assuming UserRole is stored in session
            if (role == "1")
            {
                // Fetch books added by this admin
                BooksAddedByAdmin = _bookDAO.GetBooksAddedByUser(User.Id).ToList();
            }

            return Page();
        }
    }
}
