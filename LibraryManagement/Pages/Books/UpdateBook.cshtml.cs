using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Books
{
    public class UpdateBookModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        public string ErrorMessage { get; set; }
        public UpdateBookModel(BookDAO bookDAO)
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
            Book = book;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _bookDAO.Update(Book);

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _bookDAO.GetById(id) != null;
        }

    }
}
