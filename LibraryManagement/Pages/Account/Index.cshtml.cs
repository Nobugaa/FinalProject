using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Account
{
    public class IndexUserModel : PageModel
    {
        private readonly UserDAO _userDAO;

        public IEnumerable<User> User { get; set; } = default!;

        public IndexUserModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        public async Task OnGetAsync()
        {
            User = _userDAO.GetAll();
        }

        public IActionResult OnPostToggleActive(int id)
        {
            var user = _userDAO.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Active = !user.Active; // Toggle active state
            _userDAO.Update(user);

            return RedirectToPage();
        }
    }
}
