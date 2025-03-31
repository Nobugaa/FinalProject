using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Books
{
    public class DeleteBookModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        public string ErrorMessage { get; set; }
        public DeleteBookModel(BookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _bookDAO.Delete(Book);
            return RedirectToPage("./Index");
        }
    }
}
