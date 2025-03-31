using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Books
{
    public class CreateBookModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        public string ErrorMessage { get; set; }
        public CreateBookModel(BookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var title = Request.Form["Title"];
            var author = Request.Form["Author"];
            var category = Request.Form["Category"];
            var isbn = Request.Form["Isbn"];
            var quantity = int.Parse(Request.Form["Quantity"]);
            var book = new Book
            {
                Title = title,
                Category = category,
                Author = author,
                Quantity = quantity,
                Isbn = isbn,
                CreatedAt = System.DateTime.Now
            };
            var success = _bookDAO.Create(book);
            if (success)
            {
                return RedirectToPage("/Books/Index");
            }
            ErrorMessage = "Course already exists";
            return Page();
        }
    }
}
