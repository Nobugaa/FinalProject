using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using ProductManagementASPNETCoreRazorPage.Hubs;

namespace LibraryManagement.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class DeleteBookModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        private readonly IHubContext<SignalrServer> _hubContext;
        public string ErrorMessage { get; set; }
        public DeleteBookModel(BookDAO bookDAO, IHubContext<SignalrServer> hubContext)
        {
            _bookDAO = bookDAO;
            _hubContext = hubContext;
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
            await _hubContext.Clients.All.SendAsync("LoadALL"); // Notify clients
            return RedirectToPage("./Index");
        }
    }
}
