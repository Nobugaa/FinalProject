using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using ProductManagementASPNETCoreRazorPage.Hubs;

namespace LibraryManagement.Pages.Books
{
    public class CreateBookModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        private readonly UserDAO _userDAO;
        private readonly IHubContext<SignalrServer> _hubContext;
        public string ErrorMessage { get; set; }
        public CreateBookModel(BookDAO bookDAO, UserDAO userDAO, IHubContext<SignalrServer> hubContext)
        {
            _bookDAO = bookDAO;
            _userDAO = userDAO;
            _hubContext = hubContext;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var title = Request.Form["Title"];
            var author = Request.Form["Author"];
            var category = Request.Form["Category"];
            var isbn = Request.Form["Isbn"];
            var quantity = int.Parse(Request.Form["Quantity"]);

            // Lấy ID người dùng đăng nhập từ Claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                ErrorMessage = "You must be logged in to create a book.";
                return Page();
            }
            int createdBy = int.Parse(userIdClaim.Value); // Chuyển ID về kiểu số nguyên

            var book = new Book
            {
                Title = title,
                Category = category,
                Author = author,
                Quantity = quantity,
                Isbn = isbn,
                CreatedAt = DateTime.Now,
                AddedBy = createdBy,// Lưu ID người tạo sách ,
                AddedByNavigation = _userDAO.GetById(createdBy)

            };

            var success = _bookDAO.Create(book);
            if (success)
            {
                await _hubContext.Clients.All.SendAsync("LoadALL"); // Notify clients
                return RedirectToPage("/Books/Index");
            }


            ErrorMessage = "Book already exists";
            return Page();
        }
    }
}
