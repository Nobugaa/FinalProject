using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Account
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public DeleteUserModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userDAO.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userDAO.Update(User);

            return RedirectToPage("./Index");
        }
    }
}
