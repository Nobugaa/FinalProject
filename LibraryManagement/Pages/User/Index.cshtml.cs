using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Pages.User
{
    public class IndexUserModel : PageModel
    {
        private readonly BusinessObject.Models.MyLibraryContext _context;

        public IndexUserModel(BusinessObject.Models.MyLibraryContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
