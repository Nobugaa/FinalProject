using BusinessObject.Models;
using DataAccessObject;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagement.Pages.Books
{
    public class IndexBookModel : PageModel
    {
        private readonly BookDAO _bookDAO;
        public string Keyword { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IndexBookModel(BookDAO bookDAO)
        {
            _bookDAO = bookDAO;
        }
        public void OnGet(string searchString)
        {
            Keyword = searchString;
            Books = _bookDAO.FindByName(Keyword);
        }
    }
}
