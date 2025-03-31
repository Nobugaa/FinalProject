using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Books
{
    public class BookDetailsModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        public string ErrorMessage { get; set; }
        public BookDetailsModel(BookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookDAO.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }
    }
}
