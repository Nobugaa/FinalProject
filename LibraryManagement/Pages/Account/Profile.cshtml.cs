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

        public User User1 { get; set; } = default!;
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

            User1 = user;

            if (User.IsInRole("Admin"))
            {
                // Fetch books added by this admin
                BooksAddedByAdmin = _bookDAO.GetBooksAddedByUser(User1.Id).ToList();
            }

            return Page();
        }
    }
}
