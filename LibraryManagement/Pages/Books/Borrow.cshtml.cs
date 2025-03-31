using DataAccessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace LibraryManagement.Pages.Books
{
    [Authorize(Roles = "Student")]
    public class BorrowModel : PageModel
    {
        private readonly BorrowDAO _borrowDAO;
        public string Message { get; set; }
        public BorrowModel(BorrowDAO borrowDAO)
        {
            _borrowDAO = borrowDAO;
        }
        public void OnGet(int id)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
            {
                Message = "Invalid user ID";
                return;
            }

            bool success = _borrowDAO.BorrowBook(userId, id);
            Message = success ? "Borrowed successfully!" : "You have already borrow this book.";
        }
    }
}
