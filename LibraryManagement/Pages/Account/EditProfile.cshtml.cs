using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Account
{
    public class EditProfileModel : PageModel
    {

        private readonly UserDAO _userDAO;

        public EditProfileModel(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

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
            User = user;
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

            // Check if user exists
            var existingUser = _userDAO.GetById(User.Id);
            if (existingUser == null)
            {
                return NotFound(); // Return 404 if the user doesn't exist
            }


            bool updated = _userDAO.Update(User);
            if (!updated)
            {
                return BadRequest("Failed to update user.");
            }


            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _userDAO.GetById(id) != null;
        }
    }
}
